#nullable enable

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BUCS_SIS
{
    partial class FacultyProfileForm
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                roster.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            var resources = new ComponentResourceManager(typeof(FacultyProfileForm));
            btnBack = new Button();
            btnBack.Name = "btnBack";
            btnBack.Location = new Point(24, 17);
            btnBack.Size = new Size(80, 34);
            btnBack.Text = "Back";
            btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnBack.BackColor = Color.FromArgb(47, 47, 97);
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderColor = Color.FromArgb(117, 121, 173);
            btnBack.UseVisualStyleBackColor = false;
            btnBack.TabIndex = 8;

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FacultyProfileForm));
            picProfessor = new PictureBox();
            pnlName = new Panel();
            pnlName.Name = "pnlName";
            pnlName.Location = new Point(123, 20);
            pnlName.Size = new Size(450, 36);
            pnlName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlName.ForeColor = Color.White;
            pnlName.BackColor = Color.Transparent;

            pnlEmail = new Panel();
            picProfessor = new PictureBox();
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
            picProfessor.Location = new Point(617, 16);
            picProfessor.Size = new Size(100, 100);
            picProfessor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picProfessor.SizeMode = PictureBoxSizeMode.Zoom;
            picProfessor.BorderStyle = BorderStyle.FixedSingle;
            picProfessor.TabStop = false;

            pnlBlock = new Panel();
            pnlBlock.Name = "pnlBlock";
            pnlBlock.Location = new Point(24, 119);
            pnlBlock.Size = new Size(693, 31);
            pnlBlock.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlBlock.ForeColor = Color.White;
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

            lblSearchStudent = new Label();
            lblSearchStudent.Name = "lblSearchStudent";
            lblSearchStudent.Location = new Point(24, 158);
            lblSearchStudent.Size = new Size(140, 21);
            lblSearchStudent.Text = "Search students";
            lblSearchStudent.ForeColor = Color.FromArgb(221, 225, 247);
            lblSearchStudent.BackColor = Color.Transparent;
            lblSearchStudent.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            lblSearchStudent.TextAlign = ContentAlignment.MiddleLeft;

            txtSearchStudent = new TextBox();
            txtSearchStudent.Name = "txtSearchStudent";
            txtSearchStudent.Location = new Point(24, 182);
            txtSearchStudent.Size = new Size(410, 27);
            txtSearchStudent.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearchStudent.PlaceholderText = "Student ID or name";
            txtSearchStudent.AccessibleName = "Search students by ID or name";
            txtSearchStudent.TabIndex = 0;

            btnClearSearch = new Button();
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Location = new Point(442, 179);
            btnClearSearch.Size = new Size(73, 31);
            btnClearSearch.Text = "Clear";
            btnClearSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearSearch.BackColor = Color.FromArgb(47, 47, 97);
            btnClearSearch.ForeColor = Color.White;
            btnClearSearch.FlatStyle = FlatStyle.Flat;
            btnClearSearch.FlatAppearance.BorderColor = Color.FromArgb(117, 121, 173);
            btnClearSearch.UseVisualStyleBackColor = false;
            btnClearSearch.TabIndex = 1;

            lblSearchCount = new Label();
            lblSearchCount.Name = "lblSearchCount";
            lblSearchCount.Location = new Point(521, 181);
            lblSearchCount.Size = new Size(196, 27);
            lblSearchCount.Text = "";
            lblSearchCount.ForeColor = Color.FromArgb(221, 225, 247);
            lblSearchCount.BackColor = Color.Transparent;
            lblSearchCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSearchCount.TextAlign = ContentAlignment.MiddleRight;

            tablepanel = new DataGridView();
            tablepanel.Name = "tablepanel";
            tablepanel.Location = new Point(24, 222);
            tablepanel.Size = new Size(693, 283);
            tablepanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            tablepanel.BackgroundColor = Color.FromArgb(234, 237, 248);
            tablepanel.BorderStyle = BorderStyle.FixedSingle;
            tablepanel.GridColor = Color.FromArgb(217, 221, 240);
            tablepanel.TabIndex = 2;

            lblStudentId = new Label();
            lblStudentId.Name = "lblStudentId";
            lblStudentId.Location = new Point(24, 518);
            lblStudentId.Size = new Size(150, 20);
            lblStudentId.Text = "Student ID";
            lblStudentId.ForeColor = Color.FromArgb(221, 225, 247);
            lblStudentId.BackColor = Color.Transparent;
            lblStudentId.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStudentId.TextAlign = ContentAlignment.MiddleLeft;

            lblStatus = new Label();
            lblStatus.Name = "lblStatus";
            lblStatus.Location = new Point(192, 518);
            lblStatus.Size = new Size(170, 20);
            lblStatus.Text = "Enrollment status";
            lblStatus.ForeColor = Color.FromArgb(221, 225, 247);
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;

            lblStudentName = new Label();
            lblStudentName.Name = "lblStudentName";
            lblStudentName.Location = new Point(373, 518);
            lblStudentName.Size = new Size(200, 20);
            lblStudentName.Text = "Student name";
            lblStudentName.ForeColor = Color.FromArgb(221, 225, 247);
            lblStudentName.BackColor = Color.Transparent;
            lblStudentName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStudentName.TextAlign = ContentAlignment.MiddleLeft;

            txtStudentId = new TextBox();
            txtStudentId.Name = "txtStudentId";
            txtStudentId.Location = new Point(24, 541);
            txtStudentId.Size = new Size(154, 26);
            txtStudentId.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtStudentId.TabIndex = 3;

            cmbStatus = new ComboBox();
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Location = new Point(192, 541);
            cmbStatus.Size = new Size(167, 26);
            cmbStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmbStatus.TabIndex = 4;

            txtName = new TextBox();
            txtName.Name = "txtName";
            txtName.Location = new Point(373, 541);
            txtName.Size = new Size(344, 26);
            txtName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtName.TabStop = false;

            btnAdd = new Button();
            btnAdd.Name = "btnAdd";
            btnAdd.Location = new Point(24, 582);
            btnAdd.Size = new Size(134, 37);
            btnAdd.Text = "Add Student";
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAdd.BackColor = Color.FromArgb(47, 47, 97);
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
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderColor = Color.FromArgb(117, 121, 173);
            btnAdd.Location = new Point(180, 665);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(148, 47);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "ADD STUDENT";
            btnAdd.TextImageRelation = TextImageRelation.TextAboveImage;
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.TabIndex = 5;

            btnDrop = new Button();
            btnDrop.Name = "btnDrop";
            btnDrop.Location = new Point(169, 582);
            btnDrop.Size = new Size(134, 37);
            btnDrop.Text = "Drop Student";
            btnDrop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDrop.BackColor = Color.FromArgb(47, 47, 97);
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
            btnDrop.FlatStyle = FlatStyle.Flat;
            btnDrop.FlatAppearance.BorderColor = Color.FromArgb(117, 121, 173);
            btnDrop.Location = new Point(355, 665);
            btnDrop.Name = "btnDrop";
            btnDrop.Size = new Size(148, 47);
            btnDrop.TabIndex = 9;
            btnDrop.Text = "DROP STUDENT";
            btnDrop.TextImageRelation = TextImageRelation.TextAboveImage;
            btnDrop.UseVisualStyleBackColor = false;
            btnDrop.TabIndex = 6;

            btnSave = new Button();
            btnSave.Name = "btnSave";
            btnSave.Location = new Point(314, 582);
            btnSave.Size = new Size(147, 37);
            btnSave.Text = "Save Changes";
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSave.BackColor = Color.FromArgb(78, 88, 166);
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
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderColor = Color.FromArgb(117, 121, 173);
            btnSave.UseVisualStyleBackColor = false;
            btnSave.TabIndex = 7;

            btnEnrollmentChart = new Button();
            btnEnrollmentChart.Name = "btnEnrollmentChart";
            btnEnrollmentChart.Location = new Point(472, 582);
            btnEnrollmentChart.Size = new Size(171, 37);
            btnEnrollmentChart.Text = "Enrollment Chart";
            btnEnrollmentChart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEnrollmentChart.BackColor = Color.FromArgb(47, 47, 97);
            btnEnrollmentChart.ForeColor = Color.White;
            btnEnrollmentChart.FlatStyle = FlatStyle.Flat;
            btnEnrollmentChart.FlatAppearance.BorderColor = Color.FromArgb(117, 121, 173);
            btnEnrollmentChart.UseVisualStyleBackColor = false;
            btnEnrollmentChart.TabIndex = 9;

            SuspendLayout();
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
            ClientSize = new Size(844, 642);
            MinimumSize = new Size(800, 560);
            Font = new Font("Segoe UI", 9F);
            ResizeRedraw = true;
            DoubleBuffered = true;
            StartPosition = FormStartPosition.CenterScreen;
            facultyBackdrop = resources.GetObject("$this.BackgroundImage") as Image;
            Icon = resources.GetObject("$this.Icon") as Icon;
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
            Text = "FACULTY PROFILE";
            Controls.AddRange(new Control[]
            {
                btnBack, pnlName, pnlEmail, picProfessor, pnlBlock,
                lblSearchStudent, txtSearchStudent, btnClearSearch, lblSearchCount,
                tablepanel, lblStudentId, lblStatus, lblStudentName, txtStudentId,
                cmbStatus, txtName, btnAdd, btnDrop, btnSave, btnEnrollmentChart
            });
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BUCS Student Information System - Faculty Profile";
            Load += FacultyProfileForm_Load;
            ((System.ComponentModel.ISupportInitialize)picProfessor).EndInit();
            ((System.ComponentModel.ISupportInitialize)tablepanel).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Image? facultyBackdrop;
        private Button btnBack = null!;
        private Panel pnlName = null!;
        private Panel pnlEmail = null!;
        private PictureBox picProfessor = null!;
        private Panel pnlBlock = null!;
        private Label lblSearchStudent = null!;
        private TextBox txtSearchStudent = null!;
        private Button btnClearSearch = null!;
        private Label lblSearchCount = null!;
        private DataGridView tablepanel = null!;
        private Label lblStudentId = null!;
        private Label lblStatus = null!;
        private Label lblStudentName = null!;
        private TextBox txtStudentId = null!;
        private ComboBox cmbStatus = null!;
        private TextBox txtName = null!;
        private Button btnAdd = null!;
        private Button btnDrop = null!;
        private Button btnSave = null!;
        private Button btnEnrollmentChart = null!;
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
