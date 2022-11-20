using System.Collections;
using System.Globalization;
using System.Resources;

namespace VkThread.Elements
{
    public partial class Campaigns : Form
    {
        Guna.UI2.WinForms.Guna2Button[] buttonArray = new Guna.UI2.WinForms.Guna2Button[30];
        int h = 3;
        int i = 1;
        bool newCamp = true;
        int scriptId;
        string camp_title;
        public Campaigns()
        {
            InitializeComponent();
            utils.CreateEmotions();
            utils.RemoveEmotions();
            GenCampBtn();
            update();
        }
        private void update()
        {
            try
            {
                Dictionary<int, Campaigns_struct> campaigns = Database.GetCampaigns();
                foreach (int key in campaigns.Keys)
                {
                    string title = campaigns[key].title;
                    add_button(title);
                }
            }
            catch (Exception ex)
            {
                Database.addError(ex);
            }
        }
        private void guna2TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            label7.Text = guna2TrackBar1.Value.ToString();
        }
        private void add_button(string title)
        {
            buttonArray[i] = new Guna.UI2.WinForms.Guna2Button();
            buttonArray[i].Size = new Size(190, 50);
            buttonArray[i].FillColor = Color.FromArgb(22, 23, 27);
            buttonArray[i].Name = "Button_" + i;
            buttonArray[i].Text = title;
            buttonArray[i].FocusedColor = Color.FromArgb(32, 33, 39);
            buttonArray[i].HoverState.ForeColor = Color.FromArgb(196, 153, 232);
            buttonArray[i].HoverState.FillColor = Color.FromArgb(32, 33, 39);
            buttonArray[i].Click += (sender, EventArgs) => { buttonNext_Click(sender, EventArgs); };
            buttonArray[i].Location = new Point(1, (60 * i));
            sPanel1.Controls.Add(buttonArray[i]);
            i++;
        }
        private void camp_create()
        {
            string title = sTextBox1.Texts;
            string description = sTextBox2.Texts;
            int sex = 0;
            if (guna2CheckBox1.Checked)
            {
                sex = 1;
            }
            if (guna2CheckBox2.Checked)
            {
                sex = 2;
            }
            if (guna2CheckBox1.Checked && guna2CheckBox2.Checked)
            {
                sex = 3;
            }
            string age_1 = sTextBox3.Texts;
            string age_2 = sTextBox4.Texts;
            // CREATE SCRIPT
            int script_id = script_create();
            //CREATE CAMPAIGN
            int speed = guna2TrackBar1.Value;
            //int groups = guna2RadioButton1.Checked ? 0 : 1;
            int groups = 0;
            string keywords = "";
            Database.CreateCampaign(title, description, keywords, sex, script_id, age_1, age_2, speed, groups);
        }
        private void camp_update()
        {
            try
            {
                string title = sTextBox1.Texts;
                string description = sTextBox2.Texts;
                int sex = 0;
                if (guna2CheckBox1.Checked)
                {
                    sex = 1;
                }
                if (guna2CheckBox2.Checked)
                {
                    sex = 2;
                }
                if (guna2CheckBox1.Checked && guna2CheckBox2.Checked)
                {
                    sex = 3;
                }
                string age_1 = sTextBox3.Text;
                string age_2 = sTextBox4.Text;
                // CREATE SCRIPT
                script_update();
                //int script_id = script_create();
                //CREATE CAMPAIGN
                int speed = guna2TrackBar1.Value;
                //int groups = guna2RadioButton1.Checked ? 0 : 1;
                int groups = 0;
                string keywords = "";
                Database.UpdateCampaign(camp_title, title, description, keywords, sex, age_1, age_2, speed, groups);
            }
            catch (Exception ex)
            {
                Database.addError(ex);
            }
        }
        private void script_update()
        {
            RichTextBox rtb = new();
            rtb.Rtf = richTextBox1.Rtf;
            string[] lines = richTextBox1.Lines;
            try
            {
                while (rtb.Rtf.Contains(@"{\pict"))
                {
                    try
                    {
                        var rtft = rtb.Rtf;
                        int start = rtb.Rtf.IndexOf(@"{\pict");
                        int ssStart = rtb.Rtf.IndexOf(@"pichgoal");
                        int ss = ssStart;
                        bool ok = true;
                        while (ok)
                        {
                            string substring = rtb.Rtf.Substring(ss, 1);
                            if (substring == @"}")
                            {

                                ok = false;
                                int ssend = ss - start + 1;
                                string tss = rtb.Rtf.Substring(start, ssend);
                                foreach (string emote in utils.unemotions.Keys)
                                {
                                    if (tss.Contains(emote))
                                    {
                                        rtb.Rtf = rtb.Rtf.Replace(tss, @$"{utils.unemotions[emote]}");
                                    }
                                }
                            }
                            ss++;
                        }
                    }
                    catch { }
                }


            }
            catch
            {

            }
            /*            foreach (string line in rtb.Lines)
                        {
                            string newline = line;
                            if (line.Length > 0)
                            {
                                newline = !line.Contains(@"\n") ? @$"{line} \n" : line;
                            }
                            lines[line_numb] = newline;
                            line_numb++;
                        }
                        rtb.Lines = lines;*/
            Database.UpdateScriptById(rtb.Text, scriptId);
        }
        private int script_create()
        {
            RichTextBox rtb = new();
            rtb.Rtf = richTextBox1.Rtf;
            try
            {
                while (rtb.Rtf.Contains(@"{\pict"))
                {
                    try
                    {
                        var rtft = rtb.Rtf;
                        int start = rtb.Rtf.IndexOf(@"{\pict");
                        int ssStart = rtb.Rtf.IndexOf(@"pichgoal");
                        int ss = ssStart;
                        bool ok = true;
                        while (ok)
                        {
                            string substring = rtb.Rtf.Substring(ss, 1);
                            if (substring == @"}")
                            {

                                ok = false;
                                int ssend = ss - start + 1;
                                string tss = rtb.Rtf.Substring(start, ssend);
                                foreach (string emote in utils.unemotions.Keys)
                                {
                                    if (tss.Contains(emote))
                                    {
                                        rtb.Rtf = rtb.Rtf.Replace(tss, @$"{utils.unemotions[emote]}");
                                    }
                                }
                            }
                            ss++;
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
            int sid = Database.CreateScript(rtb.Text);
            return sid;
        }
        private void GenCampBtn()
        {
            Guna.UI2.WinForms.Guna2Button btnNewCamp = new Guna.UI2.WinForms.Guna2Button();
            btnNewCamp.Size = new Size(200, 50);
            btnNewCamp.FillColor = Color.FromArgb(74, 31, 108);
            btnNewCamp.Name = "Button_newCamp";
            btnNewCamp.Text = "Добавить кампанию";
            btnNewCamp.Click += (sender, EventArgs) => { button_newCamp(sender, EventArgs); };
            btnNewCamp.Location = new Point(0, (0));
            sPanel1.Controls.Add(btnNewCamp);
        }
        private void button_newCamp(object sender, EventArgs e)
        {
            newCamp = true;
            clear_fields();
            guna2Button4.Text = "Создать кампанию";
        }
        private void updateCampView(string title)
        {
            clear_fields();
            Dictionary<int, Campaigns_struct> campaign = Database.GetCampaign(title);
            string description = campaign[0].description;
            string keywords = campaign[0].keywords;
            int sex = campaign[0].sex;
            scriptId = campaign[0].scriptId;
            int age_1 = campaign[0].age_1;
            int age_2 = campaign[0].age_2;
            int speed = campaign[0].speed;
            int groups = campaign[0].groups;
            if (guna2TrackBar1.InvokeRequired)
            {
                guna2TrackBar1.Invoke(delegate { guna2TrackBar1.Value = speed; });
                label7.Invoke(delegate { label7.Text = guna2TrackBar1.Value.ToString(); });
                sTextBox1.Invoke(delegate { sTextBox1.Texts = title; });
                sTextBox2.Invoke(delegate { sTextBox2.Texts = description; });
                sTextBox3.Invoke(delegate { sTextBox3.Texts = age_1.ToString(); });
                sTextBox4.Invoke(delegate { sTextBox4.Texts = age_2.ToString(); });
                richTextBox1.Invoke(delegate { richTextBox1.Text = Database.GetScriptById(scriptId); });
                guna2TrackBar1.Invoke(delegate { guna2TrackBar1.Value = speed; });
                switch (sex)
                {
                    case (1):
                        guna2CheckBox1.Invoke(delegate { guna2CheckBox1.Checked = true; });

                        break;
                    case (2):
                        guna2CheckBox2.Invoke(delegate { guna2CheckBox2.Checked = true; });

                        break;
                    case (3):
                        guna2CheckBox1.Invoke(delegate { guna2CheckBox1.Checked = true; });
                        guna2CheckBox2.Invoke(delegate { guna2CheckBox2.Checked = true; });
                        break;
                }
            }


            /*            if (groups == 0)
                        {
                            guna2RadioButton1.Checked = true;
                            guna2RadioButton2.Checked = false;
                        }
                        else
                        {
                            guna2RadioButton1.Checked = false;
                            guna2RadioButton2.Checked = true;
                        }*/


            //textBox3.Text = keywords;


        }
        private void buttonNext_Click(object sender, EventArgs e)
        {

            newCamp = false;
            Guna.UI2.WinForms.Guna2Button btn = (Guna.UI2.WinForms.Guna2Button)sender;
            btn.BackColor = Color.FromArgb(32, 33, 39);
            string title = btn.Text;
            camp_title = title;
            Thread updateCampViewbtn = new Thread(() => updateCampView(title));
            updateCampViewbtn.Start();
            guna2Button4.Text = "Обновить кампанию";

        }
        private void clear_fields()
        {
            if (sTextBox1.InvokeRequired)
            {
                sTextBox1.Invoke(delegate { sTextBox1.Texts = ""; });
                sTextBox2.Invoke(delegate { sTextBox2.Texts = ""; });
                sTextBox3.Invoke(delegate { sTextBox3.Texts = ""; });
                sTextBox4.Invoke(delegate { sTextBox4.Texts = ""; });
                guna2CheckBox1.Invoke(delegate { guna2CheckBox1.Checked = false; });
                guna2CheckBox2.Invoke(delegate { guna2CheckBox2.Checked = false; });
                guna2TrackBar1.Invoke(delegate { guna2TrackBar1.Value = 10; });
                richTextBox1.Invoke(delegate { richTextBox1.ResetText(); });
            }
            else
            {
                sTextBox1.Texts = "";
                sTextBox2.Texts = "";
                sTextBox3.Texts = "";
                sTextBox4.Texts = "";
                guna2CheckBox1.Checked = false;
                guna2CheckBox2.Checked = false;
                guna2TrackBar1.Value = 10;
                richTextBox1.ResetText();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            AddEmotions();
            //guna2Panel1.Visible = true;
            //richTextBox1.BackColor = Color.FromArgb(102, 74, 254);
        }
        void AddEmotions()
        {
            foreach (string emote in utils.emotions.Keys)
            {
                while (richTextBox1.Text.Contains(emote))
                {
                    int ind = richTextBox1.Text.IndexOf(emote);
                    richTextBox1.Select(ind, emote.Length);
                    string cb = Clipboard.GetText();
                    Clipboard.SetImage((Image)utils.emotions[emote]);
                    richTextBox1.Paste();
                    try
                    {
                        Clipboard.SetText(cb);
                    }
                    catch (Exception ex)
                    {
                        Database.addError(ex);
                    }
                }
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            newCamp = true;
            clear_fields();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            if (!guna2Panel1.Visible)
            {
                guna2Panel1.Visible = true;
                ResourceSet rsrcSet = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, false, true);

                int xlocation = 10;
                int ylocation = 10;
                foreach (DictionaryEntry entry in rsrcSet)
                {
                    string name = entry.Key.ToString();
                    if (name.Contains("emoji_"))
                    {
                        Bitmap bmp = (Bitmap)Properties.Resources.ResourceManager.GetObject(name);
                        Button btn = new Button();
                        btn.BackgroundImage = bmp;
                        btn.Text = "";
                        btn.Name = name;
                        btn.Location = new Point(xlocation, ylocation);
                        btn.Size = new Size(17, 17);
                        guna2Panel1.Controls.Add(btn);
                        btn.Click += btn_msg;
                        int x = guna2Panel1.Size.Width;
                        if (xlocation > x)
                        {
                            xlocation = 10;
                            ylocation += 10;
                        }
                        else
                        {
                            xlocation += 20;
                        }
                    }
                }
            }
            else
            {
                guna2Panel1.Visible = false;
            }

        }
        public void btn_msg(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string code = btn.Name.Split("emoji_")[1];
            string emoji = $"&#{code};";
            richTextBox1.Focus();
            int caretIndex = richTextBox1.SelectionStart;
            //richTextBox1.Text.Insert(caretIndex, emoji);
            //richTextBox1.Text.Insert(caretIndex, "eqwrqw");
            richTextBox1.SelectionStart = caretIndex;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectedText = emoji;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (newCamp)
            {
                camp_create();
            }
            else
            {
                camp_update();
            }
            i = 1;
            sPanel1.Controls.Clear();
            clear_fields();
            GenCampBtn();
            update();
        }
    }
}
