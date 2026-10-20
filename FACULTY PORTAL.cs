using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BUCS_SIS;

namespace BUCS_SIS
{
    public partial class TeacherLoginForm : Form
    {
        private readonly string connectionString =
            "server=localhost;port=3306;database=student_db;" +
            "uid=root;password=@Bok15518;charset=utf8mb4;";

        private bool isLoggingIn;

        // Available after successful authentication.
        public int AuthenticatedTeacherId { get; private set; }
        public string AuthenticatedTeacherEmail { get; private set; } = "";

        public TeacherLoginForm()
        {
            InitializeComponent();

            Text = "Teacher Login";

            SetPasswordMode(txtPassword);
            SetPasswordMode(txtTeacherCode);

            btnLogin.Click += BtnLogin_Click;
            btnBack.Click += BtnBack_Click;
            FormClosing += TeacherLoginForm_FormClosing;

            AcceptButton = btnLogin;
            CancelButton = btnBack;
        }

        private async void BtnLogin_Click(
            object? sender,
            EventArgs e)
        {
            if (isLoggingIn)
            {
                return;
            }

            string email = ReadInput(txtIdentity).Trim();
            string password = ReadInput(txtPassword);
            string teacherCode = ReadInput(txtTeacherCode).Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                ShowRequiredMessage(
                    "Please enter your email address.",
                    txtIdentity);

                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowRequiredMessage(
                    "Please enter your password.",
                    txtPassword);

                return;
            }

            if (string.IsNullOrWhiteSpace(teacherCode))
            {
                ShowRequiredMessage(
                    "Please enter your assigned teacher code.",
                    txtTeacherCode);

                return;
            }

            SetBusy(true);

            try
            {
                int teacherId = 0;
                string storedEmail = "";
                byte[] storedHash = Array.Empty<byte>();
                byte[] storedSalt = Array.Empty<byte>();
                byte[] storedCodeHash = Array.Empty<byte>();
                int iterations = 0;

                using (MySqlConnection connection =
                    new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    const string sql = @"
                        SELECT
                            account_id,
                            email,
                            password_hash,
                            password_salt,
                            password_iterations,
                            teacher_code_hash
                        FROM teacher_accounts
                        WHERE email = @email
                          AND is_active = TRUE
                        LIMIT 1;";

                    using (MySqlCommand command =
                        new MySqlCommand(sql, connection))
                    {
                        command.Parameters.Add(
                            "@email",
                            MySqlDbType.VarChar,
                            254).Value = email;

                        using (var reader =
                            await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                teacherId = reader.GetInt32(0);
                                storedEmail = reader.GetString(1);
                                storedHash = (byte[])reader.GetValue(2);
                                storedSalt = (byte[])reader.GetValue(3);
                                iterations = reader.GetInt32(4);
                                storedCodeHash =
                                    (byte[])reader.GetValue(5);
                            }
                        }
                    }
                }

                if (teacherId == 0)
                {
                    ShowInvalidLogin();
                    return;
                }

                // Check the password and code against the SAME account.
                bool valid = await Task.Run(() =>
                {
                    byte[] enteredPasswordHash =
                        Rfc2898DeriveBytes.Pbkdf2(
                            password,
                            storedSalt,
                            iterations,
                            HashAlgorithmName.SHA256,
                            32);

                    byte[] enteredCodeHash = SHA256.HashData(
                        Encoding.UTF8.GetBytes(teacherCode));

                    bool passwordMatches =
                        CryptographicOperations.FixedTimeEquals(
                            enteredPasswordHash,
                            storedHash);

                    bool codeMatches =
                        CryptographicOperations.FixedTimeEquals(
                            enteredCodeHash,
                            storedCodeHash);

                    return passwordMatches && codeMatches;
                });

                if (!valid)
                {
                    ShowInvalidLogin();
                    return;
                }

                AuthenticatedTeacherId = teacherId;
                AuthenticatedTeacherEmail = storedEmail;

                ClearInput(txtPassword);
                ClearInput(txtTeacherCode);

                MessageBox.Show(
                    this,
                    "Teacher login successful!",
                    "Welcome",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Authentication is complete.
                // Open the teacher profile here when it is ready,
                // passing AuthenticatedTeacherId to load its data.
            }
            catch (MySqlException)
            {
                MessageBox.Show(
                    this,
                    "Unable to connect to the teacher account database.\n\n" +
                    "Check your MySQL connection settings and " +
                    "make sure the teacher_accounts table exists.",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    this,
                    "Login could not be completed. " +
                    "Please check the account setup and try again.",
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private void ShowInvalidLogin()
        {
            AuthenticatedTeacherId = 0;
            AuthenticatedTeacherEmail = "";

            MessageBox.Show(
                this,
                "Invalid email, password, or teacher code.",
                "Login Failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            ClearInput(txtPassword);
            ClearInput(txtTeacherCode);

            FocusInput(txtPassword);
        }

        private void SetBusy(bool busy)
        {
            isLoggingIn = busy;

            txtIdentity.Enabled = !busy;
            txtPassword.Enabled = !busy;
            txtTeacherCode.Enabled = !busy;

            btnLogin.Enabled = !busy;
            btnBack.Enabled = !busy;
        }

        private static TextBox? FindTextBox(Control control)
        {
            if (control is TextBox textBox)
            {
                return textBox;
            }

            foreach (Control child in control.Controls)
            {
                TextBox? result = FindTextBox(child);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        private static string ReadInput(Control control)
        {
            TextBox? textBox = FindTextBox(control);

            return textBox != null
                ? textBox.Text
                : control.Text;
        }

        private static void ClearInput(Control control)
        {
            TextBox? textBox = FindTextBox(control);

            if (textBox != null)
            {
                textBox.Clear();
            }
            else
            {
                control.Text = "";
            }
        }

        private static void SetPasswordMode(Control control)
        {
            TextBox? textBox = FindTextBox(control);

            if (textBox != null)
            {
                textBox.UseSystemPasswordChar = true;
            }
        }

        private static void FocusInput(Control control)
        {
            TextBox? textBox = FindTextBox(control);

            if (textBox != null)
            {
                textBox.Focus();
            }
            else
            {
                control.Focus();
            }
        }

        private void ShowRequiredMessage(
            string message,
            Control input)
        {
            MessageBox.Show(
                this,
                message,
                "Missing Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            FocusInput(input);
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            if (!isLoggingIn)
            {
                Close();
            }
        }

        private void TeacherLoginForm_FormClosing(
            object? sender,
            FormClosingEventArgs e)
        {
            if (isLoggingIn)
            {
                e.Cancel = true;
            }
        }
    }
}