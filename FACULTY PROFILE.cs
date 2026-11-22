#nullable enable

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ScottPlot.WinForms;

namespace BUCS_SIS
{
    public partial class FacultyProfileForm : Form
    {
        private readonly int teacherAccountId;

        private readonly string connectionString =
            Environment.GetEnvironmentVariable("BUCS_SIS_CONNECTION_STRING")
            ?? "server=localhost;port=3306;database=student_db;user id=root;password=@Bok15518;";

        private readonly DataTable roster = new DataTable();

        // IDs that were displayed when the roster was loaded.
        private readonly HashSet<int> originalStudents =
            new HashSet<int>();

        private int cohortId;
        private string cohortName = "";
        private readonly double[] savedEnrollmentCounts = new double[3];
        private bool hasChanges;
        private bool loaded;
        private bool changingRosterFilter;

        public FacultyProfileForm()
        {
            InitializeComponent();
        }

        public FacultyProfileForm(int authenticatedTeacherId) : this()
        {
            if (authenticatedTeacherId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(authenticatedTeacherId));
            }

            teacherAccountId = authenticatedTeacherId;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (loaded)
            {
                return;
            }

            loaded = true;

            if (teacherAccountId <= 0)
            {
                ShowError("Please log in through the faculty portal.");
                Close();
                return;
            }

            ConfigureControls();

            try
            {
                LoadFaculty();
                LoadRoster();
            }
            catch (MySqlException ex)
            {
                ShowError("Unable to load faculty information.\n\n" +
                    ex.Message);

                Close();
            }
            catch (InvalidOperationException ex)
            {
                ShowError(ex.Message);
                Close();
            }
        }

        private void ConfigureControls()
        {
            Text = "FACULTY PROFILE";

            picProfessor.SizeMode = PictureBoxSizeMode.Zoom;

            txtStudentId.MaxLength = 50;
            txtName.ReadOnly = true;

            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[]
            {
                "Regular", "Irregular", "LOA"
            });
            cmbStatus.SelectedIndex = 0;

            btnAdd.Text = "Add Student";
            btnDrop.Text = "Drop Student";
            btnSave.Text = "Save Changes";
            btnBack.Text = "Back";

            roster.Columns.Add("AccountId", typeof(int));
            roster.Columns.Add("StudentId", typeof(string));
            roster.Columns.Add("Status", typeof(string));
            roster.Columns.Add("StudentName", typeof(string));
            roster.PrimaryKey = new[]
            {
                roster.Columns["AccountId"]!
            };

            tablepanel.AutoGenerateColumns = false;
            tablepanel.Columns.Clear();

            tablepanel.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStudentId",
                HeaderText = "Student ID",
                DataPropertyName = "StudentId",
                FillWeight = 27
            });

            tablepanel.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Enrollment Status",
                DataPropertyName = "Status",
                FillWeight = 25
            });

            tablepanel.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStudentName",
                HeaderText = "Student Name",
                DataPropertyName = "StudentName",
                FillWeight = 48
            });

            roster.CaseSensitive = false;
            tablepanel.DataSource = roster.DefaultView;
            roster.DefaultView.ListChanged += (_, _) => UpdateRosterCount();
            txtSearchStudent.TextChanged += FilterRoster;
            btnClearSearch.Click += (_, _) =>
            {
                txtSearchStudent.Clear();
                txtSearchStudent.Focus();
            };
            UpdateRosterCount();
            tablepanel.ReadOnly = true;
            tablepanel.AllowUserToAddRows = false;
            tablepanel.AllowUserToDeleteRows = false;
            tablepanel.AllowUserToResizeRows = false;
            tablepanel.MultiSelect = false;
            tablepanel.RowHeadersVisible = false;

            tablepanel.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            tablepanel.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            tablepanel.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            tablepanel.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            tablepanel.EnableHeadersVisualStyles = false;
            tablepanel.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(49, 49, 103);
            tablepanel.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tablepanel.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                tablepanel.ColumnHeadersDefaultCellStyle.BackColor;
            tablepanel.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            tablepanel.ColumnHeadersDefaultCellStyle.Padding = new Padding(9, 0, 0, 0);
            tablepanel.DefaultCellStyle.Padding = new Padding(9, 0, 0, 0);
            tablepanel.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 221, 250);
            tablepanel.DefaultCellStyle.SelectionForeColor = Color.FromArgb(24, 25, 63);
            tablepanel.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(241, 243, 252);
            tablepanel.DefaultCellStyle.ForeColor = Color.Black;
            tablepanel.DefaultCellStyle.BackColor = Color.White;
            tablepanel.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            // Space for 15 data rows at the current display scaling.
            int rowHeight = (int)Math.Round(28 * DeviceDpi / 96.0);
            int headerHeight = (int)Math.Round(34 * DeviceDpi / 96.0);

            tablepanel.RowTemplate.Height = rowHeight;
            tablepanel.ColumnHeadersHeight = headerHeight;
            // Fixed columns, with vertical scrolling for larger rosters.
            tablepanel.ScrollBars = ScrollBars.Vertical;

            foreach (DataGridViewColumn column in tablepanel.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            btnAdd.Click += AddStudent;
            btnDrop.Click += DropStudent;
            btnSave.Click += SaveChanges;
            btnEnrollmentChart.Click += ShowEnrollmentChart;
            btnBack.Click += (sender, e) => Close();

            tablepanel.SelectionChanged += RosterSelectionChanged;

            CancelButton = btnBack;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Rectangle area = ClientRectangle;
            if (area.Width <= 0 || area.Height <= 0)
                return;

            using var background = new LinearGradientBrush(
                area,
                Color.FromArgb(20, 20, 69),
                Color.FromArgb(54, 53, 103),
                LinearGradientMode.ForwardDiagonal);
            e.Graphics.FillRectangle(background, e.ClipRectangle);

            if (facultyBackdrop is Image artwork)
            {
                int sourceWidth = Math.Max(1, artwork.Width * 13 / 100);
                int stripWidth = Math.Min(area.Width, (int)Math.Round(112 * DeviceDpi / 96.0));
                var source = new Rectangle(artwork.Width - sourceWidth, 0, sourceWidth, artwork.Height);
                var strip = new Rectangle(area.Right - stripWidth, 0, stripWidth, area.Height);
                e.Graphics.DrawImage(artwork, strip, source, GraphicsUnit.Pixel);
            }
        }

        private void FilterRoster(object? sender, EventArgs e)
        {
            string search = txtSearchStudent.Text.Trim();
            changingRosterFilter = true;

            try
            {
                string pattern = EscapeSearchText(search);
                roster.DefaultView.RowFilter = search.Length == 0
                    ? string.Empty
                    : $"[StudentId] LIKE '%{pattern}%' OR [StudentName] LIKE '%{pattern}%'";
                tablepanel.CurrentCell = null;
                tablepanel.ClearSelection();
                txtStudentId.Clear();
                txtName.Clear();
            }
            finally
            {
                changingRosterFilter = false;
            }

            UpdateRosterCount();
        }

        private static string EscapeSearchText(string search)
        {
            var escaped = new StringBuilder();
            foreach (char character in search)
            {
                switch (character)
                {
                    case '\'':
                        escaped.Append("''");
                        break;
                    case '[':
                    case ']':
                    case '%':
                    case '*':
                        escaped.Append('[').Append(character).Append(']');
                        break;
                    default:
                        escaped.Append(character);
                        break;
                }
            }

            return escaped.ToString();
        }

        private void UpdateRosterCount()
        {
            int shown = roster.DefaultView.Count;
            lblSearchCount.Text = string.IsNullOrEmpty(roster.DefaultView.RowFilter)
                ? $"{shown} {(shown == 1 ? "student" : "students")}"
                : shown == 0
                    ? "No matching students"
                    : $"{shown} of {roster.Rows.Count} students";
        }

        private void LoadFaculty()
        {
            byte[]? photo;

            using (var connection =
                new MySqlConnection(connectionString))
            {
                connection.Open();

                using var command = new MySqlCommand(@"
                    SELECT
                        f.full_name,
                        t.email,
                        f.profile_picture,
                        c.cohort_id,
                        c.year_block_program
                    FROM teacher_accounts t
                    INNER JOIN faculty_profiles f
                        ON f.account_id = t.account_id
                    INNER JOIN faculty_cohorts c
                        ON c.cohort_id = f.cohort_id
                    WHERE t.account_id = @id
                      AND t.is_active = TRUE;",
                    connection);

                command.Parameters.AddWithValue(
                    "@id", teacherAccountId);

                using var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    throw new InvalidOperationException(
                        "This professor has no assigned faculty profile " +
                        "or cohort, or the account is inactive.");
                }

                cohortId = Convert.ToInt32(reader["cohort_id"]);

                DisplayValue(
                    pnlName, reader["full_name"].ToString(), 18F);

                DisplayValue(
                    pnlEmail, reader["email"].ToString(), 11F);

                cohortName = reader.GetString(reader.GetOrdinal("year_block_program"));
                DisplayValue(pnlBlock, cohortName, 12F);

                photo = reader["profile_picture"] == DBNull.Value
                    ? null
                    : (byte[])reader["profile_picture"];
            }

            SetProfessorPhoto(photo);
        }

        private void LoadRoster()
        {
            using var connection =
                new MySqlConnection(connectionString);

            connection.Open();

            // Include automatically matched students unless dropped.
            // Also include students explicitly added to this cohort.
            using var command = new MySqlCommand(@"
                SELECT
                    a.account_id,
                    a.student_id,
                    COALESCE(
                        o.enrollment_status,
                        p.enrollment_status
                    ) AS enrollment_status,
                    CONCAT_WS(
                        ' ',
                        a.last_name,
                        CONCAT(
                            a.first_name,
                            IF(
                                p.middle_name IS NULL
                                OR p.middle_name = '',
                                '',
                                CONCAT(' ', p.middle_name)
                            )
                        )
                    ) AS student_name
                FROM student_accounts a
                INNER JOIN student_profiles p
                    ON p.account_id = a.account_id
                INNER JOIN faculty_cohorts c
                    ON c.cohort_id = @cohort
                LEFT JOIN faculty_roster_overrides o
                    ON o.cohort_id = c.cohort_id
                   AND o.student_account_id = a.account_id
                WHERE
                    o.is_included = TRUE
                    OR (
                        o.student_account_id IS NULL
                        AND p.year_block_program = c.year_block_program
                    )
                ORDER BY a.last_name, a.first_name, a.student_id;",
                connection);

            command.Parameters.AddWithValue("@cohort", cohortId);

            roster.Clear();
            originalStudents.Clear();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["account_id"]);

                roster.Rows.Add(
                    id,
                    reader["student_id"].ToString(),
                    reader["enrollment_status"].ToString(),
                    reader["student_name"].ToString());

                originalStudents.Add(id);
            }

            CaptureSavedEnrollmentCounts();
            hasChanges = false;
            tablepanel.ClearSelection();
            txtStudentId.Clear();
            txtName.Clear();
        }

        private void AddStudent(object? sender, EventArgs e)
        {
            string studentId = txtStudentId.Text.Trim();

            if (studentId.Length == 0 || cmbStatus.SelectedIndex < 0)
            {
                ShowError("Enter a Student ID and select a status.");
                return;
            }

            try
            {
                int id;
                string fullName;
                string databaseStudentId;

                using (var connection =
                    new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using var command = new MySqlCommand(@"
                        SELECT
                            a.account_id,
                            a.student_id,
                            CONCAT_WS(
                                ' ',
                                a.last_name,
                                a.first_name,
                                NULLIF(p.middle_name, '')
                            ) AS student_name
                        FROM student_accounts a
                        INNER JOIN student_profiles p
                            ON p.account_id = a.account_id
                        WHERE a.student_id = @studentId
                        LIMIT 1;",
                        connection);

                    command.Parameters.AddWithValue(
                        "@studentId", studentId);

                    using var reader = command.ExecuteReader();

                    if (!reader.Read())
                    {
                        ShowError(
                            "Student not found. The student must register " +
                            "and save their information first.");
                        return;
                    }

                    id = Convert.ToInt32(reader["account_id"]);
                    databaseStudentId =
                        reader["student_id"].ToString() ?? "";
                    fullName =
                        reader["student_name"].ToString() ?? "";
                }

                txtName.Text = fullName;

                DataRow? existing = roster.Rows.Find(id);
                string status = cmbStatus.SelectedItem!.ToString()!;

                if (existing != null)
                {
                    existing["Status"] = status;
                    existing["StudentId"] = databaseStudentId;
                    existing["StudentName"] = fullName;
                }
                else
                {
                    roster.Rows.Add(
                        id, databaseStudentId, status, fullName);
                }

                hasChanges = true;
            }
            catch (MySqlException ex)
            {
                ShowError("Unable to find the student.\n\n" + ex.Message);
            }
        }

        private void DropStudent(object? sender, EventArgs e)
        {
            string studentId = txtStudentId.Text.Trim();
            DataRow? target = null;

            if (studentId.Length > 0)
            {
                foreach (DataRow row in roster.Rows)
                {
                    if (string.Equals(
                        row["StudentId"].ToString(),
                        studentId,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        target = row;
                        break;
                    }
                }
            }
            else if (tablepanel.CurrentRow?.DataBoundItem
                is DataRowView selected)
            {
                target = selected.Row;
            }

            if (target == null)
            {
                ShowError("Enter a listed Student ID or select a student.");
                return;
            }

            roster.Rows.Remove(target);
            hasChanges = true;

            txtStudentId.Clear();
            txtName.Clear();
            tablepanel.ClearSelection();
        }

        private void RosterSelectionChanged(
            object? sender, EventArgs e)
        {
            if (changingRosterFilter ||
                tablepanel.CurrentRow is not { Selected: true, DataBoundItem: DataRowView selected })
            {
                return;
            }

            txtStudentId.Text = selected["StudentId"].ToString();
            txtName.Text = selected["StudentName"].ToString();
            cmbStatus.SelectedItem = selected["Status"].ToString();
        }

        private void SaveChanges(object? sender, EventArgs e)
        {
            if (!hasChanges)
            {
                MessageBox.Show(this, "There are no changes to save.");
                return;
            }

            try
            {
                using var connection =
                    new MySqlConnection(connectionString);

                connection.Open();

                using var transaction = connection.BeginTransaction();

                // Recheck the authenticated teacher's assignment.
                using (var check = new MySqlCommand(@"
                    SELECT f.cohort_id
                    FROM faculty_profiles f
                    INNER JOIN teacher_accounts t
                        ON t.account_id = f.account_id
                    WHERE f.account_id = @teacher
                      AND t.is_active = TRUE
                    FOR UPDATE;",
                    connection, transaction))
                {
                    check.Parameters.AddWithValue(
                        "@teacher", teacherAccountId);

                    object? assigned = check.ExecuteScalar();

                    if (assigned == null ||
                        Convert.ToInt32(assigned) != cohortId)
                    {
                        throw new InvalidOperationException(
                            "Your cohort assignment has changed. " +
                            "Reopen the faculty profile.");
                    }
                }

                HashSet<int> current = new HashSet<int>();

                foreach (DataRow row in roster.Rows)
                {
                    int id = Convert.ToInt32(row["AccountId"]);
                    current.Add(id);

                    SaveOverride(
                        connection,
                        transaction,
                        id,
                        true,
                        row["Status"].ToString()!);
                }

                foreach (int id in originalStudents)
                {
                    if (!current.Contains(id))
                    {
                        SaveOverride(
                            connection,
                            transaction,
                            id,
                            false,
                            "Regular");
                    }
                }

                transaction.Commit();
                CaptureSavedEnrollmentCounts();

                originalStudents.Clear();

                foreach (int id in current)
                {
                    originalStudents.Add(id);
                }

                hasChanges = false;

                MessageBox.Show(
                    this,
                    "Roster changes have been saved.",
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                ShowError("Unable to save the roster.\n\n" + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ShowError(ex.Message);
            }
        }

        private void CaptureSavedEnrollmentCounts()
        {
            Array.Clear(savedEnrollmentCounts, 0, savedEnrollmentCounts.Length);

            foreach (DataRow row in roster.Rows)
            {
                switch (row["Status"] as string)
                {
                    case "Regular":
                        savedEnrollmentCounts[0]++;
                        break;
                    case "Irregular":
                        savedEnrollmentCounts[1]++;
                        break;
                    case "LOA":
                        savedEnrollmentCounts[2]++;
                        break;
                }
            }
        }

        private void ShowEnrollmentChart(object? sender, EventArgs e)
        {
            using var chartWindow = new Form
            {
                Text = "Enrollment Chart",
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(800, 450),
                MinimumSize = new Size(520, 340)
            };

            var chart = new FormsPlot { Dock = DockStyle.Fill };
            double[] positions = { 0, 1, 2 };
            ScottPlot.Tick[] labels =
            {
                new ScottPlot.Tick(0, "Regular"),
                new ScottPlot.Tick(1, "Irregular"),
                new ScottPlot.Tick(2, "LOA")
            };

            var plot = chart.Plot;
            plot.Add.Bars(positions, savedEnrollmentCounts);
            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(labels);

            double highestCount = Math.Max(
                savedEnrollmentCounts[0],
                Math.Max(savedEnrollmentCounts[1], savedEnrollmentCounts[2]));
            plot.Axes.SetLimits(-0.6, 2.6, 0, Math.Max(1, Math.Ceiling(highestCount * 1.15)));
            plot.Title(cohortName);
            plot.YLabel("Number of Students");

            var summary = new Label
            {
                Dock = DockStyle.Top,
                Height = 36,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = $"Saved roster — Regular: {savedEnrollmentCounts[0]}   " +
                       $"Irregular: {savedEnrollmentCounts[1]}   LOA: {savedEnrollmentCounts[2]}"
            };

            chartWindow.Controls.Add(chart);
            chartWindow.Controls.Add(summary);
            chartWindow.ShowDialog(this);
        }

        private void SaveOverride(
            MySqlConnection connection,
            MySqlTransaction transaction,
            int studentAccountId,
            bool included,
            string status)
        {
            using var command = new MySqlCommand(@"
                INSERT INTO faculty_roster_overrides
                (
                    cohort_id,
                    student_account_id,
                    is_included,
                    enrollment_status
                )
                VALUES (@cohort, @student, @included, @status)
                ON DUPLICATE KEY UPDATE
                    is_included = @included,
                    enrollment_status = @status;",
                connection, transaction);

            command.Parameters.AddWithValue("@cohort", cohortId);
            command.Parameters.AddWithValue(
                "@student", studentAccountId);
            command.Parameters.AddWithValue("@included", included);
            command.Parameters.AddWithValue("@status", status);

            command.ExecuteNonQuery();
        }

        private static void DisplayValue(
            Control panel,
            string? value,
            float fontSize)
        {
            string name = panel.Name + "_Value";
            Label? label = panel.Controls[name] as Label;

            if (label == null)
            {
                label = new Label
                {
                    Name = name,
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    AutoEllipsis = true,
                    UseMnemonic = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    BackColor = Color.Transparent,
                    ForeColor = panel.ForeColor,
                    Padding = new Padding(4, 0, 4, 0)
                };

                panel.Controls.Add(label);
            }

            label.Font = new Font("Segoe UI", fontSize);
            label.Text = value ?? "";
            label.BringToFront();
        }

        private void SetProfessorPhoto(byte[]? bytes)
        {
            Image? previous = picProfessor.Image;
            picProfessor.Image = null;
            previous?.Dispose();

            if (bytes == null || bytes.Length == 0)
            {
                return;
            }

            try
            {
                using var stream = new MemoryStream(bytes);
                using Image original = Image.FromStream(stream);

                picProfessor.Image = new Bitmap(original);
            }
            catch (Exception ex) when (
                ex is ArgumentException ||
                ex is OutOfMemoryException ||
                ex is System.Runtime.InteropServices.ExternalException)
            {
                ShowError(
                    "The professor's saved picture could not be displayed.");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (hasChanges)
            {
                DialogResult answer = MessageBox.Show(
                    this,
                    "Discard your unsaved roster changes?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (answer != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }

            base.OnFormClosing(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Image? previous = picProfessor.Image;
            picProfessor.Image = null;
            previous?.Dispose();

            base.OnFormClosed(e);
        }

        private void ShowError(string message)
        {
            MessageBox.Show(
                this,
                message,
                "Faculty Profile",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
