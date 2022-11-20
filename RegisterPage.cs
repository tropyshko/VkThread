namespace VkThread
{
    public partial class RegisterPage : Form
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        //login
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //password
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        //password 2
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        // email
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        //invite
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        //back button
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // reg button
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            bool ok = true;
            string login = textBox1.Text;
            string password = textBox2.Text;
            string password2 = textBox3.Text;
            string email = textBox4.Text;
            string invite_code = textBox5.Text;
            if (login.Length < 1)
            {
                textBox1.BackColor = Color.MediumVioletRed;
                label6.Text = "Введите логин";
                ok = false;
            }
            if (password.Length < 1)
            {
                textBox2.BackColor = Color.MediumVioletRed;
                label6.Text = "Введите пароль";
                ok = false;
            }
            if (email.Length < 1)
            {
                textBox4.BackColor = Color.MediumVioletRed;
                label6.Text = "Введите почту";
                ok = false;
            }
            if (password != password2)
            {
                textBox2.BackColor = Color.MediumVioletRed;
                textBox3.BackColor = Color.MediumVioletRed;
                label6.Text = "Пароли несовпадают";
                ok = false;
            }
            string check_invite = Database.checkInvite(invite_code);
            if (check_invite == "no")
            {
                textBox5.BackColor = Color.MediumVioletRed;
                label6.Text = "Код приглашения уже использован";
                ok = false;
            }
            if (check_invite.Length == 0)
            {
                textBox5.BackColor = Color.MediumVioletRed;
                label6.Text = "Неверный код приглашения";
                ok = false;
            }
            if (invite_code.Length < 1)
            {
                textBox5.BackColor = Color.MediumVioletRed;
                label6.Text = "Введите код приглашения";
                ok = false;
            }
            if (ok)
            {
                string pass = utils.sha256(password);
                int status = Database.add_user(login, pass, email, invite_code);
                switch (status)
                {
                    case 0:
                        Database.updateInvite(invite_code);
                        this.Close();
                        break;
                    case 1:
                        label6.Text = ("Ошибка создания аккаунта!");
                        break;
                    case 2:
                        textBox1.BackColor = Color.MediumVioletRed;
                        label6.Text = ("Такой логин уже зарегистрирован!");
                        break;
                }
            }
        }
    }
}
