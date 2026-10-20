#nullable enable

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BUCS_SIS;

namespace BUCS_SIS
{
    public partial class PROFILE : Form
    {
        private readonly int accountId;

        private readonly string connectionString =
            "server=localhost;port=3306;database=student_db;" +
            "uid=root;password=@Bok15518;charset=utf8mb4;";

        // Required by the Visual Studio Designer.
        public PROFILE()
        {
            InitializeComponent();

            Text = "Student Profile";
            StartPosition = FormStartPosition.CenterParent;

            picStudent.SizeMode = PictureBoxSizeMode.Zoom;

            btnBack.Text = "Back";
            btnBack.Click += BtnBack_Click;

            CancelButton = btnBack;

            Shown += PROFILE_Shown;
            FormClosed += PROFILE_FormClosed;
        }

        // Receives the authenticated student's account ID.
        public PROFILE(int authenticatedAccountId) : this()
        {
            if (authenticatedAccountId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(authenticatedAccountId));
            }

            accountId = authenticatedAccountId;
        }

        private void PROFILE_Shown(object? sender, EventArgs e)
        {
            if (accountId <= 0)
            {
                MessageBox.Show(
                    this,
                    "Please log in before viewing your profile.",
                    "Login Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Close();
                return;
            }

            LoadProfile();
        }

        private void LoadProfile()
        {
            byte[]? pictureBytes;

            try
            {
                using var connection =
                    new MySqlConnection(connectionString);

                connection.Open();

                const string sql = @"
                    SELECT
                        a.student_id,
                        a.first_name,
                        a.last_name,
                        a.email,
                        p.middle_name,
                        p.year_block_program,
                        p.enrollment_status,
                        p.date_of_birth,
                        p.gender,
                        p.contact_number,
                        p.profile_picture
                    FROM student_accounts a
                    INNER JOIN student_profiles p
                        ON p.account_id = a.account_id
                    WHERE a.account_id = @id;";

                using var command =
                    new MySqlCommand(sql, connection);

                command.Parameters.Add(
                    "@id",
                    MySqlDbType.Int32).Value = accountId;

                using var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    MessageBox.Show(
                        this,
                        "Please save your student information " +
                        "before viewing your profile.",
                        "Profile Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Close();
                    return;
                }

                string firstName =
                    reader["first_name"].ToString() ?? "";

                string middleName =
                    reader["middle_name"].ToString() ?? "";

                // Middle name is displayed alongside the first name.
                string givenNames = string.IsNullOrWhiteSpace(middleName)
                    ? firstName
                    : firstName + " " + middleName;

                DisplayValue(
                    pnlSurname,
                    reader["last_name"].ToString());

                DisplayValue(
                    pnlFirstName,
                    givenNames);

                DisplayValue(
                    pnlStudentId,
                    reader["student_id"].ToString());

                DisplayValue(
                    pnlEmail,
                    reader["email"].ToString());

                DisplayValue(
                    pnlYearBlockProgram,
                    reader["year_block_program"].ToString());

                DisplayValue(
                    pnlStatus,
                    reader["enrollment_status"].ToString());

                string birthDate =
                    reader["date_of_birth"] == DBNull.Value
                        ? "Not provided"
                        : Convert.ToDateTime(reader["date_of_birth"])
                            .ToString("MMMM dd, yyyy");

                DisplayValue(pnlBirth, birthDate);

                DisplayValue(
                    pnlGender,
                    reader["gender"].ToString());

                DisplayValue(
                    pnlContact,
                    reader["contact_number"].ToString());

                pictureBytes =
                    reader["profile_picture"] == DBNull.Value
                        ? null
                        : (byte[])reader["profile_picture"];
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(
                    this,
                    "Unable to load your profile.\n\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Close();
                return;
            }

            LoadPicture(pictureBytes);
        }

        private static void DisplayValue(Control container, string? value)
        {
            string labelName = container.Name + "_Value";

            Label? displayLabel =
                container.Controls[labelName] as Label;

            if (displayLabel == null)
            {
                displayLabel = new Label
                {
                    Name = labelName,
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    BackColor = Color.Transparent,
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleLeft,
                    AutoEllipsis = true,
                    UseMnemonic = false,
                    Margin = Padding.Empty,
                    Padding = new Padding(3, 0, 3, 0)
                };

                container.Controls.Add(displayLabel);
            }

            // Use smaller fonts for the information rows.
            float fontSize = container.Name switch
            {
                "pnlSurname" => 22F,
                "pnlFirstName" => 17F,
                _ => 10F
            };

            // Reduce further when the panel is short.
            float availableHeight =
                Math.Max(1, container.ClientSize.Height -
                            container.Padding.Vertical - 4);

            float maximumPoints =
                availableHeight * 72F / container.DeviceDpi / 1.4F;

            fontSize = Math.Max(1F, Math.Min(fontSize, maximumPoints));

            displayLabel.Font = new Font(
                "Segoe UI",
                fontSize,
                FontStyle.Regular,
                GraphicsUnit.Point);

            displayLabel.Text = string.IsNullOrWhiteSpace(value)
                ? "Not provided"
                : value;

            displayLabel.BringToFront();
        }

        private void LoadPicture(byte[]? pictureBytes)
        {
            Image? previous = picStudent.Image;
            picStudent.Image = null;
            previous?.Dispose();

            if (pictureBytes == null || pictureBytes.Length == 0)
            {
                return;
            }

            try
            {
                using MemoryStream stream =
                    new MemoryStream(pictureBytes);

                using Image original = Image.FromStream(stream);

                // Clone so the image remains usable after closing the stream.
                picStudent.Image = new Bitmap(original);
            }
            catch (Exception ex) when (
                ex is ArgumentException ||
                ex is OutOfMemoryException ||
                ex is System.Runtime.InteropServices.ExternalException)
            {
                MessageBox.Show(
                    this,
                    "Your information was loaded, but the saved " +
                    "picture could not be displayed.",
                    "Picture Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void BtnBack_Click(object? sender, EventArgs e)
        {
            // Returns to the update window that opened this form.
            Close();
        }

        private void PROFILE_FormClosed(
            object? sender,
            FormClosedEventArgs e)
        {
            Image? previous = picStudent.Image;
            picStudent.Image = null;
            previous?.Dispose();
        }
    }
}