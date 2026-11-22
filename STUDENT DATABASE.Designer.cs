namespace BUCS_SIS
{
    partial class STUDENT_DATABASE
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
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.BackColor = Color.Transparent;
            formsPlot1.BackgroundImage = Properties.Resources.CS_SIS_8_;
            formsPlot1.BackgroundImageLayout = ImageLayout.Center;
            formsPlot1.ForeColor = SystemColors.ButtonFace;
            formsPlot1.Location = new Point(26, 40);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(701, 449);
            formsPlot1.TabIndex = 0;
            // 
            // STUDENT_DATABASE
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.CS_SIS_8_;
            ClientSize = new Size(884, 501);
            Controls.Add(formsPlot1);
            Name = "STUDENT_DATABASE";
            Text = "BUCS Student Information System";
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot1;
    }
}