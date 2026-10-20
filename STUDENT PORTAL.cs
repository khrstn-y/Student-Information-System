using System;
using System.Windows.Forms;
using BUCS_SIS;

namespace BUCS_SIS
{
    public partial class StudentLoginForm : Form
    {
        public StudentLoginForm()
        {
            InitializeComponent();

            Text = "Student Login";

            txtPassword.UseSystemPasswordChar = true;

            btnLogin.Click += BtnLogin_Click;
            btnSignUp.Click += BtnSignUp_Click;
            btnBack.Click += BtnBack_Click;

            AcceptButton = btnLogin;
            CancelButton = btnBack;
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string identity = txtIdentity.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(identity))
            {
                MessageBox.Show(
                    this,
                    "Please enter your username or email.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtIdentity.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    this,
                    "Please enter your password.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPassword.Focus();
                return;
            }

            // MySQL authentication goes here:
            // 1. Find the student account by username or email.
            // 2. Verify its stored password hash.
            // 3. Open the profile using the authenticated account ID.
            // Only open the profile after successful authentication.

            MessageBox.Show(
                this,
                "Database authentication has not been connected yet.",
                "Student Login",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnSignUp_Click(object? sender, EventArgs e)
        {
            // Assumes your signup class is named SignUpForm
            // and has a constructor without arguments.
            using (SignUpForm signUpForm = new SignUpForm())
            {
                Hide();

                try
                {
                    signUpForm.StartPosition =
                        FormStartPosition.CenterParent;

                    signUpForm.ShowDialog(this);
                }
                finally
                {
                    Show();
                    txtPassword.Clear();
                    txtIdentity.Focus();
                }
            }
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            // Returns to the welcome window opened earlier.
            Close();
        }
    }
}