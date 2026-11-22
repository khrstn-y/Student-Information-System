using System.Drawing;
using System.Windows.Forms;

namespace BUCS_SIS
{
    partial class StudentLoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentLoginForm));
            txtIdentity = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnSignUp = new Button();
            btnBack = new Button();
            SuspendLayout();
            // 
            // txtIdentity
            // 
            txtIdentity.BackColor = Color.White;
            txtIdentity.BorderStyle = BorderStyle.FixedSingle;
            txtIdentity.Font = new Font("Montserrat Light", 10F);
            txtIdentity.ForeColor = Color.Black;
            txtIdentity.Location = new Point(346, 251);
            txtIdentity.Name = "txtIdentity";
            txtIdentity.Size = new Size(200, 24);
            txtIdentity.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Montserrat Light", 10F);
            txtPassword.ForeColor = Color.Black;
            txtPassword.Location = new Point(346, 340);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(200, 24);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.Transparent;
            btnLogin.BackgroundImage = (Image)resources.GetObject("btnLogin.BackgroundImage");
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Montserrat ExtraBold", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(271, 406);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(148, 47);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "LOG IN";
            btnLogin.TextImageRelation = TextImageRelation.TextAboveImage;
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // btnSignUp
            // 
            btnSignUp.BackColor = Color.Transparent;
            btnSignUp.BackgroundImage = (Image)resources.GetObject("btnSignUp.BackgroundImage");
            btnSignUp.FlatAppearance.BorderSize = 0;
            btnSignUp.FlatStyle = FlatStyle.Flat;
            btnSignUp.Font = new Font("Montserrat ExtraBold", 12F, FontStyle.Bold);
            btnSignUp.ForeColor = Color.White;
            btnSignUp.Location = new Point(466, 406);
            btnSignUp.Name = "btnSignUp";
            btnSignUp.Size = new Size(148, 47);
            btnSignUp.TabIndex = 3;
            btnSignUp.Text = "SIGN UP";
            btnSignUp.TextImageRelation = TextImageRelation.TextAboveImage;
            btnSignUp.UseVisualStyleBackColor = false;
            // 
            // btnBack
            // 
            btnBack.AccessibleName = "Back";
            btnBack.BackColor = Color.Transparent;
            btnBack.BackgroundImage = Properties.Resources.BACK_BUTTON;
            btnBack.CausesValidation = false;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Location = new Point(44, 425);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(38, 41);
            btnBack.TabIndex = 4;
            btnBack.UseVisualStyleBackColor = false;
            // 
            // StudentLoginForm
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            CancelButton = btnBack;
            ClientSize = new Size(884, 501);
            Controls.Add(btnBack);
            Controls.Add(btnSignUp);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtIdentity);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "StudentLoginForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "BUCS Student Information System";
            Load += StudentLoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtIdentity;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnSignUp;
        private Button btnBack;
    }
}