#nullable enable

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;

namespace BUCS_SIS
{
    public partial class StudentProfileUpdateForm : Form
    {
        private readonly int accountId;

        private byte[]? photoBytes;

        // Replace this with your MySQL password.
        private readonly string connectionString =
            "server=localhost;port=3306;database=student_db;" +
            "uid=root;password=YOUR_MYSQL_PASSWORD;charset=utf8mb4;";

        // Required for the Visual Studio Designer.
        public StudentProfileUpdateForm()
        {
            InitializeComponent();
            ConfigureForm();
        }

        // Use this constructor after successful student authentication.
        public StudentProfileUpdateForm(int authenticatedAccountId)
            : this()
        {
            if (authenticatedAccountId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(authenticatedAccountId));
            }

            accountId = authenticatedAccountId;
        }

        private void ConfigureForm()
        {
            Text = "Student Information";
            StartPosition = FormStartPosition.CenterParent;

            txtStudentId.MaxLength = 50;
            txtFirstName.MaxLength = 100;
            txtMiddleName.MaxLength = 100;
            txtLastName.MaxLength = 100;
            txtEmail.MaxLength = 254;
            txtYearBlockProgram.MaxLength = 216;
            txtContact.MaxLength = 30;

            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[]
            {
                "Regular",
                "Irregular",
                "LOA"
            });

            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.Items.Clear();
            cmbGender.Items.AddRange(new object[]
            {
                "Male",
                "Female",
                "Other",
                "Prefer not to say"
            });

            dtpBirth.Format = DateTimePickerFormat.Short;
            dtpBirth.MaxDate = DateTime.Today;
            dtpBirth.ShowCheckBox = true;
            dtpBirth.Checked = false;

            picStudentPhoto.SizeMode = PictureBoxSizeMode.Zoom;

            btnSave.Text = "Save Student";
            btnClear.Text = "Clear Fields";
            btnView.Text = "View Your Profile";
            btnBack.Text = "Back";
            btnUploadPhoto.Text = "Upload 2×2 Picture";

            btnSave.Click += SaveStudent;
            btnClear.Click += ClearFields;
            btnView.Click += ViewProfile;
            btnUploadPhoto.Click += UploadPhoto;
            btnBack.Click += BackToPrevious;

            Shown += FormShown;
            FormClosed += ReleasePhoto;

            AcceptButton = btnSave;
            CancelButton = btnBack;
        }

        private void FormShown(object? sender, EventArgs e)
        {
            if (accountId <= 0)
            {
                MessageBox.Show(
                    this,
                    "Please log in first. This window requires " +
                    "the authenticated student's account ID.",
                    "Login Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Close();
                return;
            }

            LoadStudent();
        }

        private void LoadStudent()
        {
            try
            {
                using var connection =
                    new MySqlConnection(connectionString);

                connection.Open();

                const string sql = @"
                    SELECT
                        a.student_id,
                        a.first_name,
                        a.last_name,
                        a.email,
                        p.account_id AS profile_id,
                        p.middle_name,
                        p.year_block_program,
                        p.enrollment_status,
                        p.date_of_birth,
                        p.gender,
                        p.contact_number,
                        p.profile_picture
                    FROM student_accounts a
                    LEFT JOIN student_profiles p
                        ON p.account_id = a.account_id
                    WHERE a.account_id = @id;";

                using var command =
                    new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue("@id", accountId);

                using var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    MessageBox.Show(
                        this,
                        "Student account not found.",
                        "Account Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    Close();
                    return;
                }

                txtStudentId.Text = reader["student_id"].ToString();
                txtFirstName.Text = reader["first_name"].ToString();
                txtLastName.Text = reader["last_name"].ToString();
                txtEmail.Text = reader["email"].ToString();

                if (reader["profile_id"] == DBNull.Value)
                {
                    MessageBox.Show(
                        this,
                        "Complete your information and click Save Student.",
                        "Complete Your Profile",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                txtMiddleName.Text =
                    reader["middle_name"].ToString();

                txtYearBlockProgram.Text =
                    reader["year_block_program"].ToString();

                cmbStatus.SelectedItem =
                    reader["enrollment_status"].ToString();

                dtpBirth.Value =
                    Convert.ToDateTime(reader["date_of_birth"]);

                dtpBirth.Checked = true;

                cmbGender.SelectedItem =
                    reader["gender"].ToString();

                txtContact.Text =
                    reader["contact_number"].ToString();

                SetPhoto(
                    reader["profile_picture"] == DBNull.Value
                        ? null
                        : (byte[])reader["profile_picture"]);
            }
            catch (MySqlException)
            {
                ShowError(
                    "Unable to load your profile. Check your MySQL " +
                    "connection and make sure year_block_program " +
                    "and profile_picture exist in student_profiles.");

                Close();
            }
            catch (ArgumentException)
            {
                ShowError(
                    "The saved date of birth or picture is invalid.");

                Close();
            }
        }

        private bool ValidateInputs()
        {
            foreach (TextBox input in new[]
            {
                txtStudentId,
                txtFirstName,
                txtLastName,
                txtEmail,
                txtYearBlockProgram,
                txtContact
            })
            {
                if (string.IsNullOrWhiteSpace(input.Text))
                {
                    MessageBox.Show(
                        this,
                        "Complete all required fields. " +
                        "Middle name and picture are optional.",
                        "Missing Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    input.Focus();
                    return false;
                }
            }

            string email = txtEmail.Text.Trim();

            if (!MailAddress.TryCreate(email, out MailAddress? address) ||
                !string.Equals(
                    address.Address,
                    email,
                    StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    this,
                    "Enter a valid email address.",
                    "Invalid Email",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtEmail.Focus();
                return false;
            }

            if (cmbStatus.SelectedIndex < 0 ||
                cmbGender.SelectedIndex < 0)
            {
                MessageBox.Show(
                    this,
                    "Select your enrollment status and gender.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (!dtpBirth.Checked ||
                dtpBirth.Value.Date > DateTime.Today)
            {
                MessageBox.Show(
                    this,
                    "Select a valid date of birth.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                dtpBirth.Focus();
                return false;
            }

            return true;
        }

        private void SaveStudent(object? sender, EventArgs e)
        {
            if (accountId <= 0 || !ValidateInputs())
            {
                return;
            }

            try
            {
                using var connection =
                    new MySqlConnection(connectionString);

                connection.Open();

                using var transaction =
                    connection.BeginTransaction();

                // Confirm and lock the current student's account.
                using (var check = new MySqlCommand(@"
                    SELECT account_id
                    FROM student_accounts
                    WHERE account_id = @id
                    FOR UPDATE;",
                    connection,
                    transaction))
                {
                    check.Parameters.AddWithValue("@id", accountId);

                    if (check.ExecuteScalar() == null)
                    {
                        MessageBox.Show(
                            this,
                            "The student account no longer exists.");

                        return;
                    }
                }

                const string accountSql = @"
                    UPDATE student_accounts
                    SET student_id = @studentId,
                        first_name = @firstName,
                        last_name = @lastName,
                        email = @email
                    WHERE account_id = @id;";

                using (var command = new MySqlCommand(
                    accountSql,
                    connection,
                    transaction))
                {
                    command.Parameters.AddWithValue("@id", accountId);

                    command.Parameters.AddWithValue(
                        "@studentId", txtStudentId.Text.Trim());

                    command.Parameters.AddWithValue(
                        "@firstName", txtFirstName.Text.Trim());

                    command.Parameters.AddWithValue(
                        "@lastName", txtLastName.Text.Trim());

                    command.Parameters.AddWithValue(
                        "@email", txtEmail.Text.Trim());

                    command.ExecuteNonQuery();
                }

                const string profileSql = @"
                    INSERT INTO student_profiles
                    (
                        account_id,
                        middle_name,
                        year_block_program,
                        enrollment_status,
                        date_of_birth,
                        gender,
                        contact_number,
                        profile_picture
                    )
                    VALUES
                    (
                        @id,
                        @middle,
                        @yearBlockProgram,
                        @status,
                        @birth,
                        @gender,
                        @contact,
                        @photo
                    )
                    ON DUPLICATE KEY UPDATE
                        middle_name = @middle,
                        year_block_program = @yearBlockProgram,
                        enrollment_status = @status,
                        date_of_birth = @birth,
                        gender = @gender,
                        contact_number = @contact,
                        profile_picture = @photo;";

                using (var command = new MySqlCommand(
                    profileSql,
                    connection,
                    transaction))
                {
                    command.Parameters.AddWithValue("@id", accountId);

                    command.Parameters.AddWithValue(
                        "@middle",
                        string.IsNullOrWhiteSpace(txtMiddleName.Text)
                            ? (object)DBNull.Value
                            : txtMiddleName.Text.Trim());

                    command.Parameters.AddWithValue(
                        "@yearBlockProgram",
                        txtYearBlockProgram.Text.Trim());

                    command.Parameters.AddWithValue(
                        "@status",
                        cmbStatus.SelectedItem!.ToString());

                    command.Parameters.AddWithValue(
                        "@birth",
                        dtpBirth.Value.Date);

                    command.Parameters.AddWithValue(
                        "@gender",
                        cmbGender.SelectedItem!.ToString());

                    command.Parameters.AddWithValue(
                        "@contact",
                        txtContact.Text.Trim());

                    command.Parameters.Add(
                        "@photo",
                        MySqlDbType.MediumBlob).Value =
                            (object?)photoBytes ?? DBNull.Value;

                    command.ExecuteNonQuery();
                }

                transaction.Commit();

                MessageBox.Show(
                    this,
                    "Your student information and picture have been saved.",
                    "Saved Successfully",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                ShowError(
                    "That student ID or email belongs to another account.");
            }
            catch (MySqlException)
            {
                ShowError(
                    "Unable to save your information. " +
                    "Check your database connection and table setup.");
            }
        }

        private void ClearFields(object? sender, EventArgs e)
        {
            foreach (TextBox input in new[]
            {
                txtStudentId,
                txtFirstName,
                txtMiddleName,
                txtLastName,
                txtEmail,
                txtYearBlockProgram,
                txtContact
            })
            {
                input.Clear();
            }

            cmbStatus.SelectedIndex = -1;
            cmbGender.SelectedIndex = -1;

            dtpBirth.Value = DateTime.Today;
            dtpBirth.Checked = false;

            // Preserve the picture.
            // Clearing inputs does not change the database.
            txtStudentId.Focus();
        }

        private void UploadPhoto(object? sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Select Student Picture",
                Filter = "Image Files|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                if (new FileInfo(dialog.FileName).Length >
                    5 * 1024 * 1024)
                {
                    MessageBox.Show(
                        this,
                        "Please select an image smaller than 5 MB.");

                    return;
                }

                using Image original =
                    Image.FromFile(dialog.FileName);

                using Bitmap square = new Bitmap(600, 600);

                // Square photo: 600 pixels at 300 DPI = 2 × 2 inches.
                square.SetResolution(300, 300);

                int side = Math.Min(
                    original.Width,
                    original.Height);

                int left = (original.Width - side) / 2;
                int top = (original.Height - side) / 2;

                using (Graphics graphics =
                    Graphics.FromImage(square))
                {
                    graphics.Clear(Color.White);

                    graphics.InterpolationMode =
                        InterpolationMode.HighQualityBicubic;

                    graphics.DrawImage(
                        original,
                        new Rectangle(0, 0, 600, 600),
                        new Rectangle(left, top, side, side),
                        GraphicsUnit.Pixel);
                }

                using MemoryStream stream = new MemoryStream();
                square.Save(stream, ImageFormat.Jpeg);

                SetPhoto(stream.ToArray());
            }
            catch (Exception ex) when (
                ex is IOException ||
                ex is ArgumentException ||
                ex is OutOfMemoryException ||
                ex is UnauthorizedAccessException ||
                ex is System.Runtime.InteropServices.ExternalException)
            {
                ShowError(
                    "Unable to open this picture. Select a valid JPG or PNG.");
            }
        }

        private static Image? DecodePhoto(byte[]? bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }

            using MemoryStream stream = new MemoryStream(bytes);
            using Image original = Image.FromStream(stream);

            return new Bitmap(original);
        }

        private void SetPhoto(byte[]? bytes)
        {
            Image? newImage = DecodePhoto(bytes);
            Image? oldImage = picStudentPhoto.Image;

            picStudentPhoto.Image = newImage;
            photoBytes = bytes;

            oldImage?.Dispose();
        }

        private void ViewProfile(object? sender, EventArgs e)
        {
            string details;
            byte[]? savedPhoto;

            try
            {
                using var connection =
                    new MySqlConnection(connectionString);

                connection.Open();

                const string sql = @"
                    SELECT
                        a.student_id,
                        a.first_name,
                        p.middle_name,
                        a.last_name,
                        a.email,
                        p.year_block_program,
                        p.enrollment_status,
                        p.date_of_birth,
                        p.gender,
                        p.contact_number,
                        p.profile_picture
                    FROM student_accounts a
                    INNER JOIN student_profiles p
                        ON p.account_id = a.account_id
                    WHERE a.account_id = @id;";

                using var command =
                    new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue("@id", accountId);

                using var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    MessageBox.Show(
                        this,
                        "Please save your student information first.");

                    return;
                }

                details =
                    $"Student ID: {reader["student_id"]}\r\n" +
                    $"First Name: {reader["first_name"]}\r\n" +
                    $"Middle Name: {reader["middle_name"]}\r\n" +
                    $"Last Name: {reader["last_name"]}\r\n" +
                    $"Email: {reader["email"]}\r\n" +
                    $"Year, Block & Program: " +
                    $"{reader["year_block_program"]}\r\n" +
                    $"Enrollment Status: " +
                    $"{reader["enrollment_status"]}\r\n" +
                    $"Date of Birth: " +
                    $"{Convert.ToDateTime(reader["date_of_birth"]):MMMM dd, yyyy}\r\n" +
                    $"Gender: {reader["gender"]}\r\n" +
                    $"Contact Number: {reader["contact_number"]}";

                savedPhoto =
                    reader["profile_picture"] == DBNull.Value
                        ? null
                        : (byte[])reader["profile_picture"];
            }
            catch (MySqlException)
            {
                ShowError("Unable to load your saved profile.");
                return;
            }

            try
            {
                ShowProfileWindow(details, savedPhoto);
            }
            catch (ArgumentException)
            {
                ShowError(
                    "The saved picture is invalid. " +
                    "Upload another picture and save again.");
            }
        }

        private void ShowProfileWindow(
            string details,
            byte[]? savedPhoto)
        {
            using Image? image = DecodePhoto(savedPhoto);

            using Form profileWindow = new Form
            {
                Text = "Your Student Profile",
                ClientSize = new Size(650, 650),
                MinimumSize = new Size(500, 500),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White
            };

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(20)
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 180));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 45));

            PictureBox picture = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = image,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.WhiteSmoke
            };

            TextBox profileText = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 11F),
                BackColor = Color.White,
                Text = details
            };

            Button backButton = new Button
            {
                Text = "Back",
                Dock = DockStyle.Fill
            };

            backButton.Click += (sender, e) =>
                profileWindow.Close();

            layout.Controls.Add(picture, 0, 0);
            layout.Controls.Add(profileText, 0, 1);
            layout.Controls.Add(backButton, 0, 2);

            profileWindow.Controls.Add(layout);
            profileWindow.CancelButton = backButton;

            profileWindow.ShowDialog(this);
        }

        private void BackToPrevious(object? sender, EventArgs e)
        {
            Close();
        }

        private void ReleasePhoto(
            object? sender,
            FormClosedEventArgs e)
        {
            Image? image = picStudentPhoto.Image;
            picStudentPhoto.Image = null;
            image?.Dispose();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(
                this,
                message,
                "Student Profile",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}