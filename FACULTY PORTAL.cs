using System;
using System.Windows.Forms;
using BUCS_SIS;

namespace BUCS_SIS
{
    public partial class TeacherLoginForm : Form
    {
        public TeacherLoginForm()
        {
            InitializeComponent();

            Text = "Teacher Login";

            txtPassword.UseSystemPasswordChar = true;
            txtTeacherCode.UseSystemPasswordChar = true;

            btnLogin.Click += BtnLogin_Click;
            btnBack.Click += BtnBack_Click;

            AcceptButton = btnLogin;
            CancelButton = btnBack;
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string identity = txtIdentity.Text.Trim();
            string password = txtPassword.Text;
            string teacherCode = txtTeacherCode.Text.Trim();

            if (string.IsNullOrWhiteSpace(identity))
            {
                ShowRequiredMessage(
                    "Please enter your username or email.",
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

            // MySQL authentication goes here:
            // 1. Find the existing teacher account by username or email.
            // 2. Verify its stored password hash.
            // 3. Verify the code assigned to that same teacher account.
            // 4. Open the profile using the authenticated account ID.
            // Only open the profile if all checks succeed.

            MessageBox.Show(
                this,
                "Database authentication has not been connected yet.",
                "Teacher Login",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ShowRequiredMessage(
            string message,
            TextBox input)
        {
            MessageBox.Show(
                this,
                message,
                "Missing Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            input.Focus();
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}