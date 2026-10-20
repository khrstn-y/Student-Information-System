namespace BUCS_SIS
{
    partial class StudentLoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentLoginForm));
            txtIdentity = new ReaLTaiizor.Controls.CyberTextBox();
            txtPassword = new ReaLTaiizor.Controls.CyberTextBox();
            btnLogin = new Button();
            btnSignUp = new Button();
            btnBack = new Button();
            SuspendLayout();
            // 
            // txtIdentity
            // 
            txtIdentity.Alpha = 20;
            txtIdentity.BackColor = Color.Transparent;
            txtIdentity.Background_WidthPen = 3F;
            txtIdentity.BackgroundPen = true;
            txtIdentity.ColorBackground = Color.White;
            txtIdentity.ColorBackground_Pen = Color.Transparent;
            txtIdentity.ColorLighting = Color.Transparent;
            txtIdentity.ColorPen_1 = Color.Transparent;
            txtIdentity.ColorPen_2 = Color.Transparent;
            txtIdentity.CyberTextBoxStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            txtIdentity.Font = new Font("Montserrat Light", 10F);
            txtIdentity.ForeColor = Color.Black;
            txtIdentity.Lighting = false;
            txtIdentity.LinearGradientPen = false;
            txtIdentity.Location = new Point(346, 251);
            txtIdentity.Name = "txtIdentity";
            txtIdentity.PenWidth = 15;
            txtIdentity.RGB = false;
            txtIdentity.Rounding = true;
            txtIdentity.RoundingInt = 60;
            txtIdentity.Size = new Size(200, 40);
            txtIdentity.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            txtIdentity.TabIndex = 0;
            txtIdentity.Tag = "Cyber";
            txtIdentity.TextButton = "";
            txtIdentity.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            txtIdentity.Timer_RGB = 300;
            // 
            // txtPassword
            // 
            txtPassword.Alpha = 20;
            txtPassword.BackColor = Color.Transparent;
            txtPassword.Background_WidthPen = 3F;
            txtPassword.BackgroundPen = true;
            txtPassword.ColorBackground = Color.White;
            txtPassword.ColorBackground_Pen = Color.Transparent;
            txtPassword.ColorLighting = Color.Transparent;
            txtPassword.ColorPen_1 = Color.Transparent;
            txtPassword.ColorPen_2 = Color.Transparent;
            txtPassword.CyberTextBoxStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            txtPassword.Font = new Font("Montserrat Light", 10F);
            txtPassword.ForeColor = Color.Black;
            txtPassword.Lighting = false;
            txtPassword.LinearGradientPen = false;
            txtPassword.Location = new Point(346, 340);
            txtPassword.Name = "txtPassword";
            txtPassword.PenWidth = 15;
            txtPassword.RGB = false;
            txtPassword.Rounding = true;
            txtPassword.RoundingInt = 60;
            txtPassword.Size = new Size(200, 40);
            txtPassword.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            txtPassword.TabIndex = 1;
            txtPassword.Tag = "Cyber";
            txtPassword.TextButton = "";
            txtPassword.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            txtPassword.Timer_RGB = 300;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.Transparent;
            btnLogin.BackgroundImage = (Image)resources.GetObject("btnLogin.BackgroundImage");
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Montserrat ExtraBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(271, 406);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(148, 47);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "LOG IN";
            btnLogin.TextImageRelation = TextImageRelation.TextAboveImage;
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += button1_Click;
            // 
            // btnSignUp
            // 
            btnSignUp.BackColor = Color.Transparent;
            btnSignUp.BackgroundImage = (Image)resources.GetObject("btnSignUp.BackgroundImage");
            btnSignUp.FlatAppearance.BorderSize = 0;
            btnSignUp.FlatStyle = FlatStyle.Flat;
            btnSignUp.Font = new Font("Montserrat ExtraBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSignUp.ForeColor = Color.White;
            btnSignUp.Location = new Point(466, 406);
            btnSignUp.Name = "btnSignUp";
            btnSignUp.Size = new Size(148, 47);
            btnSignUp.TabIndex = 4;
            btnSignUp.Text = "SIGN UP";
            btnSignUp.TextImageRelation = TextImageRelation.TextAboveImage;
            btnSignUp.UseVisualStyleBackColor = false;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.Transparent;
            btnBack.BackgroundImage = Properties.Resources.BACK_BUTTON;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Location = new Point(44, 425);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(38, 41);
            btnBack.TabIndex = 35;
            btnBack.UseVisualStyleBackColor = false;
            // 
            // StudentLoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(884, 501);
            Controls.Add(btnBack);
            Controls.Add(btnSignUp);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtIdentity);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "StudentLoginForm";
            Text = "BUCS Student Information System";
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.CyberTextBox txtIdentity;
        private ReaLTaiizor.Controls.CyberTextBox txtPassword;
        private Button btnLogin;
        private Button btnSignUp;
        private Button btnBack;
    }
}