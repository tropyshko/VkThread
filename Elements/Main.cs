namespace VkThread.Elements
{
    public partial class Main : Form
    {
        bool isFirstLoad = true;
        int[] postsAll;
        private static Image mForm;
        private Point start;
        private bool drawing = false;
        private Image orig;
        public Main()
        {
            InitializeComponent();
            first_load();
            asyncUpdate();
            timer1.Interval = 30000;
            timer1.Start();
            timer2.Interval = 1000;
            timer2.Start();
            orig = pictureBox1.Image;
        }
        private void first_load()
        {
            if (isFirstLoad)
            {
                panel3.Visible = true;
                isFirstLoad = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            asyncUpdate();

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            asyncUpdatePosts();
        }
        async private void asyncUpdate()
        {
            await Task.Run(() =>
            {
                updateAll();
            });
        }
        async private void asyncUpdatePosts()
        {
            await Task.Run(() =>
            {
                updatePosts();
            });
        }
        private void updatePosts()
        {
            long postsCount = Database.postsCount();
            long clicksCount = long.Parse(label4.Text);
            long work_time = 0;
            updateTopBar(postsCount, clicksCount, work_time);
        }

        private void updateAll()
        {
            long postsCount = Database.postsCount();
            long work_time = 0;
            int female = 0;
            int male = 0;
            int age_18 = 0;
            int age_21 = 0;
            int age_24 = 0;
            int age_27 = 0;
            int age_30 = 0;
            int age_35 = 0;
            int age_45 = 0;
            int age_55 = 0;
            long clicksCount = 0;
            postsAll = new int[13];
            try
            {
                Dictionary<int, string> data = Database.GetLinks();
                foreach (int key in data.Keys)
                {
                    string dat = data[key];
                    int c = GetStat(dat);
                    clicksCount += c;
                    string link = data[key];
                    dynamic resp = api.getLinkStats(link, "day");
                    dynamic stats = resp.response.stats;
                    foreach (dynamic date in stats)
                    {
                        string tstamp = date.timestamp;
                        DateTime postDate = utils.DateTimeFromUnixTime(tstamp);
                        int month = postDate.Month;
                        int views = date.views;
                        postsAll[month] = postsAll[month] + views;
                        if (views > 0)
                        {
                            dynamic sex_age = date.sex_age;
                            foreach (dynamic range in sex_age)
                            {
                                int fm = range.female;
                                int ml = range.male;
                                female += fm;
                                male += ml;
                                string age = range.age_range;
                                switch (age)
                                {
                                    case "<18":
                                        age_18 += fm;
                                        age_18 += ml;
                                        break;
                                    case "18-21":
                                        age_21 += fm;
                                        age_21 += ml;
                                        break;
                                    case "21-24":
                                        age_24 += fm;
                                        age_24 += ml;
                                        break;
                                    case "24-27":
                                        age_27 += fm;
                                        age_27 += ml;
                                        break;
                                    case "27-30":
                                        age_30 += fm;
                                        age_30 += ml;
                                        break;
                                    case "30-35":
                                        age_35 += fm;
                                        age_35 += ml;
                                        break;
                                    case "35-45":
                                        age_45 += fm;
                                        age_45 += ml;
                                        break;
                                    case ">45":
                                        age_55 += fm;
                                        age_55 += ml;
                                        break;
                                }
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Database.addError(ex);
            }
            try
            {
                var ts = postsAll;
                update_plot(postsAll);
            }
            catch (Exception ex)
            {
                Database.addError(ex);
            }
            try
            {
                updateAges(age_18, age_21, age_24, age_27, age_30, age_35, age_45, age_55);
            }
            catch (Exception ex)
            {
                Database.addError(ex);
            }
            try
            {
                updateSideBar(male, female);
            }
            catch (Exception ex)
            {
                Database.addError(ex);
            }

            updateTopBar(postsCount, clicksCount, work_time);
            mForm = pictureBox1.Image;
            panel3.Invoke(delegate { panel3.Visible = false; });
        }

        public static int GetStat(string lid)
        {
            dynamic data = api.getLinkStatsAll(lid);
            int x = 0;
            foreach (var item in data.response.stats)
            {
                {
                    int view = item.views;
                    x += view;
                }
            }
            return x;
        }
        //AllPosts
        private void updateTopBar(long posts, long clicks, long workTime)
        {
            float conv = posts > 0 ? clicks * 100 / posts : 0;
            //All posts
            label2.Invoke(new MethodInvoker(delegate
            {
                label2.Text = posts.ToString();
            }));
            //All clicks
            label4.Invoke(new MethodInvoker(delegate
            {
                label4.Text = clicks.ToString();
            }));
            //Conv
            label6.Invoke(new MethodInvoker(delegate
            {
                label6.Text = $"{conv}%";
            }));
            //Work time
            label8.Invoke(new MethodInvoker(delegate
            {
                label8.Text = workTime.ToString();
            }));
        }
        private void updateSideBar(int m, int w)
        {
            int AllCount = m + w;
            int sex_m_proc = (m * 100) / AllCount;
            int sex_w_proc = (w * 100) / AllCount;
            if (guna2CircleProgressBar1.InvokeRequired)
            {
                //Circle
                guna2CircleProgressBar1.Invoke(new MethodInvoker(delegate
                {
                    guna2CircleProgressBar1.Value = sex_m_proc;
                }));
                // Man
                label13.Invoke(new MethodInvoker(delegate
                {
                    label30.Text = $"{sex_m_proc}%";
                }));
                //woman
                label14.Invoke(new MethodInvoker(delegate
                {
                    label32.Text = $"{sex_w_proc}%";
                }));
            }
            else
            {
                //Circle
                guna2CircleProgressBar1.Value = sex_m_proc;
                // Man
                label30.Text = $"{sex_m_proc}%";
                //woman
                label32.Text = $"{sex_w_proc}%";
            }
        }
        private void updateAges(int lb1, int lb2, int lb3, int lb4, int lb5, int lb6, int lb7, int lb8)
        {
            int lbAll = lb1 + lb2 + lb3 + lb4 + lb5 + lb6 + lb7 + lb8;

            int bar1 = (lb1 * 100) / lbAll;
            int bar2 = (lb2 * 100) / lbAll;
            int bar3 = (lb3 * 100) / lbAll;
            int bar4 = (lb4 * 100) / lbAll;
            int bar5 = (lb5 * 100) / lbAll;
            int bar6 = (lb6 * 100) / lbAll;
            int bar7 = (lb7 * 100) / lbAll;
            int bar8 = (lb8 * 100) / lbAll;

            if (guna2ProgressBar1.InvokeRequired)
            {
                // <18
                guna2ProgressBar1.Invoke(new MethodInvoker(delegate { guna2ProgressBar1.Value = bar1; }));
                label14.Invoke(new MethodInvoker(delegate { label14.Text = lb1.ToString(); }));
                // 18-21
                guna2ProgressBar2.Invoke(new MethodInvoker(delegate { guna2ProgressBar2.Value = bar2; }));
                label16.Invoke(new MethodInvoker(delegate { label16.Text = lb2.ToString(); }));
                // 21-24
                guna2ProgressBar3.Invoke(new MethodInvoker(delegate { guna2ProgressBar3.Value = bar3; }));
                label18.Invoke(new MethodInvoker(delegate { label18.Text = lb3.ToString(); }));
                // 24-27
                guna2ProgressBar4.Invoke(new MethodInvoker(delegate { guna2ProgressBar4.Value = bar4; }));
                label22.Invoke(new MethodInvoker(delegate { label22.Text = lb4.ToString(); }));
                // 27-30
                guna2ProgressBar5.Invoke(new MethodInvoker(delegate { guna2ProgressBar5.Value = bar5; }));
                label23.Invoke(new MethodInvoker(delegate { label23.Text = lb5.ToString(); }));
                // 30-35
                guna2ProgressBar6.Invoke(new MethodInvoker(delegate { guna2ProgressBar6.Value = bar6; }));
                label24.Invoke(new MethodInvoker(delegate { label24.Text = lb6.ToString(); }));
                // 35-45
                guna2ProgressBar7.Invoke(new MethodInvoker(delegate { guna2ProgressBar7.Value = bar7; }));
                label28.Invoke(new MethodInvoker(delegate { label28.Text = lb7.ToString(); }));
                // >45
                guna2ProgressBar8.Invoke(new MethodInvoker(delegate { guna2ProgressBar8.Value = bar8; }));
                label29.Invoke(new MethodInvoker(delegate { label29.Text = lb8.ToString(); }));
            }
            else
            {
                // <18
                guna2ProgressBar1.Value = bar1;
                label14.Text = lb1.ToString();
                // 18-21
                guna2ProgressBar2.Value = bar2;
                label16.Text = lb2.ToString();
                // 21-24
                guna2ProgressBar3.Value = bar3;
                label18.Text = lb3.ToString();
                // 24-27
                guna2ProgressBar4.Value = bar4;
                label22.Text = lb4.ToString();
                // 27-30
                guna2ProgressBar5.Value = bar5;
                label23.Text = lb5.ToString();
                // 30-35
                guna2ProgressBar6.Value = bar6;
                label24.Text = lb6.ToString();
                // 35-45
                guna2ProgressBar7.Value = bar7;
                label28.Text = lb7.ToString();
                // >45
                guna2ProgressBar8.Value = bar8;
                label29.Text = lb8.ToString();
            }
        }
        private void update_plot(int[] posts)
        {
            //int[] conv = new int[] { 3, 6, 3, 4, 6, 7, 8, 2, 1, 10, 11, 4, 0, 4, 5, 6, 7, 8, 0, 10, 21, 12, 0, 44, 35, 1, 17, 18, 0, 50 };

            Array.Reverse(posts);
            int wX = pictureBox1.Width;
            int hX = pictureBox1.Height;
            Bitmap flag = create_plot(wX, hX);
            plotDraw(wX, hX, "Переходы", flag, posts, Color.FromArgb(164, 0, 241), 2, true);
            //plotDraw("Переходы", flag, conv, Color.LimeGreen, 1);
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new MethodInvoker(delegate { pictureBox1.Image = flag; }));
            }
            else { pictureBox1.Image = flag; }
        }
        private Graphics plotDraw(int wX, int hX2, string title, Bitmap flag, int[] posts, Color color, float width = 1, bool history = false)
        {
            //wx width
            //hx height
            Graphics flagGraphics = Graphics.FromImage(flag);
            Point[] ptarray = new Point[posts.Length];
            int hX = hX2 - 25;
            int maxView = posts.Max();
            int maxDay = posts.Count();
            int medView = hX / maxView;
            int medDay = wX / maxDay;
            Pen viewsPen = new Pen(color);
            viewsPen.Width = width;
            int x = 0;
            int lastX = 0;
            int lastY = hX;
            int lastPost = 0;
            Bitmap flagDays = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Bitmap flagPosts = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            Graphics DaysGraphics = Graphics.FromImage(flagDays);
            Graphics PostsGraphics = Graphics.FromImage(flagPosts);
            int day1 = posts.Count();
            // Posts
            foreach (int view in posts)
            {
                int x1 = lastX;
                int y1 = lastY;
                int x2 = wX - (x * medDay);
                int y2 = hX - (view * medView);
                ptarray[x] = new Point(x2, y2);
                x++;

                if (history)
                {
                    Font drawFont = new Font("Roboto", 8);
                    SolidBrush drawBrush = new SolidBrush(Color.White);
                    int diff = Math.Abs(lastPost - view);
                    if (diff > 5)
                    {
                        lastPost = view;
                        string drawString = view.ToString();
                        PostsGraphics.DrawString(drawString, drawFont, drawBrush, pictureBox3.Width / 5, y2);
                    }
                    DaysGraphics.DrawString(day1.ToString(), drawFont, drawBrush, x2, pictureBox2.Height / 2);

                    day1--;
                }
            }

            flagGraphics.DrawCurve(viewsPen, ptarray);
            /*            int days = posts.Count();
                        {
                            for (int day = 1; day < days; day++)
                            {
                                xx2++;
                                int x2 = wX - (xx2 * medDay);
                                int x1 = lastX;
                                int y1 = lastY;
                                lastX = x2;
                                Font drawFont2 = new Font("Roboto", 8);
                                SolidBrush drawBrush2 = new SolidBrush(Color.White);
                                float xS2 = x2;
                                float yS2 = pictureBox2.Height / 2;
                            }
                        }*/
            pictureBox2.Image = flagDays;
            pictureBox3.Image = flagPosts;
            return flagGraphics;
        }
        private Bitmap create_plot(int wX, int hX2)
        {
            // Система Координат
            Bitmap flag = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics flagGraphics = Graphics.FromImage(flag);
            Pen border = new Pen(Color.White);
            /*flagGraphics.DrawLine(border, 0, (int)(hX - 1), wX, (int)(hX - 1));
            flagGraphics.DrawLine(border, (int)(0), 0, (int)(0), hX);
            flagGraphics.DrawLine(border, (int)(wX - 1), (int)(0), (int)(wX - 1), (int)(hX - 1));*/
            return flag;
        }
        private void guna2ProgressBar6_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void guna2ProgressBar5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void sPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            pictureBox1.Image = orig;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            start = new Point(e.X, e.Y);
            Image flag = pictureBox1.Image;
            pictureBox1.Image = flag;
            label33.Text = e.Location.ToString();
            Graphics flagGraphics = Graphics.FromImage(flag);
            Pen border = new Pen(Color.Green);
            int x = (int)(e.Location.X);
            int y = (int)(e.Location.Y);
            flagGraphics.DrawLine(border, 0, y, x, y);

        }

        private void guna2ProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
