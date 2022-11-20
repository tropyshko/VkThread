namespace VkThread.Elements
{
    public partial class Groups : Form
    {
        public bool group_parse = true;
        public static Groups _Groups;
        public DateTime parse_start;
        public DateTime ItemTime_start;
        public DateTime ItemTime_end;
        public Groups()
        {
            InitializeComponent();
            Thread update_groupsThread = new Thread(() => update_groupsSelf());
            update_groupsThread.Start();
        }

        private void update_groupsAll()
        {
            if (guna2DataGridView1.InvokeRequired)
            {
                guna2DataGridView1.Invoke(new MethodInvoker(delegate { guna2DataGridView1.Rows.Clear(); }));
            }
            else
            {
                guna2DataGridView1.Rows.Clear();
            }
            Dictionary<int, Group_struct> groups = Database.GetGroups();
            foreach (int key in groups.Keys)
            {
                string url = groups[key].Url;
                string category = groups[key].category;
                string last_post = groups[key].last_post;
                string keywords = groups[key].keywords;
                string city = groups[key].city;
                string medium_age = groups[key].medium_age;
                string sex = groups[key].sex;
                string members = groups[key].members;
                string title = groups[key].title;
                string add_date = groups[key].add_date;
                string gender = "";
                if (sex == "1")
                {
                    gender = "М";
                }
                else if (sex == "2")
                {
                    gender = "Ж";
                }
                else if (sex == "3")
                {
                    gender = "Неиз.";
                }
                if (guna2DataGridView1.InvokeRequired)
                {
                    guna2DataGridView1.Invoke(new MethodInvoker(delegate { guna2DataGridView1.Rows.Add(false, url, category, last_post, keywords, city, add_date, medium_age, gender, members, title); }));
                }
                else
                {
                    guna2DataGridView1.Rows.Add(false, url, category, last_post, keywords, city, add_date, medium_age, gender, members, title);
                }
            }
            label9.Invoke(new MethodInvoker(delegate { label9.Text = $"Количество групп: {guna2DataGridView1.Rows.Count}"; }));

        }
        private void update_groupsSelf()
        {
            if (guna2DataGridView1.InvokeRequired)
            {
                guna2DataGridView1.Invoke(new MethodInvoker(delegate { guna2DataGridView1.Rows.Clear(); }));
            }
            else
            {
                guna2DataGridView1.Rows.Clear();
            }
            Dictionary<int, Group_struct> groups = Database.GetGroupsSelf();
            foreach (int key in groups.Keys)
            {
                string url = groups[key].Url;
                string category = groups[key].category;
                string last_post = groups[key].last_post;
                string keywords = groups[key].keywords;
                string city = groups[key].city;
                string medium_age = groups[key].medium_age;
                string sex = groups[key].sex;
                string members = groups[key].members;
                string title = groups[key].title;
                string add_date = groups[key].add_date;
                string gender = "";
                if (sex == "1")
                {
                    gender = "М";
                }
                else if (sex == "2")
                {
                    gender = "Ж";
                }
                else if (sex == "3")
                {
                    gender = "Неиз.";
                }
                if (guna2DataGridView1.InvokeRequired)
                {
                    guna2DataGridView1.Invoke(new MethodInvoker(delegate { guna2DataGridView1.Rows.Add(false, url, category, last_post, keywords, city, add_date, medium_age, gender, members, title); }));
                }
                else
                {
                    guna2DataGridView1.Rows.Add(false, url, category, last_post, keywords, city, add_date, medium_age, gender, members, title);
                }
            }
            label9.Invoke(new MethodInvoker(delegate { label9.Text = $"Количество групп: {guna2DataGridView1.Rows.Count}"; }));
        }
        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        //mark all
        private void guna2Button2_Click(object sender, EventArgs e)
        {

            var rows = guna2DataGridView1.Rows;
            foreach (DataGridViewRow row in rows)
            {
                guna2DataGridView1[0, row.Index].Value = true;
            }
        }
        //unmark all
        private void guna2Button3_Click(object sender, EventArgs e)
        {

            var rows = guna2DataGridView1.Rows;
            foreach (DataGridViewRow row in rows)
            {
                guna2DataGridView1[0, row.Index].Value = false;
            }
        }
        // remove marked
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
                                    Database.RemoveGroup(numb);
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
                if (guna2ToggleSwitch1.Checked)
                {
                    //update_groupsSelf();
                    update_groupsAll();
                }
                else
                {
                    update_groupsAll();
                }
            }
            catch (Exception ex)
            {
                Database.addError(ex);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timePassed();
            //timeLeft();
        }
        private void GroupsCheckin()
        {

        }
        public void timeLeft(DateTime start, DateTime end)
        {
            try
            {
                int ItemsAll = guna2ProgressBar1.Maximum;
                int ItemsCurrent = guna2ProgressBar1.Value;
                int ItemsLeft = ItemsAll - ItemsCurrent;
                TimeSpan OnOne = end - start;
                double secOnOne = OnOne.TotalSeconds * ItemsLeft;
                double minOnOne = TimeSpan.FromSeconds(secOnOne).TotalMinutes;
                double timeL = Math.Truncate(minOnOne);
                string txt = $"Осталось времени: {timeL} минут";
                label4.Invoke(new MethodInvoker(delegate { label4.Text = txt; }));
            }
            catch
            {

                label4.Text = $"";
            }
        }
        private void timePassed()
        {
            DateTime start = parse_start;
            DateTime now = DateTime.Now;
            TimeSpan diff = now - start;
            string txt = $"Прошло времени: {Math.Floor(diff.TotalMinutes)} минут {diff.Seconds} секунд";
            label3.Invoke(new MethodInvoker(delegate { label3.Text = txt; }));
        }
        public void update_keyword(string keyword)
        {
            label10.Invoke(new MethodInvoker(delegate { label10.Text = $"Текущее ключевое слово: {keyword}"; }));
        }
        public void update_currentGroup(string group)
        {
            label11.Invoke(new MethodInvoker(delegate { label11.Text = $"Текущая группа: {group}"; }));
        }
        //search
        private void guna2Button8_Click(object sender, EventArgs e)
        {
            string q = sTextBox1.Texts;
            int min_members = int.Parse(sTextBox2.Texts);
            bool isCityAnalyze = guna2CheckBox1.Checked;
            bool isSexAnalyze = guna2CheckBox2.Checked;
            bool isAgeAnalyze = guna2CheckBox3.Checked;
            bool isActive = guna2CheckBox4.Checked;
            bool searchName = guna2CheckBox5.Checked;
            string btn_text = guna2Button8.Text;
            var progress_max = new Progress<int>(s => guna2ProgressBar1.Maximum = s);
            var progress_value = new Progress<int>(s => guna2ProgressBar1.Value = s);
            Thread groupsSearch_thread = new Thread(() => GroupsParse.groups_search(q, searchName, isActive, isCityAnalyze, isAgeAnalyze, isSexAnalyze, progress_max, progress_value, min_members));
            if (guna2Button8.Text == "Поиск")
            {
                if (sTextBox1.Texts.Length > 0)
                {
                    if (min_members > 0)
                    {
                        group_parse = true;
                        parse_start = DateTime.Now;
                        timer1.Interval = 1000;
                        timer1.Start();
                        label3.Visible = true;
                        guna2Button8.Text = "Остановить";
                        groupsSearch_thread.Start();
                    }
                    else
                    {
                        label5.Text = "Введите количество участников";
                    }
                }
                else
                {
                    label5.Text = "Введите ключевое слово";
                }
            }
            else if (guna2Button8.Text == "Остановить")
            {
                group_parse = false;
                label3.Visible = false;
                timer1.Stop();
                guna2Button8.Text = "Поиск";
            }
        }
        public void update_table(string url, string last_post, string city, string keyword, string activity, string userId, int age, int sex, int members_count, string title)
        {
            DateTime dateTime = DateTime.Now;
            string add_date = dateTime.ToString();
            guna2DataGridView1.Invoke(new MethodInvoker(delegate { guna2DataGridView1.Rows.Add(false, url, activity, last_post, keyword, city, add_date, age, sex, members_count, title); }));
            label9.Invoke(new MethodInvoker(delegate { label9.Text = $"Количество групп: {guna2DataGridView1.Rows.Count}"; }));
        }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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

        private void Groups_Load(object sender, EventArgs e)
        {
            _Groups = this;
        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            /*            if (guna2ToggleSwitch1.Checked)
                        {
                            Thread groups_load = new(update_groupsSelf);
                            groups_load.Start();
                            guna2Button2.Visible = true;
                            guna2Button3.Visible = true;
                            guna2Button5.Visible = true;
                        }
                        else
                        {
                            Thread groups_load = new(update_groupsAll);
                            groups_load.Start();
                            guna2Button2.Visible = false;
                            guna2Button3.Visible = false;
                            guna2Button5.Visible = false;
                        }*/
        }

        private void sTextBox2__TextChanged(object sender, EventArgs e)
        {

        }

        private void sTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string url = sTextBox3.Texts;
            if (url.Length > 0)
            {
                if (url.Contains("vk.com/"))
                {
                    update_table(url, "", "", "", "", Config.userId, 0, 0, 0, "Добавленная группа");
                    Database.add_group(url, "", "", "", "", Config.userId, 0, 0, 0, "Добавленная группа");
                }
                else
                {
                    label5.Text = "Введите корректную ссылку";
                }
            }
            else
            {
                label5.Text = "Введите ссылку на группу";
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number))
            {
                e.Handled = true;
            }
            if (number == 8)
            {
                Console.WriteLine("");
            }
        }
    }
}
