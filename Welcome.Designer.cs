namespace BUCS_SIS
{
    partial class Welcome
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            LOG_STUD = new Button();
            LOG_PROF = new Button();
            SuspendLayout();
            // 
            // LOG_STUD
            // 
            LOG_STUD.BackColor = Color.Transparent;
            LOG_STUD.BackgroundImage = Properties.Resources.TEXTBOX;
            LOG_STUD.FlatAppearance.BorderSize = 0;
            LOG_STUD.FlatStyle = FlatStyle.Flat;
            LOG_STUD.Font = new Font("Montserrat ExtraBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LOG_STUD.ForeColor = Color.White;
            LOG_STUD.Location = new Point(496, 283);
            LOG_STUD.Name = "LOG_STUD";
            LOG_STUD.Size = new Size(246, 60);
            LOG_STUD.TabIndex = 0;
            LOG_STUD.Text = "LOG IN AS STUDENT";
            LOG_STUD.TextImageRelation = TextImageRelation.TextAboveImage;
            LOG_STUD.UseVisualStyleBackColor = false;
            // 
            // LOG_PROF
            // 
            LOG_PROF.BackColor = Color.Transparent;
            LOG_PROF.BackgroundImage = Properties.Resources.TEXTBOX;
            LOG_PROF.FlatAppearance.BorderSize = 0;
            LOG_PROF.FlatStyle = FlatStyle.Flat;
            LOG_PROF.Font = new Font("Montserrat ExtraBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LOG_PROF.ForeColor = Color.White;
            LOG_PROF.Location = new Point(496, 407);
            LOG_PROF.Name = "LOG_PROF";
            LOG_PROF.Size = new Size(246, 60);
            LOG_PROF.TabIndex = 1;
            LOG_PROF.Text = "LOG IN AS PROFESSOR";
            LOG_PROF.TextImageRelation = TextImageRelation.TextAboveImage;
            LOG_PROF.UseVisualStyleBackColor = false;
            // 
            // Welcome
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.CS_SIS;
            ClientSize = new Size(884, 501);
            Controls.Add(LOG_PROF);
            Controls.Add(LOG_STUD);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Welcome";
            Text = "BUCS Student Information System";
            ResumeLayout(false);
        }

        #endregion

        private Button LOG_STUD;
        private Button LOG_PROF;
    }
}
