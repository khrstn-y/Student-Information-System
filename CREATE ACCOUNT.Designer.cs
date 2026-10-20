#nullable enable

using System.Drawing;
using System.Windows.Forms;

namespace BUCS_SIS
{
    partial class SignUpForm
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(
                    typeof(SignUpForm));

            btnCreateAccount = new Button();
            btnGoToLogin = new Button();
            txtFirstName = new TextBox();
            txtStudentId = new TextBox();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            txtLastName = new TextBox();
            txtEmail = new TextBox();
            btnBack = new Button();

            SuspendLayout();

            // 
            // txtFirstName
            // 
            txtFirstName.BackColor = Color.White;
            txtFirstName.BorderStyle = BorderStyle.FixedSingle;
            txtFirstName.Font = new Font("Montserrat Light", 10F);
            txtFirstName.ForeColor = Color.Black;
            txtFirstName.Location = new Point(380, 112);
            txtFirstName.MaxLength = 100;
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(177, 30);
            txtFirstName.TabIndex = 0;

            // 
            // txtLastName
            // 
            txtLastName.BackColor = Color.White;
            txtLastName.BorderStyle = BorderStyle.FixedSingle;
            txtLastName.Font = new Font("Montserrat Light", 10F);
            txtLastName.ForeColor = Color.Black;
            txtLastName.Location = new Point(578, 112);
            txtLastName.MaxLength = 100;
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(177, 30);
            txtLastName.TabIndex = 1;

            // 
            // txtStudentId
            // 
            txtStudentId.BackColor = Color.White;
            txtStudentId.BorderStyle = BorderStyle.FixedSingle;
            txtStudentId.Font = new Font("Montserrat Light", 10F);
            txtStudentId.ForeColor = Color.Black;
            txtStudentId.Location = new Point(380, 169);
            txtStudentId.MaxLength = 50;
            txtStudentId.Name = "txtStudentId";
            txtStudentId.Size = new Size(177, 30);
            txtStudentId.TabIndex = 2;

            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.White;
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Montserrat Light", 10F);
            txtEmail.ForeColor = Color.Black;
            txtEmail.Location = new Point(578, 169);
            txtEmail.MaxLength = 254;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(177, 30);
            txtEmail.TabIndex = 3;

            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Montserrat Light", 10F);
            txtPassword.ForeColor = Color.Black;
            txtPassword.Location = new Point(380, 221);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(375, 30);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;

            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BackColor = Color.White;
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.Font = new Font("Montserrat Light", 10F);
            txtConfirmPassword.ForeColor = Color.Black;
            txtConfirmPassword.Location = new Point(380, 273);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(375, 30);
            txtConfirmPassword.TabIndex = 5;
            txtConfirmPassword.UseSystemPasswordChar = true;

            // 
            // btnCreateAccount
            // 
            btnCreateAccount.BackColor = Color.Transparent;
            btnCreateAccount.BackgroundImage =
                (Image?)resources.GetObject(
                    "btnCreateAccount.BackgroundImage");

            btnCreateAccount.FlatAppearance.BorderSize = 0;
            btnCreateAccount.FlatStyle = FlatStyle.Flat;
            btnCreateAccount.Font = new Font(
                "Montserrat ExtraBold",
                12F,
                FontStyle.Bold,
                GraphicsUnit.Point);

            btnCreateAccount.ForeColor = Color.White;
            btnCreateAccount.Location = new Point(465, 329);
            btnCreateAccount.Name = "btnCreateAccount";
            btnCreateAccount.Size = new Size(197, 46);
            btnCreateAccount.TabIndex = 6;
            btnCreateAccount.Text = "CREATE ACCOUNT";
            btnCreateAccount.TextImageRelation =
                TextImageRelation.TextAboveImage;
            btnCreateAccount.UseVisualStyleBackColor = false;

            // 
            // btnGoToLogin
            // 
            btnGoToLogin.BackColor = Color.Transparent;
            btnGoToLogin.BackgroundImage =
                (Image?)resources.GetObject(
                    "btnGoToLogin.BackgroundImage");

            btnGoToLogin.FlatAppearance.BorderSize = 0;
            btnGoToLogin.FlatStyle = FlatStyle.Flat;
            btnGoToLogin.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold,
                GraphicsUnit.Point);

            btnGoToLogin.ForeColor = Color.White;
            btnGoToLogin.Location = new Point(405, 399);
            btnGoToLogin.Name = "btnGoToLogin";
            btnGoToLogin.Size = new Size(320, 37);
            btnGoToLogin.TabIndex = 7;
            btnGoToLogin.Text = "Already have an account? Log in";
            btnGoToLogin.UseVisualStyleBackColor = false;
            btnGoToLogin.CausesValidation = false;

            // 
            // btnBack
            // 
            btnBack.BackColor = Color.Transparent;
            btnBack.BackgroundImage = Properties.Resources.BACK_BUTTON;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Location = new Point(44, 425);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(40, 41);
            btnBack.TabIndex = 8;
            btnBack.UseVisualStyleBackColor = false;
            btnBack.CausesValidation = false;
            btnBack.AccessibleName = "Back";

            // 
            // SignUpForm
            // 
            AcceptButton = btnCreateAccount;
            CancelButton = btnBack;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.CREATE;
            ClientSize = new Size(884, 501);

            Controls.Add(btnBack);
            Controls.Add(txtEmail);
            Controls.Add(txtLastName);
            Controls.Add(txtConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(txtStudentId);
            Controls.Add(txtFirstName);
            Controls.Add(btnGoToLogin);
            Controls.Add(btnCreateAccount);

            Icon = (Icon?)resources.GetObject("$this.Icon");
            Name = "SignUpForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "BUCS Student Information System";

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCreateAccount;
        private Button btnGoToLogin;
        private TextBox txtFirstName;
        private TextBox txtStudentId;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private TextBox txtLastName;
        private TextBox txtEmail;
        private Button btnBack;
    }
}