#nullable enable

using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BUCS_SIS
{
    public partial class StudentLoginForm : Form
    {
        private readonly string connectionString =
            "server=localhost;port=3306;database=student_db;" +
            "uid=root;password=@Bok15518;charset=utf8mb4;";

        private bool isLoggingIn;

        public StudentLoginForm()
        {
            InitializeComponent();

            Text = "Student Login";
            txtPassword.UseSystemPasswordChar = true;

            btnLogin.Click += BtnLogin_Click;
            btnSignUp.Click += BtnSignUp_Click;
            btnBack.Click += BtnBack_Click;

            FormClosing += StudentLoginForm_FormClosing;

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

            string email = txtIdentity.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show(this, "Please enter your email address.");
                txtIdentity.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(this, "Please enter your password.");
                txtPassword.Focus();
                return;
            }

            SetBusy(true);

            int authenticatedAccountId = 0;

            try
            {
                int accountId = 0;
                byte[] storedHash = Array.Empty<byte>();
                byte[] storedSalt = Array.Empty<byte>();
                int iterations = 0;

                using (var connection =
                    new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    const string sql = @"
                        SELECT
                            account_id,
                            password_hash,
                            password_salt,
                            password_iterations
                        FROM student_accounts
                        WHERE email = @email
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
                        accountId = reader.GetInt32(0);
                        storedHash = (byte[])reader.GetValue(1);
                        storedSalt = (byte[])reader.GetValue(2);
                        iterations = reader.GetInt32(3);
                    }
                }

                if (accountId == 0)
                {
                    ShowInvalidLogin();
                    return;
                }

                // Validate the expected password record format.
                if (storedHash.Length != 32 ||
                    storedSalt.Length != 16 ||
                    iterations < 1 ||
                    iterations > 2000000)
                {
                    MessageBox.Show(
                        this,
                        "This account's password data is invalid. " +
                        "Check how the account was registered.",
                        "Account Setup Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                bool passwordMatches = await Task.Run(() =>
                {
                    byte[] enteredHash =
                        Rfc2898DeriveBytes.Pbkdf2(
                            password,
                            storedSalt,
                            iterations,
                            HashAlgorithmName.SHA256,
                            32);

                    return CryptographicOperations.FixedTimeEquals(
                        enteredHash,
                        storedHash);
                });

                if (!passwordMatches)
                {
                    ShowInvalidLogin();
                    return;
                }

                authenticatedAccountId = accountId;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(
                    this,
                    "Unable to check your account.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    this,
                    "Login could not be completed. Check the account setup.",
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
            }

            if (authenticatedAccountId > 0)
            {
                txtPassword.Clear();

                using var profile =
                    new StudentProfileUpdateForm(authenticatedAccountId);

                Hide();

                try
                {
                    profile.ShowDialog(this);
                }
                finally
                {
                    Show();
                    txtPassword.Focus();
                }
            }
        }

        private void ShowInvalidLogin()
        {
            MessageBox.Show(
                this,
                "Invalid email or password.",
                "Login Failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            txtPassword.Clear();
            txtPassword.Focus();
        }

        private void SetBusy(bool busy)
        {
            isLoggingIn = busy;

            txtIdentity.Enabled = !busy;
            txtPassword.Enabled = !busy;
            btnLogin.Enabled = !busy;
            btnSignUp.Enabled = !busy;
            btnBack.Enabled = !busy;
        }

        private void BtnSignUp_Click(
            object? sender,
            EventArgs e)
        {
            if (isLoggingIn)
            {
                return;
            }

            using var signup = new SignUpForm();

            Hide();

            try
            {
                signup.StartPosition = FormStartPosition.CenterParent;
                signup.ShowDialog(this);
            }
            finally
            {
                Show();
                txtPassword.Clear();
                txtIdentity.Focus();
            }
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

        private void StudentLoginForm_FormClosing(
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