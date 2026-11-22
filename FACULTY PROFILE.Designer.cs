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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FacultyProfileForm));
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
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)picProfessor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tablepanel).BeginInit();
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
            pnlName.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            pnlName.ForeColor = Color.White;
            pnlName.Location = new Point(238, 28);
            pnlName.Name = "pnlName";
            pnlName.Size = new Size(413, 44);
            pnlName.TabIndex = 1;
            // 
            // pnlBlock
            // 
            pnlBlock.BackColor = Color.Transparent;
            pnlBlock.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
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
            tablepanel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablepanel.BackgroundColor = Color.White;
            tablepanel.ColumnHeadersHeight = 34;
            tablepanel.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            tablepanel.Font = new Font("Segoe UI", 10F);
            tablepanel.Location = new Point(31, 153);
            tablepanel.MultiSelect = false;
            tablepanel.Name = "tablepanel";
            tablepanel.ReadOnly = true;
            tablepanel.RowHeadersVisible = false;
            tablepanel.RowTemplate.Height = 28;
            tablepanel.ScrollBars = ScrollBars.Vertical;
            tablepanel.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tablepanel.Size = new Size(726, 456);
            tablepanel.TabIndex = 4;
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
            // pnlEmail
            // 
            pnlEmail.BackColor = Color.Transparent;
            pnlEmail.Font = new Font("Segoe UI", 11F);
            pnlEmail.ForeColor = Color.White;
            pnlEmail.Location = new Point(238, 78);
            pnlEmail.Name = "pnlEmail";
            pnlEmail.Size = new Size(413, 30);
            pnlEmail.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.Transparent;
            btnAdd.BackgroundImage = (Image)resources.GetObject("btnAdd.BackgroundImage");
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Montserrat ExtraBold", 9.75F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(180, 665);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(148, 47);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "ADD STUDENT";
            btnAdd.TextImageRelation = TextImageRelation.TextAboveImage;
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDrop
            // 
            btnDrop.BackColor = Color.Transparent;
            btnDrop.BackgroundImage = (Image)resources.GetObject("btnDrop.BackgroundImage");
            btnDrop.BackgroundImageLayout = ImageLayout.Stretch;
            btnDrop.FlatAppearance.BorderSize = 0;
            btnDrop.FlatStyle = FlatStyle.Flat;
            btnDrop.Font = new Font("Montserrat ExtraBold", 9.75F, FontStyle.Bold);
            btnDrop.ForeColor = Color.White;
            btnDrop.Location = new Point(355, 665);
            btnDrop.Name = "btnDrop";
            btnDrop.Size = new Size(148, 47);
            btnDrop.TabIndex = 9;
            btnDrop.Text = "DROP STUDENT";
            btnDrop.TextImageRelation = TextImageRelation.TextAboveImage;
            btnDrop.UseVisualStyleBackColor = false;
            btnDrop.Click += btnDrop_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.Transparent;
            btnSave.BackgroundImage = (Image)resources.GetObject("btnSave.BackgroundImage");
            btnSave.BackgroundImageLayout = ImageLayout.Stretch;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Montserrat ExtraBold", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(536, 665);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(148, 47);
            btnSave.TabIndex = 10;
            btnSave.Text = "SAVE CHANGES";
            btnSave.TextImageRelation = TextImageRelation.TextAboveImage;
            btnSave.UseVisualStyleBackColor = false;
            // 
            // txtName
            // 
            txtName.BackColor = Color.WhiteSmoke;
            txtName.Font = new Font("Montserrat Light", 11.25F);
            txtName.ForeColor = Color.Black;
            txtName.Location = new Point(337, 625);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "Student name...";
            txtName.ReadOnly = true;
            txtName.Size = new Size(420, 26);
            txtName.TabIndex = 7;
            txtName.TabStop = false;
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.FlatStyle = FlatStyle.Flat;
            cmbStatus.Font = new Font("Montserrat Light", 9F);
            cmbStatus.ForeColor = Color.Black;
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(180, 625);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(147, 26);
            cmbStatus.TabIndex = 6;
            // 
            // txtStudentId
            // 
            txtStudentId.Font = new Font("Montserrat Light", 11.25F);
            txtStudentId.ForeColor = Color.Black;
            txtStudentId.Location = new Point(31, 625);
            txtStudentId.MaxLength = 50;
            txtStudentId.Name = "txtStudentId";
            txtStudentId.PlaceholderText = "Student ID...";
            txtStudentId.Size = new Size(139, 26);
            txtStudentId.TabIndex = 5;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Montserrat ExtraBold", 8.249999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(42, 665);
            button1.Name = "button1";
            button1.Size = new Size(108, 47);
            button1.TabIndex = 12;
            button1.Text = "VIEW GRAPH";
            button1.TextImageRelation = TextImageRelation.TextAboveImage;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // FacultyProfileForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(25, 30, 110);
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(884, 731);
            Controls.Add(button1);
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
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FacultyProfileForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BUCS Student Information System - Faculty Profile";
            Load += FacultyProfileForm_Load;
            ((System.ComponentModel.ISupportInitialize)picProfessor).EndInit();
            ((System.ComponentModel.ISupportInitialize)tablepanel).EndInit();
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
        private Button button1;
    }
}