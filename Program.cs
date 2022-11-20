using System.Xml.Linq;

namespace VkThread
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool st = check_session();
            if (st)
            {
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(new LoginPage());
            }
        }
        static private bool check_session()
        {
            string[] data = check_auth();
            if (data != null)
            {
                string login = data[0];
                string password = data[1];
                Dictionary<int, Profile_struct> auth = Database.GetUser(login, password);
                if (auth.Count > 0)
                {
                    Config.userId = auth[auth.Keys.First()].id.ToString();
                    Config.username = auth[auth.Keys.First()].username.ToString();
                    Config.fullname = auth[auth.Keys.First()].fullname.ToString();
                    Config.balance = auth[auth.Keys.First()].balance.ToString();
                    Config.status = auth[auth.Keys.First()].status.ToString();
                    Config.token = auth[auth.Keys.First()].vk_token.ToString();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private static string[]? check_auth()
        {
            checkSessionDirectory();
            string curFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\VkThread\config.xml";
            if (File.Exists(curFile))
            {
                try
                {
                    XDocument xdoc = XDocument.Load(curFile);
                    XElement? config = xdoc.Element("config");
                    string login = config.Element("login").Value;
                    string password = config.Element("password").Value;
                    string token = config.Element("secret_key").Value;
                    if (login.Length > 0 && password.Length > 0)
                    {
                        string[] auth_data = { login, password, token };
                        return auth_data;
                    }
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
        private static void checkSessionDirectory()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\VkThread";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }

}