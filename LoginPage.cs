using System.Xml.Linq;

namespace VkThread
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (guna2Button3.Text == "Показать")
            {
                guna2Button3.Text = "Скрыть";
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                guna2Button3.Text = "Показать";
                textBox2.UseSystemPasswordChar = true;
            }
        }
        //register
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            RegisterPage register = new RegisterPage();
            register.ShowDialog();
        }
        //login
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            send();
        }
        private void send()
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            string psw_hash = utils.sha256(password);
            try
            {
                Dictionary<int, Profile_struct> auth = Database.GetUser(login, psw_hash);
                if (auth.Count > 0)
                {
                    this.Hide();
                    Config.userId = auth[auth.Keys.First()].id.ToString();
                    Config.username = auth[auth.Keys.First()].username.ToString();
                    Config.fullname = auth[auth.Keys.First()].fullname.ToString();
                    Config.balance = auth[auth.Keys.First()].balance.ToString();
                    Config.status = auth[auth.Keys.First()].status.ToString();
                    Config.token = auth[auth.Keys.First()].vk_token.ToString();
                    Form1 main = new();
                    create_config();
                    edit_config(psw_hash, login);
                    main.Show();
                }
                else
                {
                    label3.Text = "Ошибка: проверьте ваш логин или пароль";
                    label3.Show();
                }
            }
            catch
            {
                label3.Text = "Ошибка соединения";
                label3.Show();
            }
        }

        private static void checkSessionDirectory()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\VkThread";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        private static void edit_config(string password, string login = "")
        {
            string curFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\VkThread\config.xml";
            XDocument xdoc = XDocument.Load(curFile);
            var config = xdoc.Element("config");
            var pass = config.Element("password");
            pass.Value = password;
            if (login.Length > 0)
            {
                var logi = config.Element("login");
                logi.Value = login;
            }
            xdoc.Save(curFile);
        }
        private static void create_config()
        {
            string curFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\VkThread\config.xml";
            File.Delete(curFile);
            checkSessionDirectory();
            if (!File.Exists(curFile))
            {
                try
                {
                    XDocument xdoc = new XDocument();
                    // создаем первый элемент person
                    XElement config = new XElement("config");
                    // создаем атрибут name
                    XElement confLogin = new XElement("login");
                    XElement confPass = new XElement("password");
                    XElement confVkKey = new XElement("secret_key");
                    // добавляем атрибут и элементы в первый элемент person
                    config.Add(confLogin);
                    config.Add(confPass);
                    config.Add(confVkKey);
                    xdoc.Add(config);
                    xdoc.Save(curFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
        }
    }
}
