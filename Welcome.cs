using System;
using System.Windows.Forms;
using BUCS_SIS;

namespace BUCS_SIS
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();

            LOG_STUD.Click += LOG_STUD_Click;
            LOG_PROF.Click += LOG_PROF_Click;
        }

        private void LOG_STUD_Click(object? sender, EventArgs e)
        {
            using (StudentLoginForm loginForm = new StudentLoginForm())
            {
                OpenLogin(loginForm);
            }
        }

        private void LOG_PROF_Click(object? sender, EventArgs e)
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