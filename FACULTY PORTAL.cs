#nullable enable

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

        public TeacherLoginForm()
        {
            InitializeComponent();

            Text = "Faculty Portal";

            SetPasswordMode(txtPassword);
            SetPasswordMode(txtTeacherCode);

            btnLogin.Click += BtnLogin_Click;
            btnBack.Click += BtnBack_Click;

            FormClosing += TeacherLoginForm_FormClosing;

            AcceptButton = btnLogin;
            CancelButton = btnBack;
        }

        // Supports standard TextBox controls and CyberTextBox
        // controls containing an inner standard TextBox.
        private static TextBox? FindTextBox(Control control)
        {
            if (control is TextBox textBox)
            {
                return textBox;
            }

            foreach (Control child in control.Controls)
            {
                TextBox? found = FindTextBox(child);

                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }

        private static string ReadInput(Control control)
        {
            return FindTextBox(control)?.Text ?? control.Text;
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

        private static void FocusInput(Control control)
        {
            Control target = FindTextBox(control) ?? control;
            target.Focus();
        }

        private static void SetPasswordMode(Control control)
        {
            TextBox? textBox = FindTextBox(control);

            if (textBox != null)
            {
                textBox.UseSystemPasswordChar = true;
            }
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

            int authenticatedTeacherId = 0;

            try
            {
                int teacherId = 0;
                int iterations = 0;

                byte[] storedPasswordHash = Array.Empty<byte>();
                byte[] storedSalt = Array.Empty<byte>();
                byte[] storedCodeHash = Array.Empty<byte>();

                using (var connection =
                    new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    const string sql = @"
                        SELECT
                            account_id,
                            password_hash,
                            password_salt,
                            password_iterations,
                            teacher_code_hash
                        FROM teacher_accounts
                        WHERE email = @email
                          AND is_active = TRUE
                        LIMIT 1;";

                    using var command =
                        new MySqlCommand(sql, connection);

                    command.Parameters.Add(
                        "@email",
                        MySqlDbType.VarChar,
                        254).Value = email;

                    using var reader =
                        await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        teacherId = reader.GetInt32(0);
                        storedPasswordHash =
                            (byte[])reader.GetValue(1);
                        storedSalt =
                            (byte[])reader.GetValue(2);
                        iterations = reader.GetInt32(3);
                        storedCodeHash =
                            (byte[])reader.GetValue(4);
                    }
                }

                if (teacherId == 0)
                {
                    ShowInvalidLogin();
                    return;
                }

                if (storedPasswordHash.Length != 32 ||
                    storedSalt.Length != 16 ||
                    storedCodeHash.Length != 32 ||
                    iterations < 1 ||
                    iterations > 2000000)
                {
                    MessageBox.Show(
                        this,
                        "This teacher account has invalid authentication " +
                        "data. Check its database setup.",
                        "Account Setup Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                bool valid = await Task.Run(() =>
                {
                    byte[] enteredPasswordHash =
                        Rfc2898DeriveBytes.Pbkdf2(
                            password,
                            storedSalt,
                            iterations,
                            HashAlgorithmName.SHA256,
                            32);

                    byte[] enteredCodeHash =
                        SHA256.HashData(
                            Encoding.UTF8.GetBytes(teacherCode));

                    bool passwordMatches =
                        CryptographicOperations.FixedTimeEquals(
                            enteredPasswordHash,
                            storedPasswordHash);

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

                // Confirm that the professor has an assigned profile.
                using (var connection =
                    new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using var command = new MySqlCommand(@"
                        SELECT f.account_id
                        FROM faculty_profiles f
                        INNER JOIN faculty_cohorts c
                            ON c.cohort_id = f.cohort_id
                        WHERE f.account_id = @id
                        LIMIT 1;",
                        connection);

                    command.Parameters.Add(
                        "@id",
                        MySqlDbType.Int32).Value = teacherId;

                    object? result =
                        await command.ExecuteScalarAsync();

                    if (result == null || result == DBNull.Value)
                    {
                        MessageBox.Show(
                            this,
                            "Your login details are correct, but your " +
                            "faculty profile and cohort assignment " +
                            "have not been configured.",
                            "Faculty Assignment Required",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }
                }

                authenticatedTeacherId = teacherId;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(
                    this,
                    "Unable to check your teacher account.\n\n" +
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    this,
                    "Login could not be completed. " +
                    "Check the teacher account setup and try again.",
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
            }

            if (authenticatedTeacherId <= 0)
            {
                return;
            }

            ClearInput(txtPassword);
            ClearInput(txtTeacherCode);

            using (FacultyProfileForm facultyForm =
                new FacultyProfileForm(authenticatedTeacherId))
            {
                facultyForm.StartPosition =
                    FormStartPosition.CenterParent;

                Hide();

                try
                {
                    facultyForm.ShowDialog(this);
                }
                finally
                {
                    Show();
                    FocusInput(txtPassword);
                }
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

        private void ShowInvalidLogin()
        {
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

        private void BtnBack_Click(
            object? sender,
            EventArgs e)
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