using Newtonsoft.Json;

namespace VkThread.Elements
{
    public partial class Accounts : Form
    {
        public static string accounts_path { get; set; }
        public static string proxy_path { get; set; }
        public Accounts()
        {
            InitializeComponent();
            updateAccounts();
        }
        private void updateAccounts()
        {
            Dictionary<int, User_struct> accounts = Database.GetAccounts();

            if (guna2DataGridView1.InvokeRequired)
            {
                guna2DataGridView1.Invoke(new MethodInvoker(delegate { guna2DataGridView1.Rows.Clear(); }));
            }
            else
            {
                guna2DataGridView1.Rows.Clear();
            }

            foreach (int key in accounts.Keys)
            {
                int Id = accounts[key].Id;
                string number = accounts[key].Number;
                string Password = accounts[key].Password;
                int ScriptId = accounts[key].ScriptId;
                string Proxy = accounts[key].Proxy;
                string Status = accounts[key].Status;
                string BuyAt = accounts[key].BuyAt;
                string userId = accounts[key].userId;

                if (guna2DataGridView1.InvokeRequired)
                {
                    guna2DataGridView1.Invoke(new MethodInvoker(delegate { guna2DataGridView1.Rows.Add(false, number, Password, Status, BuyAt, Proxy); }));
                }
                else
                {
                    guna2DataGridView1.Rows.Add(false, number, Password, Status, BuyAt, Proxy);
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            string ip = sTextBox1.Texts;
            string port = sTextBox2.Texts;
            string login = sTextBox3.Texts;
            string password = sTextBox4.Texts;
            int NumPort;
            bool isNumber = int.TryParse(port, out NumPort);
            if (isNumber)
            {
                bool status = utils.check_proxy(ip, port, login, password);
                proxy_color(status);

            }
            else
            {
                proxy_color(false);
            }

        }
        private void proxy_color(bool status)
        {
            if (status)
            {
                sTextBox1.BorderColor = Color.Green;
                sTextBox2.BorderColor = Color.Green;
                sTextBox3.BorderColor = Color.Green;
                sTextBox4.BorderColor = Color.Green;
                sTextBox1.BorderSize = 3;
                sTextBox2.BorderSize = 3;
                sTextBox3.BorderSize = 3;
                sTextBox4.BorderSize = 3;
            }
            else
            {
                sTextBox1.BorderColor = Color.DarkRed;
                sTextBox2.BorderColor = Color.DarkRed;
                sTextBox3.BorderColor = Color.DarkRed;
                sTextBox4.BorderColor = Color.DarkRed;
                sTextBox1.BorderSize = 3;
                sTextBox2.BorderSize = 3;
                sTextBox3.BorderSize = 3;
                sTextBox4.BorderSize = 3;
            }
        }
        //add account
        private void guna2Button8_Click(object sender, EventArgs e)
        {
            string proxy_ip = sTextBox1.Texts;
            string proxy_port = sTextBox2.Texts;
            string proxy_login = sTextBox3.Texts;
            string proxy_password = sTextBox4.Texts;
            string login = sTextBox5.Texts;
            string password = sTextBox6.Texts;
            var Proxy_struct = new Proxy_struct
            {
                ip = proxy_ip,
                port = proxy_port,
                login = proxy_login,
                password = proxy_password,
            };
            if (check_acc())
            {
                Database.add_user(proxy_ip, proxy_port, proxy_login, proxy_password, login, password);
                updateAccounts();
            }
        }
        //refresh button
        private void guna2Button9_Click(object sender, EventArgs e)
        {
            updateAccounts();
        }
        //proxy load
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                proxy_path = openFileDialog.FileName;

                /*                //Read the contents of the file into a stream
                                var fileStream = openFileDialog.OpenFile();

                                using (StreamReader reader = new StreamReader(fileStream))
                                {
                                    string fileContent = reader.ReadToEnd();
                                }*/
            }
            check_load();
        }
        private void check_load()
        {
            string pp = proxy_path;
            string ap = accounts_path;
            try
            {
                if (pp.Length > 1 && ap.Length > 1)
                {
                    Dictionary<int, string> accounts = parse_account();
                    Dictionary<int, string> proxys = parse_proxy();
                    foreach (int key in accounts.Keys)
                    {
                        string account = accounts[key];
                        string[] accountData = accounts[key].Split(':');
                        string number = accountData[0];
                        string pass = accountData[1];
                        string[] proxy = proxys[key].Split(':');
                        string ip = proxy[0];
                        string port = proxy[1];
                        string login = proxy[2];
                        string password = proxy[3];
                        Database.add_user(ip, port, login, password, number, pass);
                    }
                    updateAccounts();
                }
            }
            catch { }

        }
        //accounts load
        private void guna2Button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                accounts_path = openFileDialog.FileName;
                /*
                                //Read the contents of the file into a stream
                                var fileStream = openFileDialog.OpenFile();

                                using (StreamReader reader = new StreamReader(fileStream))
                                {
                                    string fileContent = reader.ReadToEnd();
                                }*/
            }
            check_load();
        }
        private static Dictionary<int, string> parse_proxy()
        {
            var dict = new Dictionary<int, string>();
            StreamReader f = new StreamReader(proxy_path);
            int id = 0;
            while (!f.EndOfStream)
            {
                string line = f.ReadLine();
                dict.Add(id, line);
                id++;
            }
            f.Close();
            return dict;
        }
        private static Dictionary<int, string> parse_account()
        {
            var dict = new Dictionary<int, string>();
            StreamReader f = new StreamReader(accounts_path);
            int id = 0;
            while (!f.EndOfStream)
            {
                string line = f.ReadLine();
                dict.Add(id, line);
                id++;
            }
            f.Close();
            return dict;
        }
        //check marked proxy
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                var rows = guna2DataGridView1.Rows;
                foreach (DataGridViewRow row in rows)
                {
                    try
                    {
                        if (guna2DataGridView1[0, row.Index].Value != null)
                        {
                            string ew = guna2DataGridView1[0, row.Index].Value.ToString();
                            if (ew == "True")
                            {
                                try
                                {
                                    string proxy = guna2DataGridView1[5, row.Index].Value.ToString();
                                    Thread checkProxy = new Thread(() => checkProxyCol(proxy, row.Index));
                                    checkProxy.Start();
                                }
                                catch { }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Database.addError(ex);
                    }
                }

            }
            catch (Exception ex)
            {
                Database.addError(ex);
            }
        }
        //delete marked proxy
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            try
            {
                var rows = guna2DataGridView1.Rows;
                foreach (DataGridViewRow row in rows)
                {
                    try
                    {
                        if (guna2DataGridView1[0, row.Index].Value != null)
                        {
                            string ew = guna2DataGridView1[0, row.Index].Value.ToString();
                            if (ew == "True")
                            {
                                try
                                {
                                    string numb = guna2DataGridView1[1, row.Index].Value.ToString();
                                    Database.RemoveAccount(numb);
                                }
                                catch { }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Database.addError(ex);
                    }
                }
                updateAccounts();
            }
            catch (Exception ex)
            {
                Database.addError(ex);
            }
        }
        // mark all proxy
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var rows = guna2DataGridView1.Rows;
            foreach (DataGridViewRow row in rows)
            {
                guna2DataGridView1[0, row.Index].Value = true;
            }
        }
        // unmark all proxy
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var rows = guna2DataGridView1.Rows;
            foreach (DataGridViewRow row in rows)
            {
                guna2DataGridView1[0, row.Index].Value = false;
            }
        }
        private bool check_acc()
        {
            bool status = true;
            string proxy_ip = sTextBox1.Texts;
            string proxy_port = sTextBox2.Texts;
            string proxy_login = sTextBox3.Texts;
            string proxy_password = sTextBox4.Texts;
            string login = sTextBox5.Texts;
            string password = sTextBox6.Texts;
            if (login.Length < 1)
            {
                sTextBox5.BorderColor = Color.DarkRed;
                status = false;
            }
            else
            {
                sTextBox5.BorderColor = Color.Green;
                status = true;
            }
            if (password.Length < 1)
            {
                sTextBox6.BorderColor = Color.DarkRed;
                status = false;

            }
            else
            {
                sTextBox6.BorderColor = Color.Green;
                status = true;
            }
            if (proxy_ip.Length < 1)
            {
                sTextBox1.BorderColor = Color.DarkRed;
                status = false;
            }
            else
            {
                sTextBox1.BorderColor = Color.Green;
                status = true;
            }
            if (proxy_port.Length < 1)
            {
                sTextBox2.BorderColor = Color.DarkRed;
                status = false;
            }
            else
            {
                sTextBox2.BorderColor = Color.Green;
                status = true;
            }
            if (proxy_login.Length < 1)
            {
                sTextBox3.BorderColor = Color.DarkRed;
                status = false;
            }
            else
            {
                sTextBox3.BorderColor = Color.Green;
                status = true;
            }
            if (proxy_password.Length < 1)
            {
                sTextBox4.BorderColor = Color.DarkRed;
                status = false;
            }
            else
            {
                sTextBox4.BorderColor = Color.Green;
                status = true;
            }
            return status;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int ro = e.RowIndex;
            if (e.RowIndex >= 0 & e.ColumnIndex == 6)
            {
                string proxy = guna2DataGridView1[5, e.RowIndex].Value.ToString();
                Thread checkProxy = new Thread(() => checkProxyCol(proxy, ro));
                checkProxy.Start();
            }
            if (e.RowIndex >= 0 & e.ColumnIndex == 0)
            {
                if (guna2DataGridView1[0, e.RowIndex].Value != null)
                {
                    string ew = guna2DataGridView1[0, e.RowIndex].Value.ToString();
                    guna2DataGridView1[0, e.RowIndex].Value = ew == "True" ? false : (object)true;
                }
                else
                {
                    guna2DataGridView1[0, e.RowIndex].Value = true;
                }
            }
        }
        private void checkProxyCol(string proxy, int ro)
        {
            try
            {

                dynamic proxyConv = JsonConvert.DeserializeObject(proxy);
                string ip = proxyConv.ip;
                string port = proxyConv.port;
                string login = proxyConv.login;
                string password = proxyConv.password;
                bool status = utils.check_proxy(ip, port, login, password);
                if (guna2DataGridView1.InvokeRequired)
                {
                    guna2DataGridView1.Invoke(delegate { guna2DataGridView1.Rows[ro].Cells[5].Style.SelectionBackColor = status == true ? Color.DarkGreen : Color.MediumVioletRed; });
                    guna2DataGridView1.Invoke(delegate { guna2DataGridView1.Rows[ro].Cells[5].Style.BackColor = status == true ? Color.DarkGreen : Color.MediumVioletRed; });
                }
                else
                {
                    guna2DataGridView1.Rows[ro].Cells[5].Style.SelectionBackColor = status == true ? Color.DarkGreen : Color.MediumVioletRed;
                    guna2DataGridView1.Rows[ro].Cells[5].Style.BackColor = status == true ? Color.DarkGreen : Color.MediumVioletRed;
                }
            }
            catch (Exception ex)
            {
                Database.addError(ex);
            }
        }
    }
}
