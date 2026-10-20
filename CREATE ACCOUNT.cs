using System;
using System.Data;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BUCS_SIS;

namespace BUCS_SIS
{
    public partial class SignUpForm : Form
    {
        private readonly string connectionString =
            "server=localhost;port=3306;database=student_db;" +
            "uid=root;password=@Bok15518;charset=utf8mb4;";

        private const int PasswordIterations = 600000;

        private bool isSaving;

        public SignUpForm()
        {
            InitializeComponent();

            Text = "Student Sign Up";

            txtPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;

            txtFirstName.MaxLength = 100;
            txtLastName.MaxLength = 100;
            txtStudentId.MaxLength = 50;
            txtEmail.MaxLength = 254;

            btnCreateAccount.Text = "CREATE ACCOUNT";
            btnGoToLogin.Text = "Already have an account? Log in";

            btnCreateAccount.Click += BtnCreateAccount_Click;
            btnGoToLogin.Click += BtnGoToLogin_Click;
            btnBack.Click += BtnBack_Click;

            FormClosing += SignUpForm_FormClosing;

            AcceptButton = btnCreateAccount;
            CancelButton = btnBack;
        }

        private async void BtnCreateAccount_Click(
            object? sender,
            EventArgs e)
        {
            if (isSaving)
            {
                return;
            }

            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string studentId = txtStudentId.Text.Trim();
            string email = txtEmail.Text.Trim();

            // Preserve passwords exactly as entered.
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (!ValidateInputs(
                firstName,
                lastName,
                studentId,
                email,
                password,
                confirmPassword))
            {
                return;
            }

            SetSavingState(true);

            bool accountCreated = false;

            try
            {
                byte[] salt = RandomNumberGenerator.GetBytes(16);

                byte[] hash = await Task.Run(() =>
                    Rfc2898DeriveBytes.Pbkdf2(
                        password,
                        salt,
                        PasswordIterations,
                        HashAlgorithmName.SHA256,
                        32));

                using (MySqlConnection connection =
                    new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    const string sql = @"
                        INSERT INTO student_accounts
                        (
                            first_name,
                            last_name,
                            student_id,
                            email,
                            password_hash,
                            password_salt,
                            password_iterations
                        )
                        VALUES
                        (
                            @firstName,
                            @lastName,
                            @studentId,
                            @email,
                            @passwordHash,
                            @passwordSalt,
                            @iterations
                        );";

                    using (MySqlCommand command =
                        new MySqlCommand(sql, connection))
                    {
                        command.Parameters.Add(
                            "@firstName",
                            MySqlDbType.VarChar,
                            100).Value = firstName;

                        command.Parameters.Add(
                            "@lastName",
                            MySqlDbType.VarChar,
                            100).Value = lastName;

                        command.Parameters.Add(
                            "@studentId",
                            MySqlDbType.VarChar,
                            50).Value = studentId;

                        command.Parameters.Add(
                            "@email",
                            MySqlDbType.VarChar,
                            254).Value = email;

                        command.Parameters.Add(
                            "@passwordHash",
                            MySqlDbType.VarBinary,
                            32).Value = hash;

                        command.Parameters.Add(
                            "@passwordSalt",
                            MySqlDbType.VarBinary,
                            16).Value = salt;

                        command.Parameters.Add(
                            "@iterations",
                            MySqlDbType.Int32).Value =
                                PasswordIterations;

                        await command.ExecuteNonQueryAsync();

                        accountCreated = true;
                    }
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show(
                    this,
                    "That student ID or email is already registered.\n\n" +
                    "Check your information or log in to your existing account.",
                    "Account Already Exists",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (MySqlException)
            {
                MessageBox.Show(
                    this,
                    "Your account could not be saved.\n\n" +
                    "Check that MySQL is running, your connection " +
                    "settings are correct, and the student_accounts " +
                    "table exists.",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    this,
                    "An unexpected error occurred while creating " +
                    "your account. Please try again.",
                    "Registration Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetSavingState(false);
            }

            if (accountCreated)
            {
                MessageBox.Show(
                    this,
                    "Your student account has been created successfully!\n\n" +
                    "You can now log in using your email and password.",
                    "Account Created",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtPassword.Clear();
                txtConfirmPassword.Clear();

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool ValidateInputs(
            string firstName,
            string lastName,
            string studentId,
            string email,
            string password,
            string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return ShowValidation(
                    "Please enter your first name.",
                    txtFirstName);
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                return ShowValidation(
                    "Please enter your last name.",
                    txtLastName);
            }

            if (string.IsNullOrWhiteSpace(studentId))
            {
                return ShowValidation(
                    "Please enter your student ID.",
                    txtStudentId);
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return ShowValidation(
                    "Please enter your email address.",
                    txtEmail);
            }

            if (!MailAddress.TryCreate(email, out MailAddress? address) ||
                !string.Equals(
                    address.Address,
                    email,
                    StringComparison.OrdinalIgnoreCase))
            {
                return ShowValidation(
                    "Please enter a valid email address.",
                    txtEmail);
            }

            if (string.IsNullOrWhiteSpace(password) ||
                password.Length < 8)
            {
                return ShowValidation(
                    "Please use a password with at least 8 characters.",
                    txtPassword);
            }

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                return ShowValidation(
                    "Please confirm your password.",
                    txtConfirmPassword);
            }

            if (password != confirmPassword)
            {
                return ShowValidation(
                    "Your passwords do not match.",
                    txtConfirmPassword);
            }

            return true;
        }

        private bool ShowValidation(
            string message,
            TextBox input)
        {
            MessageBox.Show(
                this,
                message,
                "Check Your Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            input.Focus();

            return false;
        }

        private void SetSavingState(bool saving)
        {
            isSaving = saving;

            txtFirstName.Enabled = !saving;
            txtLastName.Enabled = !saving;
            txtStudentId.Enabled = !saving;
            txtEmail.Enabled = !saving;
            txtPassword.Enabled = !saving;
            txtConfirmPassword.Enabled = !saving;

            btnCreateAccount.Enabled = !saving;
            btnGoToLogin.Enabled = !saving;
            btnBack.Enabled = !saving;

            btnCreateAccount.Text =
                saving ? "CREATING ACCOUNT..." : "CREATE ACCOUNT";
        }

        private void BtnGoToLogin_Click(
            object? sender,
            EventArgs e)
        {
            ReturnToLogin();
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            ReturnToLogin();
        }

        private void ReturnToLogin()
        {
            if (isSaving)
            {
                return;
            }

            txtPassword.Clear();
            txtConfirmPassword.Clear();

            // Closing restores the student login window
            // that opened signup using ShowDialog().
            Close();
        }

        private void SignUpForm_FormClosing(
            object? sender,
            FormClosingEventArgs e)
        {
            if (isSaving)
            {
                e.Cancel = true;
            }
        }
    }
}