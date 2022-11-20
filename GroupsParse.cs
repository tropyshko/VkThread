using VkThread.Elements;

namespace VkThread
{
    internal class GroupsParse
    {
        public static void group_add(string q, bool isActive, bool city_analyze, bool age_analyze, bool sex_analyze, IProgress<int> progress_max, IProgress<int> progress_value)
        {

        }
        public static void groups_search(string q, bool search_in_name, bool active, bool city_analyze, bool age_analyze, bool sex_analyze, IProgress<int> progress_max, IProgress<int> progress_value, int min_members)
        {
            string[] keywords = q.Split(',');
            int keywordsLength = keywords.Length;
            int current_progress = 0;
            progress_value.Report(current_progress);
            progress_max.Report(keywordsLength * 1000);
            try
            {
                foreach (string keyword in keywords)
                {
                    Groups._Groups.update_keyword(keyword);
                    try
                    {
                        dynamic result = api.group_search(keyword);
                        foreach (dynamic item in result)
                        {
                            DateTime ps = DateTime.Now;
                            if (!Groups._Groups.group_parse)
                            {
                                //Groups._Groups.status("stopped");
                                progress_value.Report(0);
                                break;
                            }
                            string id = item.id.ToString();
                            try
                            {
                                current_progress++;
                                progress_value.Report(current_progress);
                                group_analytic(id, search_in_name, keyword, active, city_analyze, age_analyze, sex_analyze, min_members);
                            }
                            catch (Exception ex)
                            {
                                Database.addError(ex);
                            }
                            DateTime pe = DateTime.Now;
                            Groups._Groups.timeLeft(ps, pe);
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
        static void group_analytic(string group_id, bool search_in_name, string keyword, bool isActive, bool isCityAnalyze, bool isAgeAnalyze, bool isSexAnalyze, int min_members)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"----------------------------------------------------------");
            dynamic group_info = api.get_group(group_id);
            string ts = group_info.ToString();
            try
            {
                if (!ts.Contains("members_count"))
                {
                    Console.WriteLine(0);
                    try
                    {
                        utils.changeToken();
                    }
                    catch
                    {
                    }
                    group_analytic(group_id, search_in_name, keyword, isActive, isCityAnalyze, isAgeAnalyze, isSexAnalyze, min_members);
                }
                else
                {
                    Console.WriteLine(1);
                }
            }
            catch
            {

            }

            int can_post = group_info[0].can_post;
            string activity = group_info[0].activity;
            int members_count = group_info[0].members_count;
            string title = group_info[0].name;
            Groups._Groups.update_currentGroup(title);
            int age = 0;
            string city = "";
            int sex = 0;
            double df = 0;
            bool cont = true;
            if (members_count > min_members)
            {
                if (search_in_name)
                {
                    if (!title.Contains(keyword))
                    {
                        cont = false;
                    }
                }
                if (cont)
                {
                    if (can_post > 0)
                    {
                        DateTime last_post = group_last_post(group_id);
                        DateTime d2 = DateTime.Now;
                        if (isActive)
                        {
                            df = (d2 - last_post).TotalDays;
                        }
                        if (df < 1)
                        {
                            Console.WriteLine($"Название группы: {title}");
                            Console.WriteLine($"Участников: {members_count}");
                            Console.WriteLine($"Последний пост в группе: {last_post}");
                            if (isAgeAnalyze || isCityAnalyze || isSexAnalyze)
                            {
                                Console.WriteLine($"Сбор пользователей {group_id}");
                                List<Newtonsoft.Json.Linq.JObject> members = new List<Newtonsoft.Json.Linq.JObject> { };
                                int x = 0;
                                bool parse_status = true;
                                while (parse_status)
                                {
                                    try
                                    {
                                        dynamic member = api.get_group_members(group_id, members.Count);
                                        foreach (Newtonsoft.Json.Linq.JObject mem in member)
                                        {
                                            members.Add(mem);
                                        }
                                        x++;
                                    }
                                    catch
                                    {
                                        parse_status = false;
                                    }
                                }
                                if (isAgeAnalyze)
                                {
                                    Console.WriteLine($"Анализ возраста {group_info[0].name}");
                                    Dictionary<string, int> ages = group_users_age(members);
                                    int all_age = 0;
                                    int count_age = 0;
                                    foreach (string key in ages.Keys)
                                    {
                                        int year = int.Parse(key);
                                        all_age += year * ages[key];
                                        count_age += ages[key];
                                    }
                                    age = 2022 - all_age / count_age;
                                    Console.WriteLine($"Medium: {age}");
                                }
                                if (isSexAnalyze)
                                {
                                    Console.WriteLine($"Анализ пола {group_info[0].name}");
                                    Dictionary<int, int> sexx = group_users_sex(members);
                                    var sortedDict1 = from entry in sexx orderby entry.Value descending select entry;
                                    sex = sortedDict1.First().Key;
                                    Console.WriteLine($"Больше: {sex}");
                                }
                                if (isCityAnalyze)
                                {
                                    Console.WriteLine($"Анализ городов {group_info[0].name}");
                                    Dictionary<string, int> cities = group_users_city(members);
                                    var sortedDict = from entry in cities orderby entry.Value descending select entry;
                                    city = sortedDict.First().Key;
                                    Console.WriteLine($"Город: {city}");
                                }
                            }

                            string userId = "1";
                            string url = $"http://vk.com/club{group_id}";
                            try
                            {
                                Console.WriteLine("Добавление в БД");
                                Groups._Groups.update_table(url, last_post.ToString(), city, keyword, activity, userId, age, sex, members_count, title);
                                Database.add_group(url, last_post.ToString(), city, keyword, activity, Config.userId, age, sex, members_count, title);
                            }
                            catch (Exception ex)
                            {
                                Database.addError(ex);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Группа неактивна");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Группа закрыта");
                    }
                }
            }



        }
        static DateTime group_last_post(string group_id)
        {
            dynamic req = api.get_group_lastPost(group_id);
            DateTime dater = new();
            foreach (var item in req.response.items)
            {
                try
                {
                    if (!item.is_pinned)
                    {
                        string date = item.date;
                        dater = utils.DateTimeFromUnixTime(date);
                    }
                }
                catch
                {
                    string date = item.date;
                    dater = utils.DateTimeFromUnixTime(date);
                }
            }
            return dater;
        }
        private static Dictionary<int, int> group_users_sex(dynamic req)
        {
            var sex = new Dictionary<int, int>();
            foreach (var member in req)
            {
                if (member.sex == 1)
                {
                    try
                    {
                        sex.Add(1, 1);
                    }
                    catch
                    {
                        sex[1]++;
                    }
                }
                else if (member.sex == 2)
                {
                    try
                    {
                        sex.Add(2, 1);
                    }
                    catch
                    {
                        sex[2]++;
                    }
                }
                else
                {
                    try
                    {
                        sex.Add(3, 1);
                    }
                    catch
                    {
                        sex[3]++;
                    }
                }
            }
            return sex;
        }
        private static Dictionary<string, int> group_users_city(dynamic req)
        {
            List<string> cities = new List<string>();
            cities.Sort();
            foreach (var member in req)
            {
                try
                {
                    string member_city = member.city.title;
                    cities.Add(member_city);
                }
                catch (Exception ex)
                {
                    Database.addError(ex);
                }
            }
            var c_result = new Dictionary<string, int>();
            foreach (var city in cities)
            {
                try
                {
                    c_result.Add(city, 1);
                }
                catch
                {
                    c_result[city]++;
                }
            }
            return c_result;
        }
        private static Dictionary<string, int> group_users_age(dynamic req)
        {
            List<string> ages = new List<string>();
            foreach (var member in req)
            {
                try
                {
                    string bdate = member.bdate;
                    string[] words = bdate.Split(new char[] { '.' });
                    string byear = words[2];
                    ages.Add(byear);
                }
                catch (Exception ex)
                {
                    Database.addError(ex);
                }
            }
            Dictionary<string, int> result = new Dictionary<string, int>();
            ages.Sort();
            foreach (var age in ages)
            {
                try
                {
                    result.Add(age, 1);
                }
                catch
                {
                    result[age]++;
                }
            }
            return result;
        }
    }
}
