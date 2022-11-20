namespace VkThread.Elements
{
    public partial class Links : Form
    {
        public Links()
        {
            InitializeComponent();
            Thread updateThread = new Thread(update_links);
            updateThread.Start();
        }
        public void update_links()
        {
            Dictionary<int, Links_struct> groups = Database.GetLinksList();
            foreach (int key in groups.Keys)
            {
                string url = groups[key].url;
                string shortId = groups[key].shortId;
                string comment = groups[key].comment;
                string shortLink = $"http://vk.cc/{shortId}";
                if (guna2DataGridView2.InvokeRequired)
                {
                    guna2DataGridView2.Invoke(new MethodInvoker(delegate { guna2DataGridView2.Rows.Add(false, shortId, url, shortLink, comment); }));
                }
                else
                {
                    guna2DataGridView2.Rows.Add(false, shortId, url, shortLink, comment);
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string url = sTextBox1.Texts;
            string commentary = sTextBox2.Texts;
            if (url.Length < 1)
            {
                sTextBox1.BackColor = Color.MediumVioletRed;
            }
            else
            {
                try
                {
                    string shortKey = api.getShortLink(url);
                    string shortUrl = $"http://vk.cc/{shortKey}";
                    Database.add_shortLink(url, shortKey, commentary);
                    guna2DataGridView2.Rows.Add(false, shortKey, url, shortUrl, commentary);
                }
                catch
                {

                }
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            var rows = guna2DataGridView2.Rows;
            foreach (DataGridViewRow row in rows)
            {
                try
                {
                    guna2DataGridView2[0, row.Index].Value = true;
                }
                catch
                {

                }
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            var rows = guna2DataGridView2.Rows;
            foreach (DataGridViewRow row in rows)
            {
                try
                {
                    guna2DataGridView2[0, row.Index].Value = false;
                }
                catch
                {

                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            try
            {
                var rows = guna2DataGridView2.Rows;
                foreach (DataGridViewRow row in rows)
                {
                    try
                    {
                        if (guna2DataGridView2[0, row.Index].Value != null)
                        {
                            string ew = guna2DataGridView2[0, row.Index].Value.ToString();
                            if (ew == "True")
                            {
                                try
                                {
                                    string link = guna2DataGridView2[1, row.Index].Value.ToString();
                                    //Database.ReуmoveLink(link);
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
                //updateAccounts();
            }
            catch (Exception ex)
            {
                Database.addError(ex);
            }
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 & e.ColumnIndex == 0)
            {
                if (guna2DataGridView2[0, e.RowIndex].Value != null)
                {
                    string ew = guna2DataGridView2[0, e.RowIndex].Value.ToString();
                    guna2DataGridView2[0, e.RowIndex].Value = ew == "True" ? false : (object)true;
                }
                else
                {
                    guna2DataGridView2[0, e.RowIndex].Value = true;
                }
            }
        }
    }
}
