using System;
using System.Windows.Forms;
using BUCS_SIS;

namespace Student_Information_System
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();

            LOG_STUD.Click += LOG_STUD_Click;
            LOG_PROF.Click += LOG_PROF_Click;
        }

        private void LOG_STUD_Click(object sender, EventArgs e)
        {
            using (StudentLoginForm loginForm = new StudentLoginForm())
            {
                OpenLogin(loginForm);
            }
        }

        private void LOG_PROF_Click(object sender, EventArgs e)
        {
            using (TeacherLoginForm loginForm = new TeacherLoginForm())
            {
                OpenLogin(loginForm);
            }
        }

        private void OpenLogin(Form loginForm)
        {
            Hide();

            try
            {
                loginForm.StartPosition = FormStartPosition.CenterParent;
                loginForm.ShowDialog(this);
            }
            finally
            {
                Show();
            }
        }
    }
}