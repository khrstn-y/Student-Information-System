#nullable enable

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BUCS_SIS
{
    public partial class FacultyProfileForm : Form
    {
        private readonly int teacherAccountId;

        private readonly string connectionString =
            "server=localhost;port=3306;database=student_db;" +
            "uid=root;password=@Bok15518;charset=utf8mb4;";

        private readonly DataTable roster = new DataTable();

        private readonly Dictionary<string, RosterChange> pending =
            new Dictionary<string, RosterChange>(
                StringComparer.OrdinalIgnoreCase);

        private int cohortId;
        private bool loaded;
        private bool suppressSelection;

        private sealed class RosterChange
        {
            public string StudentId { get; set; } = "";
            public string FullName { get; set; } = "";
            public string Status { get; set; } = "Regular";
            public bool Included { get; set; }
        }

        // Required by the Visual Studio Designer.
        public FacultyProfileForm()
        {
            InitializeComponent();

            ConfigureUploadButton();
        }

        // Open this constructor after successful teacher login.
        public FacultyProfileForm(int authenticatedTeacherId)
            : this()
        {
            if (authenticatedTeacherId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(authenticatedTeacherId));
            }

            teacherAccountId = authenticatedTeacherId;
        }

        private void ConfigureUploadButton()
        {
            // Reuse an existing button if one was added in the Designer.
            Control[] matches = Controls.Find("btnUploadPhoto", true);

            Button upload;

            if (matches.Length == 1 && matches[0] is Button existing)
            {
                upload = existing;
            }
            else if (matches.Length == 0)
            {
                upload = new Button
                {
                    Name = "btnUploadPhoto",
                    Size = new Size(105, 42),
                    Location = new Point(
                        picProfessor.Right + 8,
                        picProfessor.Top + 28),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    FlatStyle = FlatStyle.Flat,
                    UseVisualStyleBackColor = false
                };

                Controls.Add(upload);
            }
            else
            {
                throw new InvalidOperationException(
                    "Use only one Button named btnUploadPhoto.");
            }

            upload.Text = "Upload Picture";
            upload.Click -= UploadProfessorPhoto;
            upload.Click += UploadProfessorPhoto;
            upload.BringToFront();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (loaded)
                return;

            loaded = true;

            if (teacherAccountId <= 0)
            {
                ShowError("Please log in through the faculty portal.");
                Close();
                return;
            }

            try
            {
                ConfigureControls();
                LoadFaculty();
                LoadRoster();
            }
            catch (Exception ex) when (
                ex is MySqlException ||
                ex is InvalidOperationException)
            {
                ShowError(
                    "Unable to load faculty information.\n\n" +
                    ex.Message);

                Close();
            }
        }

        private void ConfigureControls()
        {
            Text = "FACULTY PROFILE";

            picProfessor.SizeMode = PictureBoxSizeMode.Zoom;

            txtStudentId.MaxLength = 50;
            txtStudentId.PlaceholderText = "Student ID...";

            txtName.ReadOnly = false;
            txtName.TabStop = true;
            txtName.MaxLength = 300;
            txtName.BackColor = Color.White;
            txtName.PlaceholderText = "Full name for a new student...";

            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(
                new object[] { "Regular", "Irregular", "LOA" });

            cmbStatus.SelectedIndex = 0;

            btnAdd.Text = "Add Student";
            btnDrop.Text = "Drop Student";
            btnSave.Text = "Save Changes";

            roster.CaseSensitive = false;
            roster.Columns.Add("StudentId", typeof(string));
            roster.Columns.Add("Status", typeof(string));
            roster.Columns.Add("StudentName", typeof(string));

            roster.PrimaryKey = new[]
            {
                roster.Columns["StudentId"]!
            };

            tablepanel.AutoGenerateColumns = false;
            tablepanel.Columns.Clear();

            tablepanel.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStudentId",
                HeaderText = "Student ID",
                DataPropertyName = "StudentId",
                FillWeight = 27,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            tablepanel.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Enrollment Status",
                DataPropertyName = "Status",
                FillWeight = 25,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            tablepanel.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStudentName",
                HeaderText = "Student Name",
                DataPropertyName = "StudentName",
                FillWeight = 48,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

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

            tablepanel.Font = new Font("Segoe UI", 10F);
            tablepanel.DefaultCellStyle.ForeColor = Color.Black;
            tablepanel.DefaultCellStyle.BackColor = Color.White;
            tablepanel.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            int rowHeight = (int)Math.Round(28 * DeviceDpi / 96.0);
            int headerHeight = (int)Math.Round(34 * DeviceDpi / 96.0);

            tablepanel.RowTemplate.Height = rowHeight;
            tablepanel.ColumnHeadersHeight = headerHeight;
            tablepanel.Height = headerHeight + 15 * rowHeight + 2;
            tablepanel.ScrollBars = ScrollBars.Vertical;

            tablepanel.DataSource = roster;

            btnAdd.Click += AddStudent;
            btnDrop.Click += DropStudent;
            btnSave.Click += SaveChanges;
            btnBack.Click += BackToPrevious;

            tablepanel.SelectionChanged += RosterSelectionChanged;

            CancelButton = btnBack;
        }

        private void LoadFaculty()
        {
            using var connection =
                new MySqlConnection(connectionString);

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
                WHERE t.account_id = @teacher
                  AND t.is_active = TRUE;",
                connection);

            command.Parameters.AddWithValue(
                "@teacher", teacherAccountId);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                throw new InvalidOperationException(
                    "The professor is inactive or has no assigned cohort.");
            }

            cohortId = Convert.ToInt32(reader["cohort_id"]);

            DisplayValue(
                pnlName,
                reader["full_name"].ToString(),
                18F);

            DisplayValue(
                pnlEmail,
                reader["email"].ToString(),
                11F);

            DisplayValue(
                pnlBlock,
                reader["year_block_program"].ToString(),
                12F);

            byte[]? photo =
                reader["profile_picture"] == DBNull.Value
                    ? null
                    : (byte[])reader["profile_picture"];

            SetProfessorPhoto(photo);
        }

        private void LoadRoster()
        {
            using var connection =
                new MySqlConnection(connectionString);

            connection.Open();

            // Registered account names take priority over manual names.
            // Matching Student IDs appear only once.
            using var command = new MySqlCommand(@"
                SELECT
                    a.student_id,
                    COALESCE(
                        o.enrollment_status,
                        m.enrollment_status,
                        p.enrollment_status
                    ) AS enrollment_status,
                    CONCAT(
                        a.last_name, ', ', a.first_name,
                        CASE
                            WHEN p.middle_name IS NULL
                                 OR p.middle_name = ''
                            THEN ''
                            ELSE CONCAT(' ', p.middle_name)
                        END
                    ) AS student_name
                FROM student_accounts a
                CROSS JOIN faculty_cohorts c
                LEFT JOIN student_profiles p
                    ON p.account_id = a.account_id
                LEFT JOIN faculty_roster_overrides o
                    ON o.cohort_id = c.cohort_id
                   AND o.student_account_id = a.account_id
                LEFT JOIN faculty_manual_students m
                    ON m.cohort_id = c.cohort_id
                   AND m.student_id = a.student_id
                WHERE c.cohort_id = @cohort
                  AND (
                      o.is_included = TRUE
                      OR (
                          o.student_account_id IS NULL
                          AND (
                              m.is_included = TRUE
                              OR (
                                  m.student_id IS NULL
                                  AND p.year_block_program =
                                      c.year_block_program
                              )
                          )
                      )
                  )

                UNION ALL

                SELECT
                    m.student_id,
                    m.enrollment_status,
                    m.full_name AS student_name
                FROM faculty_manual_students m
                WHERE m.cohort_id = @cohort
                  AND m.is_included = TRUE
                  AND NOT EXISTS (
                      SELECT 1
                      FROM student_accounts a
                      WHERE a.student_id = m.student_id
                  )
                ORDER BY student_name, student_id;",
                connection);

            command.Parameters.AddWithValue("@cohort", cohortId);

            DataTable incoming = roster.Clone();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    incoming.Rows.Add(
                        reader["student_id"].ToString(),
                        reader["enrollment_status"].ToString(),
                        reader["student_name"].ToString());
                }
            }

            suppressSelection = true;

            try
            {
                roster.Clear();

                foreach (DataRow row in incoming.Rows)
                {
                    roster.ImportRow(row);
                }

                pending.Clear();
                ResetInputs();
            }
            finally
            {
                suppressSelection = false;
            }
        }

        private void AddStudent(object? sender, EventArgs e)
        {
            string studentId = txtStudentId.Text.Trim();
            string fullName = txtName.Text.Trim();
            string status = cmbStatus.SelectedItem?.ToString() ?? "";

            if (studentId.Length == 0 || status.Length == 0)
            {
                ShowError("Enter a Student ID and select a status.");
                return;
            }

            try
            {
                using var connection =
                    new MySqlConnection(connectionString);

                connection.Open();

                // Retrieve the official name if an account exists.
                using var command = new MySqlCommand(@"
                    SELECT
                        a.student_id,
                        CONCAT(
                            a.last_name, ', ', a.first_name,
                            CASE
                                WHEN p.middle_name IS NULL
                                     OR p.middle_name = ''
                                THEN ''
                                ELSE CONCAT(' ', p.middle_name)
                            END
                        ) AS student_name
                    FROM student_accounts a
                    LEFT JOIN student_profiles p
                        ON p.account_id = a.account_id
                    WHERE a.student_id = @studentId;",
                    connection);

                command.Parameters.AddWithValue(
                    "@studentId", studentId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        studentId =
                            reader["student_id"].ToString() ?? studentId;

                        fullName =
                            reader["student_name"].ToString() ?? "";
                    }
                }

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    ShowError(
                        "This Student ID has no account yet. " +
                        "Type the student's full name, then click Add Student.");

                    txtName.Focus();
                    return;
                }

                suppressSelection = true;

                try
                {
                    DataRow? existing = roster.Rows.Find(studentId);

                    if (existing == null)
                    {
                        roster.Rows.Add(
                            studentId,
                            status,
                            fullName);
                    }
                    else
                    {
                        existing["Status"] = status;
                        existing["StudentName"] = fullName;
                    }

                    pending[studentId] = new RosterChange
                    {
                        StudentId = studentId,
                        FullName = fullName,
                        Status = status,
                        Included = true
                    };

                    ResetInputs();
                }
                finally
                {
                    suppressSelection = false;
                }

                txtStudentId.Focus();
            }
            catch (MySqlException ex)
            {
                ShowError(
                    "Unable to add the student.\n\n" + ex.Message);
            }
        }

        private void DropStudent(object? sender, EventArgs e)
        {
            string studentId = txtStudentId.Text.Trim();
            DataRow? target = null;

            if (studentId.Length > 0)
            {
                target = roster.Rows.Find(studentId);
            }
            else if (tablepanel.CurrentRow?.DataBoundItem
                is DataRowView selected)
            {
                target = selected.Row;
            }

            if (target == null)
            {
                ShowError(
                    "Enter a listed Student ID or select a student.");

                return;
            }

            studentId = target["StudentId"].ToString()!;

            var change = new RosterChange
            {
                StudentId = studentId,
                FullName = target["StudentName"].ToString() ?? "",
                Status = target["Status"].ToString() ?? "Regular",
                Included = false
            };

            suppressSelection = true;

            try
            {
                roster.Rows.Remove(target);
                pending[studentId] = change;
                ResetInputs();
            }
            finally
            {
                suppressSelection = false;
            }
        }

        private void RosterSelectionChanged(
            object? sender,
            EventArgs e)
        {
            if (suppressSelection)
                return;

            if (tablepanel.CurrentRow?.DataBoundItem
                is not DataRowView selected)
            {
                return;
            }

            txtStudentId.Text = selected["StudentId"].ToString();
            txtName.Text = selected["StudentName"].ToString();
            cmbStatus.SelectedItem = selected["Status"].ToString();
        }

        private void ResetInputs()
        {
            tablepanel.ClearSelection();
            tablepanel.CurrentCell = null;

            txtStudentId.Clear();
            txtName.Clear();
            cmbStatus.SelectedIndex = 0;
        }

        private void SaveChanges(object? sender, EventArgs e)
        {
            if (pending.Count == 0)
            {
                MessageBox.Show(
                    this,
                    "There are no changes to save.");

                return;
            }

            try
            {
                using var connection =
                    new MySqlConnection(connectionString);

                connection.Open();

                using var transaction =
                    connection.BeginTransaction();

                using (var check = new MySqlCommand(@"
                    SELECT f.cohort_id
                    FROM faculty_profiles f
                    INNER JOIN teacher_accounts t
                        ON t.account_id = f.account_id
                    WHERE f.account_id = @teacher
                      AND t.is_active = TRUE
                    FOR UPDATE;",
                    connection,
                    transaction))
                {
                    check.Parameters.AddWithValue(
                        "@teacher", teacherAccountId);

                    object? assigned = check.ExecuteScalar();

                    if (assigned == null ||
                        Convert.ToInt32(assigned) != cohortId)
                    {
                        throw new InvalidOperationException(
                            "Your cohort assignment changed or your " +
                            "account is inactive. Reopen the faculty portal.");
                    }
                }

                using (var cohortLock = new MySqlCommand(@"
                    SELECT cohort_id
                    FROM faculty_cohorts
                    WHERE cohort_id = @cohort
                    FOR UPDATE;",
                    connection,
                    transaction))
                {
                    cohortLock.Parameters.AddWithValue(
                        "@cohort", cohortId);

                    if (cohortLock.ExecuteScalar() == null)
                    {
                        throw new InvalidOperationException(
                            "The assigned cohort no longer exists.");
                    }
                }

                foreach (RosterChange change in pending.Values)
                {
                    SaveRosterChange(
                        connection,
                        transaction,
                        change);
                }

                transaction.Commit();
                pending.Clear();
            }
            catch (Exception ex) when (
                ex is MySqlException ||
                ex is InvalidOperationException)
            {
                ShowError(
                    "Changes were not saved.\n\n" + ex.Message);

                return;
            }

            MessageBox.Show(
                this,
                "Student roster changes have been saved.",
                "Saved",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            try
            {
                LoadRoster();
            }
            catch (MySqlException ex)
            {
                ShowError(
                    "Changes were saved, but the roster could not " +
                    "be refreshed. Reopen this window.\n\n" +
                    ex.Message);
            }
        }

        private void SaveRosterChange(
            MySqlConnection connection,
            MySqlTransaction transaction,
            RosterChange change)
        {
            int? studentAccountId = null;

            // Recheck because the student may have signed up
            // after the professor clicked Add Student.
            using (var lookup = new MySqlCommand(@"
                SELECT account_id
                FROM student_accounts
                WHERE student_id = @studentId
                FOR UPDATE;",
                connection,
                transaction))
            {
                lookup.Parameters.AddWithValue(
                    "@studentId", change.StudentId);

                object? result = lookup.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    studentAccountId = Convert.ToInt32(result);
                }
            }

            if (studentAccountId.HasValue)
            {
                using var command = new MySqlCommand(@"
                    INSERT INTO faculty_roster_overrides
                    (
                        cohort_id,
                        student_account_id,
                        is_included,
                        enrollment_status
                    )
                    VALUES (@cohort, @account, @included, @status)
                    ON DUPLICATE KEY UPDATE
                        is_included = @included,
                        enrollment_status = @status;",
                    connection,
                    transaction);

                command.Parameters.AddWithValue(
                    "@cohort", cohortId);

                command.Parameters.AddWithValue(
                    "@account", studentAccountId.Value);

                command.Parameters.AddWithValue(
                    "@included", change.Included);

                command.Parameters.AddWithValue(
                    "@status", change.Status);

                command.ExecuteNonQuery();

                using var updateManual = new MySqlCommand(@"
                    UPDATE faculty_manual_students
                    SET is_included = @included,
                        enrollment_status = @status
                    WHERE cohort_id = @cohort
                      AND student_id = @studentId;",
                    connection,
                    transaction);

                updateManual.Parameters.AddWithValue(
                    "@cohort", cohortId);

                updateManual.Parameters.AddWithValue(
                    "@studentId", change.StudentId);

                updateManual.Parameters.AddWithValue(
                    "@included", change.Included);

                updateManual.Parameters.AddWithValue(
                    "@status", change.Status);

                updateManual.ExecuteNonQuery();
            }
            else
            {
                // A manual student record does not create a login account.
                using var command = new MySqlCommand(@"
                    INSERT INTO faculty_manual_students
                    (
                        cohort_id,
                        student_id,
                        full_name,
                        enrollment_status,
                        is_included
                    )
                    VALUES
                    (
                        @cohort,
                        @studentId,
                        @name,
                        @status,
                        @included
                    )
                    ON DUPLICATE KEY UPDATE
                        full_name = @name,
                        enrollment_status = @status,
                        is_included = @included;",
                    connection,
                    transaction);

                command.Parameters.AddWithValue(
                    "@cohort", cohortId);

                command.Parameters.AddWithValue(
                    "@studentId", change.StudentId);

                command.Parameters.AddWithValue(
                    "@name", change.FullName);

                command.Parameters.AddWithValue(
                    "@status", change.Status);

                command.Parameters.AddWithValue(
                    "@included", change.Included);

                command.ExecuteNonQuery();
            }
        }

        private void UploadProfessorPhoto(
            object? sender,
            EventArgs e)
        {
            if (teacherAccountId <= 0)
            {
                ShowError("Please log in first.");
                return;
            }

            using OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select Professor Picture",
                Filter = "Image Files|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            byte[] pictureBytes;
            Bitmap? preview = null;

            try
            {
                const long maximumSize = 5 * 1024 * 1024;

                if (new FileInfo(dialog.FileName).Length > maximumSize)
                {
                    ShowError(
                        "Choose a picture no larger than 5 MB.");

                    return;
                }

                using (Image original =
                    Image.FromFile(dialog.FileName))
                using (Bitmap square = new Bitmap(600, 600))
                {
                    // 600 pixels at 300 DPI = 2 inches.
                    square.SetResolution(300, 300);

                    int side = Math.Min(
                        original.Width,
                        original.Height);

                    Rectangle source = new Rectangle(
                        (original.Width - side) / 2,
                        (original.Height - side) / 2,
                        side,
                        side);

                    using (Graphics graphics =
                        Graphics.FromImage(square))
                    {
                        graphics.Clear(Color.White);

                        graphics.InterpolationMode =
                            InterpolationMode.HighQualityBicubic;

                        graphics.DrawImage(
                            original,
                            new Rectangle(0, 0, 600, 600),
                            source,
                            GraphicsUnit.Pixel);
                    }

                    using MemoryStream stream = new MemoryStream();

                    square.Save(stream, ImageFormat.Jpeg);
                    pictureBytes = stream.ToArray();

                    preview = new Bitmap(square);
                }
            }
            catch (Exception ex) when (IsPictureError(ex))
            {
                preview?.Dispose();

                ShowError(
                    "Unable to open this picture. " +
                    "Choose a valid JPG or PNG.");

                return;
            }

            try
            {
                using var connection =
                    new MySqlConnection(connectionString);

                connection.Open();

                using var transaction =
                    connection.BeginTransaction();

                using (var check = new MySqlCommand(@"
                    SELECT f.account_id
                    FROM faculty_profiles f
                    INNER JOIN teacher_accounts t
                        ON t.account_id = f.account_id
                    WHERE f.account_id = @teacher
                      AND t.is_active = TRUE
                    FOR UPDATE;",
                    connection,
                    transaction))
                {
                    check.Parameters.AddWithValue(
                        "@teacher", teacherAccountId);

                    if (check.ExecuteScalar() == null)
                    {
                        throw new InvalidOperationException(
                            "Your faculty profile was not found " +
                            "or your account is inactive.");
                    }
                }

                using (var command = new MySqlCommand(@"
                    UPDATE faculty_profiles
                    SET profile_picture = @photo
                    WHERE account_id = @teacher;",
                    connection,
                    transaction))
                {
                    command.Parameters.AddWithValue(
                        "@teacher", teacherAccountId);

                    command.Parameters.Add(
                        "@photo",
                        MySqlDbType.MediumBlob).Value = pictureBytes;

                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (Exception ex) when (
                ex is MySqlException ||
                ex is InvalidOperationException)
            {
                preview?.Dispose();

                ShowError(
                    "Unable to save your picture.\n\n" + ex.Message);

                return;
            }

            // Display only after the database save succeeds.
            Image? previous = picProfessor.Image;
            picProfessor.Image = preview;
            previous?.Dispose();

            MessageBox.Show(
                this,
                "Your profile picture has been saved.",
                "Picture Updated",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private static bool IsPictureError(Exception ex)
        {
            return ex is ArgumentException ||
                   ex is OutOfMemoryException ||
                   ex is IOException ||
                   ex is UnauthorizedAccessException ||
                   ex is System.Runtime.InteropServices.ExternalException;
        }

        private static void DisplayValue(
            Control panel,
            string? value,
            float fontSize)
        {
            string labelName = panel.Name + "_Value";
            Label? label = panel.Controls[labelName] as Label;

            if (label == null)
            {
                label = new Label
                {
                    Name = labelName,
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    AutoEllipsis = true,
                    UseMnemonic = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    BackColor = Color.Transparent,
                    ForeColor = panel.ForeColor,
                    Font = new Font("Segoe UI", fontSize),
                    Padding = new Padding(4, 0, 4, 0)
                };

                panel.Controls.Add(label);
            }

            label.Text = value ?? "";
            label.BringToFront();
        }

        private void SetProfessorPhoto(byte[]? bytes)
        {
            Image? previous = picProfessor.Image;
            picProfessor.Image = null;
            previous?.Dispose();

            if (bytes == null || bytes.Length == 0)
                return;

            try
            {
                using var stream = new MemoryStream(bytes);
                using Image original = Image.FromStream(stream);

                picProfessor.Image = new Bitmap(original);
            }
            catch (Exception ex) when (IsPictureError(ex))
            {
                ShowError(
                    "The professor's saved picture could not be displayed. " +
                    "Use Upload Picture to replace it.");
            }
        }

        private void BackToPrevious(object? sender, EventArgs e)
        {
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.Cancel || pending.Count == 0)
                return;

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