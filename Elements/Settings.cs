namespace VkThread.Elements
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            updateVkBtn();
        }

        private void updateVkBtn()
        {
            string dbToken = Database.checkToken(Config.userId);
            guna2Button8.Text = dbToken.Length > 0 ? "Отвязать аккаунт ВК" : "Привязать аккаунт ВК";
            try
            {
                label2.Text = $"Привязанный аккаунт: {api.profileInfo()}";
            }
            catch
            {
                label2.Text = "";
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (guna2Button8.Text == "Привязать аккаунт ВК")
            {
                Form browser = new Browser();
                browser.Show();
                guna2Button8.Text = "Отвязать аккаунт ВК";
                updateVkBtn();
            }
            else if (guna2Button8.Text == "Отвязать аккаунт ВК")
            {
                Database.deleteToken();
                guna2Button8.Text = "Привязать аккаунт ВК";
                updateVkBtn();
            }

        }
        private void deleteSession()
        {
            string curFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\VkThread\config.xml";
            File.Delete(curFile);
            LoginPage authform = new LoginPage();
            authform.Show();
            Form mainform = this.ParentForm;
            mainform.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int x = 0;
                int y = 100 / x;
            }
            catch (Exception ex)
            {
                Database.addError(ex);
            }
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            Database.ClearStats();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            deleteSession();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string oldPassword = utils.sha256(sTextBox1.Texts);
            string newPassword = utils.sha256(sTextBox2.Texts);
            if (oldPassword == Database.getPassword())
            {
                Database.updatePassword(newPassword);
                label3.Text = "Пароль изменен";
            }
            else
            {
                label3.Text = "Неверный текущий пароль";
            }

        }
    }
}
