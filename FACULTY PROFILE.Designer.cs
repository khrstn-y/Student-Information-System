#nullable disable

using System.Drawing;
using System.Windows.Forms;

namespace BUCS_SIS
{
    partial class FacultyProfileForm
    {
        private System.ComponentModel.IContainer components = null;

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
                    typeof(FacultyProfileForm));

            picProfessor = new PictureBox();
            pnlName = new Panel();
            pnlBlock = new Panel();
            tablepanel = new DataGridView();
            btnBack = new Button();
            pnlEmail = new Panel();

            btnAdd = new Button();
            btnDrop = new Button();
            btnSave = new Button();

            txtName = new TextBox();
            cmbStatus = new ComboBox();
            txtStudentId = new TextBox();

            ((System.ComponentModel.ISupportInitialize)picProfessor)
                .BeginInit();

            ((System.ComponentModel.ISupportInitialize)tablepanel)
                .BeginInit();

            SuspendLayout();

            //
            // picProfessor
            //
            picProfessor.BackColor = Color.White;
            picProfessor.BorderStyle = BorderStyle.FixedSingle;
            picProfessor.Location = new Point(657, 12);
            picProfessor.Name = "picProfessor";
            picProfessor.Size = new Size(100, 100);
            picProfessor.SizeMode = PictureBoxSizeMode.Zoom;
            picProfessor.TabIndex = 0;
            picProfessor.TabStop = false;

            //
            // pnlName
            //
            pnlName.BackColor = Color.Transparent;

            pnlName.Font = new Font(
                "Segoe UI",
                18F,
                FontStyle.Bold,
                GraphicsUnit.Point);

            pnlName.ForeColor = Color.White;
            pnlName.Location = new Point(238, 28);
            pnlName.Name = "pnlName";
            pnlName.Size = new Size(413, 44);
            pnlName.TabIndex = 1;

            //
            // pnlEmail
            //
            pnlEmail.BackColor = Color.Transparent;

            pnlEmail.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Regular,
                GraphicsUnit.Point);

            pnlEmail.ForeColor = Color.White;
            pnlEmail.Location = new Point(238, 78);
            pnlEmail.Name = "pnlEmail";
            pnlEmail.Size = new Size(413, 30);
            pnlEmail.TabIndex = 2;

            //
            // pnlBlock
            //
            pnlBlock.BackColor = Color.Transparent;

            pnlBlock.Font = new Font(
                "Segoe UI",
                12F,
                FontStyle.Bold,
                GraphicsUnit.Point);

            pnlBlock.ForeColor = Color.White;
            pnlBlock.Location = new Point(31, 117);
            pnlBlock.Name = "pnlBlock";
            pnlBlock.Size = new Size(726, 30);
            pnlBlock.TabIndex = 3;

            //
            // tablepanel
            //
            tablepanel.AllowUserToAddRows = false;
            tablepanel.AllowUserToDeleteRows = false;
            tablepanel.AllowUserToResizeRows = false;

            // Columns are created by ConfigureControls()
            // in FACULTY PROFILE.cs.
            tablepanel.AutoGenerateColumns = false;

            tablepanel.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            tablepanel.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            tablepanel.BackgroundColor = Color.White;
            tablepanel.BorderStyle = BorderStyle.FixedSingle;
            tablepanel.ColumnHeadersHeight = 34;

            tablepanel.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            tablepanel.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular,
                GraphicsUnit.Point);

            tablepanel.ForeColor = Color.Black;
            tablepanel.Location = new Point(31, 153);
            tablepanel.MultiSelect = false;
            tablepanel.Name = "tablepanel";
            tablepanel.ReadOnly = true;
            tablepanel.RowHeadersVisible = false;
            tablepanel.RowTemplate.Height = 28;
            tablepanel.ScrollBars = ScrollBars.Vertical;

            tablepanel.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            // 34-pixel header + 15 rows at 28 pixels + borders.
            tablepanel.Size = new Size(726, 456);
            tablepanel.TabIndex = 4;

            //
            // txtStudentId
            //
            txtStudentId.Font = new Font(
                "Montserrat Light",
                11.25F,
                FontStyle.Regular,
                GraphicsUnit.Point);

            txtStudentId.ForeColor = Color.Black;
            txtStudentId.Location = new Point(31, 625);
            txtStudentId.MaxLength = 50;
            txtStudentId.Name = "txtStudentId";
            txtStudentId.PlaceholderText = "Student ID...";
            txtStudentId.Size = new Size(139, 27);
            txtStudentId.TabIndex = 5;

            //
            // cmbStatus
            //
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.FlatStyle = FlatStyle.Flat;

            cmbStatus.Font = new Font(
                "Montserrat Light",
                9F,
                FontStyle.Regular,
                GraphicsUnit.Point);

            cmbStatus.ForeColor = Color.Black;
            cmbStatus.FormattingEnabled = true;

            // Items are populated by ConfigureControls()
            // as Regular, Irregular, and LOA.
            cmbStatus.Location = new Point(180, 625);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(147, 26);
            cmbStatus.TabIndex = 6;

            //
            // txtName
            //
            txtName.BackColor = Color.WhiteSmoke;

            txtName.Font = new Font(
                "Montserrat Light",
                11.25F,
                FontStyle.Regular,
                GraphicsUnit.Point);

            txtName.ForeColor = Color.Black;
            txtName.Location = new Point(337, 625);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Student name...";
            txtName.ReadOnly = true;
            txtName.Size = new Size(420, 27);
            txtName.TabIndex = 7;
            txtName.TabStop = false;

            //
            // btnAdd
            //
            btnAdd.BackColor = Color.Transparent;

            btnAdd.BackgroundImage =
                (Image)resources.GetObject("btnAdd.BackgroundImage");

            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;

            btnAdd.Font = new Font(
                "Montserrat ExtraBold",
                9.75F,
                FontStyle.Bold,
                GraphicsUnit.Point);

            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(136, 665);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(148, 47);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "ADD STUDENT";
            btnAdd.TextImageRelation = TextImageRelation.TextAboveImage;
            btnAdd.UseVisualStyleBackColor = false;

            //
            // btnDrop
            //
            btnDrop.BackColor = Color.Transparent;

            btnDrop.BackgroundImage =
                (Image)resources.GetObject("btnDrop.BackgroundImage");

            btnDrop.BackgroundImageLayout = ImageLayout.Stretch;
            btnDrop.FlatAppearance.BorderSize = 0;
            btnDrop.FlatStyle = FlatStyle.Flat;

            btnDrop.Font = new Font(
                "Montserrat ExtraBold",
                9.75F,
                FontStyle.Bold,
                GraphicsUnit.Point);

            btnDrop.ForeColor = Color.White;
            btnDrop.Location = new Point(340, 665);
            btnDrop.Name = "btnDrop";
            btnDrop.Size = new Size(148, 47);
            btnDrop.TabIndex = 9;
            btnDrop.Text = "DROP STUDENT";
            btnDrop.TextImageRelation = TextImageRelation.TextAboveImage;
            btnDrop.UseVisualStyleBackColor = false;

            //
            // btnSave
            //
            btnSave.BackColor = Color.Transparent;

            btnSave.BackgroundImage =
                (Image)resources.GetObject("btnSave.BackgroundImage");

            btnSave.BackgroundImageLayout = ImageLayout.Stretch;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;

            btnSave.Font = new Font(
                "Montserrat ExtraBold",
                10F,
                FontStyle.Bold,
                GraphicsUnit.Point);

            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(536, 665);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(148, 47);
            btnSave.TabIndex = 10;
            btnSave.Text = "SAVE CHANGES";
            btnSave.TextImageRelation = TextImageRelation.TextAboveImage;
            btnSave.UseVisualStyleBackColor = false;

            //
            // btnBack
            //
            btnBack.BackColor = Color.Transparent;
            btnBack.BackgroundImage = Properties.Resources.BACK_BUTTON;
            btnBack.BackgroundImageLayout = ImageLayout.Zoom;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Location = new Point(21, 12);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(39, 41);
            btnBack.TabIndex = 11;
            btnBack.UseVisualStyleBackColor = false;

            //
            // FacultyProfileForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;

            AutoScroll = true;

            BackColor = Color.FromArgb(25, 30, 110);

            BackgroundImage =
                (Image)resources.GetObject("$this.BackgroundImage");

            BackgroundImageLayout = ImageLayout.Stretch;

            ClientSize = new Size(884, 731);

            Controls.Add(txtStudentId);
            Controls.Add(cmbStatus);
            Controls.Add(txtName);

            Controls.Add(btnSave);
            Controls.Add(btnDrop);
            Controls.Add(btnAdd);

            Controls.Add(pnlEmail);
            Controls.Add(btnBack);
            Controls.Add(tablepanel);
            Controls.Add(pnlBlock);
            Controls.Add(pnlName);
            Controls.Add(picProfessor);

            Icon = (Icon)resources.GetObject("$this.Icon");

            Name = "FacultyProfileForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BUCS Student Information System - Faculty Profile";

            ((System.ComponentModel.ISupportInitialize)picProfessor)
                .EndInit();

            ((System.ComponentModel.ISupportInitialize)tablepanel)
                .EndInit();

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picProfessor;

        private Panel pnlName;
        private Panel pnlBlock;
        private Panel pnlEmail;

        private DataGridView tablepanel;

        private Button btnBack;
        private Button btnAdd;
        private Button btnDrop;
        private Button btnSave;

        private TextBox txtName;
        private ComboBox cmbStatus;
        private TextBox txtStudentId;
    }
}