using Newtonsoft.Json;

namespace VkThread.Elements
{
    public partial class Sendler : Form
    {
        string selected = "";
        int campId;
        public Sendler()
        {
            InitializeComponent();
            update();
            updateAccounts();
            timer1.Interval = 2000;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Thread updateThread = new Thread(update);
            updateThread.Start();
        }
        private void update()
        {
            try
            {
                Dictionary<int, Campaigns_struct> scripts = Database.GetCampaigns();
                if (guna2DataGridView1.InvokeRequired)
                {
                    guna2DataGridView1.Invoke(new MethodInvoker(delegate { guna2ComboBox1.Items.Clear(); }));
                }
                else
                {
                    guna2ComboBox1.Items.Clear();
                }
                foreach (int key in scripts.Keys)
                {
                    string title = scripts[key].title;
                    if (guna2ComboBox1.InvokeRequired)
                    {
                        guna2ComboBox1.Invoke(new MethodInvoker(delegate { guna2ComboBox1.Items.Add(title); }));
                    }
                    else
                    {
                        guna2ComboBox1.Items.Add(title);
                    }
                }
                try
                {
                    if (guna2DataGridView1.InvokeRequired)
                    {
                        guna2DataGridView1.Invoke(new MethodInvoker(delegate { guna2ComboBox1.SelectedItem = selected; }));
                    }
                    else
                    {
                        guna2ComboBox1.SelectedItem = selected;
                    }

                }
                catch (Exception ex)
                {
                    Database.addError(ex);
                }
            }
            catch (Exception ex)
            {
                Database.addError(ex);
            }
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
                    guna2DataGridView1.Invoke(new MethodInvoker(delegate { guna2DataGridView1.Rows.Add(false, number, Password, Status, Proxy); }));
                }
                else
                {
                    guna2DataGridView1.Rows.Add(false, number, Password, Status, Proxy);
                }
            }
        }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int ro = e.RowIndex;
            if (e.RowIndex >= 0 & e.ColumnIndex == 5)
            {
                try
                {
                    string proxy = guna2DataGridView1[4, e.RowIndex].Value.ToString();
                    dynamic proxyConv = JsonConvert.DeserializeObject(proxy);
                    string ip = proxyConv.ip;
                    string port = proxyConv.port;
                    string login = proxyConv.login;
                    string password = proxyConv.password;
                    bool status = utils.check_proxy(ip, port, login, password);

                    if (guna2DataGridView1.InvokeRequired)
                    {

                        guna2DataGridView1.Invoke(delegate { guna2DataGridView1.Rows[ro].Cells[4].Style.SelectionBackColor = status == true ? Color.DarkGreen : Color.MediumVioletRed; });
                        guna2DataGridView1.Invoke(delegate { guna2DataGridView1.Rows[ro].Cells[4].Style.BackColor = status == true ? Color.DarkGreen : Color.MediumVioletRed; });
                    }
                    else
                    {
                        guna2DataGridView1.Rows[ro].Cells[4].Style.SelectionBackColor = status == true ? Color.DarkGreen : Color.MediumVioletRed;
                        guna2DataGridView1.Rows[ro].Cells[4].Style.BackColor = status == true ? Color.DarkGreen : Color.MediumVioletRed;
                    }
                }
                catch
                {
                    guna2DataGridView1.Rows[ro].Cells[4].Style.SelectionBackColor = Color.MediumVioletRed;
                    guna2DataGridView1.Rows[ro].Cells[4].Style.BackColor = Color.MediumVioletRed;
                }
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
        // Check all
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var rows = guna2DataGridView1.Rows;
            foreach (DataGridViewRow row in rows)
            {
                guna2DataGridView1[0, row.Index].Value = true;
            }
        }
        // Remove all check
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var rows = guna2DataGridView1.Rows;
            foreach (DataGridViewRow row in rows)
            {
                guna2DataGridView1[0, row.Index].Value = false;
            }
        }
        // start sendler
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            string accounts = "";
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
                                string account = guna2DataGridView1[1, row.Index].Value.ToString();
                                accounts += $"{account} ";
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
            string btn = guna2Button4.Text;
            accounts = accounts.Trim();
            accounts = accounts.Replace(" ", ",");
            if (btn.Contains("Начать рассылку"))
            {
                Database.SetWSbyId(campId, 1, accounts);
            }
            else if (btn.Contains("Остановить рассылку"))
            {
                Database.SetWSbyId(campId, 0);
            }
            // 0 - Сигнал стоп
            // 1 - Сигнал приступить к рассылке
            // 2 - В работе
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected = guna2ComboBox1.SelectedItem.ToString();
            Dictionary<int, Campaigns_struct> campaign = Database.GetCampaign(selected);
            int id = campaign[0].id;
            campId = id;
            int camp_st_id = Database.GetWSbyId(campId);
            string camp_status = "";
            switch (camp_st_id)
            {
                case 0:
                    camp_status = "Выключен";
                    guna2Button4.Text = "Начать рассылку";
                    break;
                case 1:
                    camp_status = "Работает";
                    guna2Button4.Text = "Остановить рассылку";
                    break;
                case 2:
                    camp_status = "Работает";
                    guna2Button4.Text = "Остановить рассылку";
                    break;

            }
            label3.Text = $"Статус: {camp_status}";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            Thread updateAccountsThread = new Thread(updateAccounts);
            updateAccountsThread.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
