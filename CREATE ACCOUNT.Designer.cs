namespace BUCS_SIS
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            btnCreateAccount = new Button();
            btnGoToLogin = new Button();
            txtFirstName = new ReaLTaiizor.Controls.CyberTextBox();
            txtStudentId = new ReaLTaiizor.Controls.CyberTextBox();
            txtPassword = new ReaLTaiizor.Controls.CyberTextBox();
            txtConfirmPassword = new ReaLTaiizor.Controls.CyberTextBox();
            txtLastName = new ReaLTaiizor.Controls.CyberTextBox();
            txtEmail = new ReaLTaiizor.Controls.CyberTextBox();
            btnBack = new Button();
            SuspendLayout();
            // 
            // btnCreateAccount
            // 
            btnCreateAccount.BackColor = Color.Transparent;
            btnCreateAccount.BackgroundImage = (Image)resources.GetObject("btnCreateAccount.BackgroundImage");
            btnCreateAccount.FlatAppearance.BorderSize = 0;
            btnCreateAccount.FlatStyle = FlatStyle.Flat;
            btnCreateAccount.Font = new Font("Montserrat ExtraBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCreateAccount.ForeColor = Color.White;
            btnCreateAccount.Location = new Point(465, 329);
            btnCreateAccount.Name = "btnCreateAccount";
            btnCreateAccount.Size = new Size(197, 46);
            btnCreateAccount.TabIndex = 2;
            btnCreateAccount.Text = "CREATE ACCOUNT";
            btnCreateAccount.TextImageRelation = TextImageRelation.TextAboveImage;
            btnCreateAccount.UseVisualStyleBackColor = false;
            btnCreateAccount.Click += button2_Click;
            // 
            // btnGoToLogin
            // 
            btnGoToLogin.BackColor = Color.Transparent;
            btnGoToLogin.BackgroundImage = (Image)resources.GetObject("btnGoToLogin.BackgroundImage");
            btnGoToLogin.FlatAppearance.BorderSize = 0;
            btnGoToLogin.FlatStyle = FlatStyle.Flat;
            btnGoToLogin.Font = new Font("Montserrat ExtraBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGoToLogin.ForeColor = Color.White;
            btnGoToLogin.Location = new Point(485, 399);
            btnGoToLogin.Name = "btnGoToLogin";
            btnGoToLogin.Size = new Size(157, 37);
            btnGoToLogin.TabIndex = 4;
            btnGoToLogin.Text = "LOG IN HERE";
            btnGoToLogin.TextImageRelation = TextImageRelation.TextAboveImage;
            btnGoToLogin.UseVisualStyleBackColor = false;
            // 
            // txtFirstName
            // 
            txtFirstName.Alpha = 20;
            txtFirstName.BackColor = Color.Transparent;
            txtFirstName.Background_WidthPen = 3F;
            txtFirstName.BackgroundPen = true;
            txtFirstName.ColorBackground = Color.White;
            txtFirstName.ColorBackground_Pen = Color.Transparent;
            txtFirstName.ColorLighting = Color.Transparent;
            txtFirstName.ColorPen_1 = Color.Transparent;
            txtFirstName.ColorPen_2 = Color.Transparent;
            txtFirstName.CyberTextBoxStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            txtFirstName.Font = new Font("Montserrat Light", 7F);
            txtFirstName.ForeColor = Color.Black;
            txtFirstName.Lighting = false;
            txtFirstName.LinearGradientPen = false;
            txtFirstName.Location = new Point(380, 112);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.PenWidth = 15;
            txtFirstName.RGB = false;
            txtFirstName.Rounding = true;
            txtFirstName.RoundingInt = 60;
            txtFirstName.Size = new Size(177, 30);
            txtFirstName.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            txtFirstName.TabIndex = 5;
            txtFirstName.Tag = "Cyber";
            txtFirstName.TextButton = "";
            txtFirstName.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            txtFirstName.Timer_RGB = 300;
            txtFirstName.Load += cyberTextBox1_Load;
            // 
            // txtStudentId
            // 
            txtStudentId.Alpha = 20;
            txtStudentId.BackColor = Color.Transparent;
            txtStudentId.Background_WidthPen = 3F;
            txtStudentId.BackgroundPen = true;
            txtStudentId.ColorBackground = Color.White;
            txtStudentId.ColorBackground_Pen = Color.Transparent;
            txtStudentId.ColorLighting = Color.Transparent;
            txtStudentId.ColorPen_1 = Color.Transparent;
            txtStudentId.ColorPen_2 = Color.Transparent;
            txtStudentId.CyberTextBoxStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            txtStudentId.Font = new Font("Montserrat Light", 7F);
            txtStudentId.ForeColor = Color.Black;
            txtStudentId.Lighting = false;
            txtStudentId.LinearGradientPen = false;
            txtStudentId.Location = new Point(380, 169);
            txtStudentId.Name = "txtStudentId";
            txtStudentId.PenWidth = 15;
            txtStudentId.RGB = false;
            txtStudentId.Rounding = true;
            txtStudentId.RoundingInt = 60;
            txtStudentId.Size = new Size(177, 30);
            txtStudentId.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            txtStudentId.TabIndex = 6;
            txtStudentId.Tag = "Cyber";
            txtStudentId.TextButton = "";
            txtStudentId.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            txtStudentId.Timer_RGB = 300;
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
            txtPassword.Font = new Font("Montserrat Light", 7F);
            txtPassword.ForeColor = Color.Black;
            txtPassword.Lighting = false;
            txtPassword.LinearGradientPen = false;
            txtPassword.Location = new Point(380, 221);
            txtPassword.Name = "txtPassword";
            txtPassword.PenWidth = 15;
            txtPassword.RGB = false;
            txtPassword.Rounding = true;
            txtPassword.RoundingInt = 60;
            txtPassword.Size = new Size(375, 30);
            txtPassword.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            txtPassword.TabIndex = 7;
            txtPassword.Tag = "Cyber";
            txtPassword.TextButton = "";
            txtPassword.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            txtPassword.Timer_RGB = 300;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Alpha = 20;
            txtConfirmPassword.BackColor = Color.Transparent;
            txtConfirmPassword.Background_WidthPen = 3F;
            txtConfirmPassword.BackgroundPen = true;
            txtConfirmPassword.ColorBackground = Color.White;
            txtConfirmPassword.ColorBackground_Pen = Color.Transparent;
            txtConfirmPassword.ColorLighting = Color.Transparent;
            txtConfirmPassword.ColorPen_1 = Color.Transparent;
            txtConfirmPassword.ColorPen_2 = Color.Transparent;
            txtConfirmPassword.CyberTextBoxStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            txtConfirmPassword.Font = new Font("Montserrat Light", 7F);
            txtConfirmPassword.ForeColor = Color.Black;
            txtConfirmPassword.Lighting = false;
            txtConfirmPassword.LinearGradientPen = false;
            txtConfirmPassword.Location = new Point(380, 273);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PenWidth = 15;
            txtConfirmPassword.RGB = false;
            txtConfirmPassword.Rounding = true;
            txtConfirmPassword.RoundingInt = 60;
            txtConfirmPassword.Size = new Size(375, 30);
            txtConfirmPassword.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            txtConfirmPassword.TabIndex = 8;
            txtConfirmPassword.Tag = "Cyber";
            txtConfirmPassword.TextButton = "";
            txtConfirmPassword.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            txtConfirmPassword.Timer_RGB = 300;
            // 
            // txtLastName
            // 
            txtLastName.Alpha = 20;
            txtLastName.BackColor = Color.Transparent;
            txtLastName.Background_WidthPen = 3F;
            txtLastName.BackgroundPen = true;
            txtLastName.ColorBackground = Color.White;
            txtLastName.ColorBackground_Pen = Color.Transparent;
            txtLastName.ColorLighting = Color.Transparent;
            txtLastName.ColorPen_1 = Color.Transparent;
            txtLastName.ColorPen_2 = Color.Transparent;
            txtLastName.CyberTextBoxStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            txtLastName.Font = new Font("Montserrat Light", 7F);
            txtLastName.ForeColor = Color.Black;
            txtLastName.Lighting = false;
            txtLastName.LinearGradientPen = false;
            txtLastName.Location = new Point(578, 112);
            txtLastName.Name = "txtLastName";
            txtLastName.PenWidth = 15;
            txtLastName.RGB = false;
            txtLastName.Rounding = true;
            txtLastName.RoundingInt = 60;
            txtLastName.Size = new Size(177, 30);
            txtLastName.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            txtLastName.TabIndex = 9;
            txtLastName.Tag = "Cyber";
            txtLastName.TextButton = "";
            txtLastName.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            txtLastName.Timer_RGB = 300;
            // 
            // txtEmail
            // 
            txtEmail.Alpha = 20;
            txtEmail.BackColor = Color.Transparent;
            txtEmail.Background_WidthPen = 3F;
            txtEmail.BackgroundPen = true;
            txtEmail.ColorBackground = Color.White;
            txtEmail.ColorBackground_Pen = Color.Transparent;
            txtEmail.ColorLighting = Color.Transparent;
            txtEmail.ColorPen_1 = Color.Transparent;
            txtEmail.ColorPen_2 = Color.Transparent;
            txtEmail.CyberTextBoxStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            txtEmail.Font = new Font("Montserrat Light", 7F);
            txtEmail.ForeColor = Color.Black;
            txtEmail.Lighting = false;
            txtEmail.LinearGradientPen = false;
            txtEmail.Location = new Point(578, 169);
            txtEmail.Name = "txtEmail";
            txtEmail.PenWidth = 15;
            txtEmail.RGB = false;
            txtEmail.Rounding = true;
            txtEmail.RoundingInt = 60;
            txtEmail.Size = new Size(177, 30);
            txtEmail.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            txtEmail.TabIndex = 10;
            txtEmail.Tag = "Cyber";
            txtEmail.TextButton = "";
            txtEmail.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            txtEmail.Timer_RGB = 300;
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
            btnBack.TabIndex = 35;
            btnBack.UseVisualStyleBackColor = false;
            // 
            // Form3
            // 
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
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form3";
            Text = "BUCS Student Information System";
            ResumeLayout(false);
        }

        #endregion

        private Button btnCreateAccount;
        private Button btnGoToLogin;
        private ReaLTaiizor.Controls.CyberTextBox txtFirstName;
        private ReaLTaiizor.Controls.CyberTextBox txtStudentId;
        private ReaLTaiizor.Controls.CyberTextBox txtPassword;
        private ReaLTaiizor.Controls.CyberTextBox txtConfirmPassword;
        private ReaLTaiizor.Controls.CyberTextBox txtLastName;
        private ReaLTaiizor.Controls.CyberTextBox txtEmail;
        private Button btnBack;
    }
}