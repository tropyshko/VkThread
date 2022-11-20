namespace VkThread
{
    public partial class Form1 : Form
    {
        private Form main;
        private Form campaigns;
        private Form accounts;
        private Form analytics;
        private Form calendar;
        private Form groups;
        private Form links;
        private Form sendler;
        private Form settings;
        public Form1()
        {
            InitializeComponent();
            init_forms();
            init_hide();
        }
        private void load(Form childForm)
        {
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(childForm);
            this.panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void init_forms()
        {
            campaigns = new Elements.Campaigns();
            load(campaigns);
            accounts = new Elements.Accounts();
            load(accounts);
            analytics = new Elements.Analytics();
            load(analytics);
            calendar = new Elements.Calendar();
            load(calendar);
            groups = new Elements.Groups();
            load(groups);
            links = new Elements.Links();
            load(links);
            sendler = new Elements.Sendler();
            load(sendler);
            settings = new Elements.Settings();
            load(settings);
            main = new Elements.Main();
            load(main);
        }
        private void init_hide()
        {
            campaigns.Hide();
            accounts.Hide();
            analytics.Hide();
            calendar.Hide();
            groups.Hide();
            links.Hide();
            sendler.Hide();
            settings.Hide();
        }
        private void hide_forms()
        {
            main.Hide();
            campaigns.Hide();
            accounts.Hide();
            analytics.Hide();
            calendar.Hide();
            groups.Hide();
            links.Hide();
            sendler.Hide();
            settings.Hide();
        }
        private void button_click()
        {
        }
        private void buttons_clear()
        {
            guna2Button1.ForeColor = Color.White;
            guna2Button1.FillColor = Color.FromArgb(22, 23, 27);
            guna2Button2.ForeColor = Color.White;
            guna2Button2.FillColor = Color.FromArgb(22, 23, 27);
            guna2Button3.ForeColor = Color.White;
            guna2Button3.FillColor = Color.FromArgb(22, 23, 27);
            guna2Button4.ForeColor = Color.White;
            guna2Button4.FillColor = Color.FromArgb(22, 23, 27);
            guna2Button5.ForeColor = Color.White;
            guna2Button5.FillColor = Color.FromArgb(22, 23, 27);
            guna2Button6.ForeColor = Color.White;
            guna2Button6.FillColor = Color.FromArgb(22, 23, 27);
            guna2Button7.ForeColor = Color.White;
            guna2Button7.FillColor = Color.FromArgb(22, 23, 27);
            guna2Button8.ForeColor = Color.White;
            guna2Button8.FillColor = Color.FromArgb(22, 23, 27);
            guna2Button9.ForeColor = Color.White;
            guna2Button9.FillColor = Color.FromArgb(22, 23, 27);
        }
        // Main
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            buttons_clear();
            guna2Button1.ForeColor = Color.FromArgb(196, 153, 232);
            guna2Button1.FillColor = Color.FromArgb(32, 33, 39);
            hide_forms();
            main.Show();
        }
        //Camp
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            buttons_clear();
            guna2Button2.ForeColor = Color.FromArgb(196, 153, 232);
            guna2Button2.FillColor = Color.FromArgb(32, 33, 39);
            hide_forms();
            campaigns.Show();
        }
        //Sendler
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            buttons_clear();
            guna2Button3.ForeColor = Color.FromArgb(196, 153, 232);
            guna2Button3.FillColor = Color.FromArgb(32, 33, 39);
            hide_forms();
            sendler.Show();
        }
        //Accounts
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            buttons_clear();
            guna2Button4.ForeColor = Color.FromArgb(196, 153, 232);
            guna2Button4.FillColor = Color.FromArgb(32, 33, 39);
            hide_forms();
            accounts.Show();
        }
        //Groups
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            buttons_clear();
            guna2Button5.ForeColor = Color.FromArgb(196, 153, 232);
            guna2Button5.FillColor = Color.FromArgb(32, 33, 39);
            hide_forms();
            groups.Show();
        }
        //Links
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            buttons_clear();
            guna2Button6.ForeColor = Color.FromArgb(196, 153, 232);
            guna2Button6.FillColor = Color.FromArgb(32, 33, 39);
            hide_forms();
            links.Show();
        }
        //Calendar
        private void guna2Button7_Click(object sender, EventArgs e)
        {
            buttons_clear();
            guna2Button7.ForeColor = Color.FromArgb(196, 153, 232);
            guna2Button7.FillColor = Color.FromArgb(32, 33, 39);
            hide_forms();
            calendar.Show();
        }
        //Analytics
        private void guna2Button8_Click(object sender, EventArgs e)
        {
            buttons_clear();
            guna2Button8.ForeColor = Color.FromArgb(196, 153, 232);
            guna2Button8.FillColor = Color.FromArgb(32, 33, 39);
            hide_forms();
            analytics.Show();
        }
        //Settings
        private void guna2Button9_Click(object sender, EventArgs e)
        {
            buttons_clear();
            guna2Button9.ForeColor = Color.FromArgb(196, 153, 232);
            guna2Button9.FillColor = Color.FromArgb(32, 33, 39);
            hide_forms();
            settings.Show();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        Point lastpoint;
        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);

        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            Hide();
            notifyIcon1.Visible = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
        }
    }
}