#nullable enable

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BUCS_SIS;

namespace BUCS_SIS
{
    public partial class StudentProfileUpdateForm : Form
    {
        private readonly int accountId;
        private byte[]? photoBytes;

        private readonly string connectionString =
            "server=localhost;port=3306;database=student_db;" +
            "uid=root;password=@Bok15518;charset=utf8mb4;";

        // Required by the Visual Studio Designer.
        public StudentProfileUpdateForm()
        {
            InitializeComponent();

            Shown += ProfileForm_Shown;
            FormClosed += ProfileForm_Closed;
        }

        // Use after successful student login.
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

        private Control GetControl(string name)
        {
            Control[] matches = Controls.Find(name, true);

            if (matches.Length != 1)
            {
                throw new InvalidOperationException(
                    $"The Designer must contain one control named '{name}'.");
            }

            return matches[0];
        }

        private T GetControl<T>(string name) where T : Control
        {
            Control control = GetControl(name);

            if (control is T typedControl)
            {
                return typedControl;
            }

            throw new InvalidOperationException(
                $"'{name}' must be a {typeof(T).Name} control.");
        }

        // Finds the standard TextBox inside a CyberTextBox.
        private static TextBox? FindInnerTextBox(Control control)
        {
            if (control is TextBox textBox)
            {
                return textBox;
            }

            foreach (Control child in control.Controls)
            {
                TextBox? found = FindInnerTextBox(child);

                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }

        private TextBox GetEditor(string name)
        {
            return FindInnerTextBox(GetControl(name))
                ?? throw new InvalidOperationException(
                    $"'{name}' must be a TextBox or contain a TextBox.");
        }

        private string ReadInput(string name)
        {
            return GetEditor(name).Text.Trim();
        }

        private void SetInput(string name, string? value)
        {
            GetEditor(name).Text = value ?? "";
        }

        private ComboBox EnrollmentStatus =>
            GetControl<ComboBox>("cmbStatus");

        private ComboBox Gender =>
            GetControl<ComboBox>("cmbGender");

        private DateTimePicker BirthDate =>
            GetControl<DateTimePicker>("dtpBirth");

        private PictureBox StudentPhoto =>
            GetControl<PictureBox>("picStudentPhoto");

        private void ProfileForm_Shown(object? sender, EventArgs e)
        {
            if (accountId <= 0)
            {
                ShowError(
                    "Please log in first. Open this form using " +
                    "new StudentProfileUpdateForm(accountId).");

                Close();
                return;
            }

            try
            {
                ConfigureControls();
                LoadStudent();
            }
            catch (InvalidOperationException ex)
            {
                ShowError(ex.Message);
                Close();
            }
        }

        private void ConfigureControls()
        {
            Text = "Student Information";
            StartPosition = FormStartPosition.CenterParent;

            GetEditor("txtStudentId").MaxLength = 50;
            GetEditor("txtFirstName").MaxLength = 100;
            GetEditor("txtMiddleName").MaxLength = 100;
            GetEditor("txtLastName").MaxLength = 100;
            GetEditor("txtEmail").MaxLength = 254;
            GetEditor("txtYearBlockProgram").MaxLength = 216;
            GetEditor("txtContact").MaxLength = 30;

            EnrollmentStatus.DropDownStyle =
                ComboBoxStyle.DropDownList;

            EnrollmentStatus.Items.Clear();
            EnrollmentStatus.Items.AddRange(new object[]
            {
                "Regular",
                "Irregular",
                "LOA"
            });

            Gender.DropDownStyle = ComboBoxStyle.DropDownList;
            Gender.Items.Clear();
            Gender.Items.AddRange(new object[]
            {
                "Male",
                "Female",
                "Other",
                "Prefer not to say"
            });

            BirthDate.Format = DateTimePickerFormat.Short;
            BirthDate.MaxDate = DateTime.Today;
            BirthDate.ShowCheckBox = true;
            BirthDate.Checked = false;

            StudentPhoto.SizeMode = PictureBoxSizeMode.Zoom;

            Button save = GetControl<Button>("btnSave");
            Button clear = GetControl<Button>("btnClear");
            Button view = GetControl<Button>("btnView");
            Button back = GetControl<Button>("btnBack");
            Button upload = GetControl<Button>("btnUploadPhoto");

            save.Text = "Save Student";
            clear.Text = "Clear Fields";
            view.Text = "View Your Profile";
            back.Text = "Back";
            upload.Text = "Upload 2×2 Picture";

            save.Click += SaveStudent;
            clear.Click += ClearFields;
            view.Click += ViewProfile;
            back.Click += BackToPrevious;
            upload.Click += UploadPhoto;

            AcceptButton = save;
            CancelButton = back;
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
                    ShowError("Student account not found.");
                    Close();
                    return;
                }

                SetInput(
                    "txtStudentId",
                    reader["student_id"].ToString());

                SetInput(
                    "txtFirstName",
                    reader["first_name"].ToString());

                SetInput(
                    "txtLastName",
                    reader["last_name"].ToString());

                SetInput(
                    "txtEmail",
                    reader["email"].ToString());

                if (reader["profile_id"] == DBNull.Value)
                {
                    MessageBox.Show(
                        this,
                        "Complete your student information and " +
                        "click Save Student.",
                        "Complete Your Profile",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                SetInput(
                    "txtMiddleName",
                    reader["middle_name"].ToString());

                SetInput(
                    "txtYearBlockProgram",
                    reader["year_block_program"].ToString());

                SetInput(
                    "txtContact",
                    reader["contact_number"].ToString());

                EnrollmentStatus.SelectedItem =
                    reader["enrollment_status"].ToString();

                Gender.SelectedItem =
                    reader["gender"].ToString();

                DateTime birth =
                    Convert.ToDateTime(reader["date_of_birth"]);

                if (birth >= BirthDate.MinDate &&
                    birth <= BirthDate.MaxDate)
                {
                    BirthDate.Value = birth;
                    BirthDate.Checked = true;
                }

                photoBytes =
                    reader["profile_picture"] == DBNull.Value
                        ? null
                        : (byte[])reader["profile_picture"];

                try
                {
                    SetPhoto(photoBytes);
                }
                catch (Exception ex) when (IsImageError(ex))
                {
                    ShowError(
                        "Your information was loaded, but the saved " +
                        "picture could not be displayed. Upload a replacement.");
                }
            }
            catch (MySqlException ex)
            {
                ShowError(
                    "Unable to load your profile.\n\n" + ex.Message);

                Close();
            }
        }

        private bool ValidateInputs()
        {
            foreach (string name in new[]
            {
                "txtStudentId",
                "txtFirstName",
                "txtLastName",
                "txtEmail",
                "txtYearBlockProgram",
                "txtContact"
            })
            {
                if (string.IsNullOrWhiteSpace(ReadInput(name)))
                {
                    ShowError(
                        "Complete all required fields. " +
                        "Middle name and picture are optional.");

                    GetEditor(name).Focus();
                    return false;
                }
            }

            string email = ReadInput("txtEmail");

            if (!MailAddress.TryCreate(
                    email, out MailAddress? address) ||
                !string.Equals(
                    address.Address,
                    email,
                    StringComparison.OrdinalIgnoreCase))
            {
                ShowError("Enter a valid email address.");
                GetEditor("txtEmail").Focus();
                return false;
            }

            if (EnrollmentStatus.SelectedIndex < 0 ||
                Gender.SelectedIndex < 0)
            {
                ShowError("Select your enrollment status and gender.");
                return false;
            }

            if (!BirthDate.Checked ||
                BirthDate.Value.Date > DateTime.Today)
            {
                ShowError("Select a valid date of birth.");
                BirthDate.Focus();
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
                        ShowError("The student account no longer exists.");
                        return;
                    }
                }

                using (var command = new MySqlCommand(@"
                    UPDATE student_accounts
                    SET student_id = @studentId,
                        first_name = @firstName,
                        last_name = @lastName,
                        email = @email
                    WHERE account_id = @id;",
                    connection,
                    transaction))
                {
                    command.Parameters.AddWithValue("@id", accountId);

                    command.Parameters.AddWithValue(
                        "@studentId", ReadInput("txtStudentId"));

                    command.Parameters.AddWithValue(
                        "@firstName", ReadInput("txtFirstName"));

                    command.Parameters.AddWithValue(
                        "@lastName", ReadInput("txtLastName"));

                    command.Parameters.AddWithValue(
                        "@email", ReadInput("txtEmail"));

                    command.ExecuteNonQuery();
                }

                using (var command = new MySqlCommand(@"
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
                        @academic,
                        @status,
                        @birth,
                        @gender,
                        @contact,
                        @photo
                    )
                    ON DUPLICATE KEY UPDATE
                        middle_name = @middle,
                        year_block_program = @academic,
                        enrollment_status = @status,
                        date_of_birth = @birth,
                        gender = @gender,
                        contact_number = @contact,
                        profile_picture = @photo;",
                    connection,
                    transaction))
                {
                    string middle = ReadInput("txtMiddleName");

                    command.Parameters.AddWithValue("@id", accountId);

                    command.Parameters.AddWithValue(
                        "@middle",
                        middle.Length == 0
                            ? (object)DBNull.Value
                            : middle);

                    command.Parameters.AddWithValue(
                        "@academic",
                        ReadInput("txtYearBlockProgram"));

                    command.Parameters.AddWithValue(
                        "@status",
                        EnrollmentStatus.SelectedItem!.ToString());

                    command.Parameters.AddWithValue(
                        "@birth",
                        BirthDate.Value.Date);

                    command.Parameters.AddWithValue(
                        "@gender",
                        Gender.SelectedItem!.ToString());

                    command.Parameters.AddWithValue(
                        "@contact",
                        ReadInput("txtContact"));

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
            catch (MySqlException ex)
            {
                ShowError(
                    "Unable to save your information.\n\n" + ex.Message);
            }
        }

        private void ClearFields(object? sender, EventArgs e)
        {
            foreach (string name in new[]
            {
                "txtStudentId",
                "txtFirstName",
                "txtMiddleName",
                "txtLastName",
                "txtEmail",
                "txtYearBlockProgram",
                "txtContact"
            })
            {
                SetInput(name, "");
            }

            EnrollmentStatus.SelectedIndex = -1;
            Gender.SelectedIndex = -1;

            BirthDate.Value = DateTime.Today;
            BirthDate.Checked = false;

            // Preserve the photo. This does not delete saved information.
            GetEditor("txtStudentId").Focus();
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
                    ShowError("Select an image smaller than 5 MB.");
                    return;
                }

                using Image original =
                    Image.FromFile(dialog.FileName);

                using Bitmap square = new Bitmap(600, 600);
                square.SetResolution(300, 300);

                int side = Math.Min(
                    original.Width, original.Height);

                using (Graphics graphics =
                    Graphics.FromImage(square))
                {
                    graphics.Clear(Color.White);

                    graphics.InterpolationMode =
                        InterpolationMode.HighQualityBicubic;

                    graphics.DrawImage(
                        original,
                        new Rectangle(0, 0, 600, 600),
                        new Rectangle(
                            (original.Width - side) / 2,
                            (original.Height - side) / 2,
                            side,
                            side),
                        GraphicsUnit.Pixel);
                }

                using MemoryStream stream = new MemoryStream();
                square.Save(stream, ImageFormat.Jpeg);

                SetPhoto(stream.ToArray());
            }
            catch (Exception ex) when (
                IsImageError(ex) ||
                ex is IOException ||
                ex is UnauthorizedAccessException)
            {
                ShowError(
                    "Unable to open this picture. Select a valid JPG or PNG.");
            }
        }

        private static bool IsImageError(Exception ex)
        {
            return ex is ArgumentException ||
                   ex is OutOfMemoryException ||
                   ex is System.Runtime.InteropServices.ExternalException;
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
            Image? previous = StudentPhoto.Image;

            StudentPhoto.Image = newImage;
            photoBytes = bytes;

            previous?.Dispose();
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

                using var command = new MySqlCommand(@"
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
                    WHERE a.account_id = @id;",
                    connection);

                command.Parameters.AddWithValue("@id", accountId);

                using var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    MessageBox.Show(
                        this, "Save your student information first.");
                    return;
                }

                StringBuilder text = new StringBuilder();

                text.AppendLine(
                    $"Student ID: {reader["student_id"]}");
                text.AppendLine(
                    $"First Name: {reader["first_name"]}");
                text.AppendLine(
                    $"Middle Name: {reader["middle_name"]}");
                text.AppendLine(
                    $"Last Name: {reader["last_name"]}");
                text.AppendLine(
                    $"Email: {reader["email"]}");
                text.AppendLine(
                    $"Year, Block & Program: {reader["year_block_program"]}");
                text.AppendLine(
                    $"Enrollment Status: {reader["enrollment_status"]}");
                text.AppendLine(
                    $"Date of Birth: " +
                    $"{Convert.ToDateTime(reader["date_of_birth"]):MMMM dd, yyyy}");
                text.AppendLine(
                    $"Gender: {reader["gender"]}");
                text.AppendLine(
                    $"Contact Number: {reader["contact_number"]}");

                details = text.ToString();

                savedPhoto =
                    reader["profile_picture"] == DBNull.Value
                        ? null
                        : (byte[])reader["profile_picture"];
            }
            catch (MySqlException ex)
            {
                ShowError(
                    "Unable to load your saved profile.\n\n" + ex.Message);
                return;
            }

            try
            {
                ShowProfileWindow(details, savedPhoto);
            }
            catch (Exception ex) when (IsImageError(ex))
            {
                ShowError(
                    "The saved picture could not be displayed. " +
                    "Upload a replacement and save again.");
            }
        }

        private void ShowProfileWindow(
            string details,
            byte[]? savedPhoto)
        {
            using Image? image = DecodePhoto(savedPhoto);

            using Form window = new Form
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
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = image
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

            Button back = new Button
            {
                Text = "Back",
                Dock = DockStyle.Fill
            };

            back.Click += (sender, e) => window.Close();

            layout.Controls.Add(picture, 0, 0);
            layout.Controls.Add(profileText, 0, 1);
            layout.Controls.Add(back, 0, 2);

            window.Controls.Add(layout);
            window.CancelButton = back;

            window.ShowDialog(this);
        }

        private void BackToPrevious(object? sender, EventArgs e)
        {
            Close();
        }

        private void ProfileForm_Closed(
            object? sender,
            FormClosedEventArgs e)
        {
            Control[] matches =
                Controls.Find("picStudentPhoto", true);

            if (matches.Length == 1 &&
                matches[0] is PictureBox picture)
            {
                Image? previous = picture.Image;
                picture.Image = null;
                previous?.Dispose();
            }
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