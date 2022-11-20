using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Text.Json;

namespace VkThread;
public class User_struct
{
    public int Id { get; set; }
    public string Number { get; set; }
    public string Password { get; set; }
    public string Proxy { get; set; }
    public int ScriptId { get; set; }
    public string Status { get; set; }
    public string BuyAt { get; set; }
    public string userId { get; set; }
}
public class Profile_struct
{
    public int id { get; set; }
    public string fullname { get; set; }
    public string balance { get; set; }
    public string status { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string vk_token { get; set; }
}
public class Group_struct
{
    public string? Id { get; set; }
    public string? Url { get; set; }
    public string last_post { get; set; }
    public string city { get; set; }
    public string keywords { get; set; }
    public string category { get; set; }
    public string medium_age { get; set; }
    public string sex { get; set; }
    public string members { get; set; }
    public string title { get; set; }
    public string add_date { get; set; }

}
public class Script_struct
{
    public int Id { get; set; }
    public string Script_text { get; set; }
}
public class Scripts_struct
{
    public int id { get; set; }
    public string text { get; set; }
    public string view { get; set; }
    public string title { get; set; }
}
public class Campaigns_struct
{
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string keywords { get; set; }
    public int sex { get; set; }
    public int scriptId { get; set; }
    public int age_1 { get; set; }
    public int age_2 { get; set; }
    public int speed { get; set; }
    public int groups { get; set; }
}
public class City_struct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CityId { get; set; }
}
public class Proxy_struct
{
    public string ip { get; set; }
    public string port { get; set; }
    public string login { get; set; }
    public string password { get; set; }
}
public class Links_struct
{
    public string url { get; set; }
    public string shortId { get; set; }
    public string comment { get; set; }
}
class Database
{
    public static Dictionary<int, Campaigns_struct> GetCampaigns()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        var campaignsDictionary = new Dictionary<int, Campaigns_struct>();

        try
        {
            string sql = $"SELECT * FROM `campaigns` WHERE userId = '{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt16(0);
                string title = reader.GetString(1);
                campaignsDictionary.Add(id,
                new Campaigns_struct
                {
                    id = id,
                    title = title,
                });
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
        return campaignsDictionary;
    }
    public static Dictionary<int, Campaigns_struct> GetCampaign(string? campTitle)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        var campaignsDictionary = new Dictionary<int, Campaigns_struct>();

        try
        {
            string sql = $"SELECT * FROM `campaigns` WHERE title = '{campTitle}' and userId = {Config.userId}";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt16(0);
                string title = reader.GetString(1);
                string description = reader.GetString(2);
                string keywords = reader.GetString(3);
                int sex = reader.GetInt16(4);
                int scriptId = reader.GetInt16(5);
                int age_1 = reader.GetInt16(7);
                int age_2 = reader.GetInt16(8);
                int speed = reader.GetInt16(10);
                int groups = reader.GetInt16(11);
                campaignsDictionary.Add(0,
                new Campaigns_struct
                {
                    id = id,
                    title = title,
                    description = description,
                    keywords = keywords,
                    sex = sex,
                    scriptId = scriptId,
                    age_1 = age_1,
                    age_2 = age_2,
                    speed = speed,
                    groups = groups,
                });
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
        return campaignsDictionary;
    }
    public static Dictionary<int, User_struct> GetAccounts()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        var usersDictionary = new Dictionary<int, User_struct>();
        try
        {
            string sql = $"Select * from accounts WHERE userId = '{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int Id = reader.GetInt16(0);
                string? Number = reader.GetString(1);
                string? Password = reader.GetString(2);
                string? Proxy = reader.GetString(3);
                int ScriptId = reader.GetInt16(4);
                string? Status = reader.GetString(5);
                string? BuyAt = reader.GetString(6);
                string? userId = reader.GetString(7);
                usersDictionary.Add(Id,
                    new User_struct
                    {
                        Id = Id,
                        Number = Number,
                        Password = Password,
                        ScriptId = ScriptId,
                        Proxy = Proxy,
                        Status = Status,
                        BuyAt = BuyAt,
                        userId = userId
                    });
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
        return usersDictionary;
    }
    public static Dictionary<int, Group_struct> GetSelfGroups()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        var groupsDictionary = new Dictionary<int, Group_struct>();
        try
        {
            string sql = $"SELECT * FROM `groups` WHERE userId='{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int groupId = reader.GetInt16(0);
                string url = reader.GetString(1);
                string lastPost = reader.GetString(2);
                string city = reader.GetString(3);
                string keywords = reader.GetString(4);
                string category = reader.GetString(5);
                string userId = reader.GetString(6);
                string medium_age = reader.GetString(7);
                string sex = reader.GetString(8);
                string members = reader.GetString(9);
                string title = reader.GetString(10);
                string add_date = reader.GetString(11);

                groupsDictionary.Add(groupId,
                    new Group_struct
                    {
                        Url = url,
                        last_post = lastPost,
                        city = city,
                        keywords = keywords,
                        category = category,
                        medium_age = medium_age,
                        sex = sex,
                        members = members,
                        title = title,
                        add_date = add_date,
                    });
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
        return groupsDictionary;
    }
    public static Dictionary<int, Group_struct> GetGroups()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        var groupsDictionary = new Dictionary<int, Group_struct>();
        try
        {
            string sql = "SELECT * FROM `groups` WHERE 1";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int groupId = reader.GetInt16(0);
                string url = reader.GetString(1);
                string lastPost = reader.GetString(2);
                string city = reader.GetString(3);
                string keywords = reader.GetString(4);
                string category = reader.GetString(5);
                string userId = reader.GetString(6);
                string medium_age = reader.GetString(7);
                string sex = reader.GetString(8);
                string members = reader.GetString(9);
                string title = reader.GetString(10);
                string add_date = reader.GetString(11);

                groupsDictionary.Add(groupId,
                    new Group_struct
                    {
                        Url = url,
                        last_post = lastPost,
                        city = city,
                        keywords = keywords,
                        category = category,
                        medium_age = medium_age,
                        sex = sex,
                        members = members,
                        title = title,
                        add_date = add_date,
                    });
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
        return groupsDictionary;
    }
    public static Dictionary<int, Group_struct> GetGroupsSelf()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        var groupsDictionary = new Dictionary<int, Group_struct>();
        try
        {
            string sql = $"SELECT * FROM `groups` WHERE userId='{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int groupId = reader.GetInt16(0);
                string url = reader.GetString(1);
                string lastPost = reader.GetString(2);
                string city = reader.GetString(3);
                string keywords = reader.GetString(4);
                string category = reader.GetString(5);
                string userId = reader.GetString(6);
                string medium_age = reader.GetString(7);
                string sex = reader.GetString(8);
                string members = reader.GetString(9);
                string title = reader.GetString(10);
                string add_date = reader.GetString(11);

                groupsDictionary.Add(groupId,
                    new Group_struct
                    {
                        Url = url,
                        last_post = lastPost,
                        city = city,
                        keywords = keywords,
                        category = category,
                        medium_age = medium_age,
                        sex = sex,
                        members = members,
                        title = title,
                        add_date = add_date,
                    });
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
        return groupsDictionary;
    }
    public static string GetScript(int sid)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        string script = "";
        try
        {
            string sql = $"Select * from scripts WHERE id = '{sid}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int scriptId = reader.GetInt16(0);
                script = reader.GetString(1);
            }

        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
        return script;

    }
    public static string GetWSwaiting()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        string status = "";
        try
        {
            string sql = $"SELECT Value FROM config WHERE Title = 'Work_status'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                status = reader.GetString(0);
            }

        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
        return status;
    }
    public static Dictionary<int, Profile_struct> GetUser(string username, string password)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        var profileDictionary = new Dictionary<int, Profile_struct>();
        try
        {
            string sql = $"Select * from users WHERE username = '{username}' AND password = '{password}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int userId = reader.GetInt16(0);
                string? fullname = reader.GetString(1);
                string? balance = reader.GetString(2);
                string? status = reader.GetString(3);
                string? vk_token = reader.GetString(8);
                profileDictionary.Add(0,
                    new Profile_struct
                    {
                        id = userId,
                        fullname = fullname,
                        balance = balance,
                        status = status,
                        username = username,
                        password = password,
                        vk_token = vk_token
                    });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
        return profileDictionary;
    }
    public static void UpdateToken(string token)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"Update users set vk_token = '{token}' where id = '{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            // Выполнить Command (Использованная для delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();
            Console.WriteLine("Row Count affected = " + rowCount);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public static void addToken(string number, string token)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"INSERT INTO `tokens_list`(`number`, `token`) VALUES ('{number}','{token}')";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            int rowCount = cmd.ExecuteNonQuery();
            Console.WriteLine("Row Count affected = " + rowCount);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public static int GetStats(int id)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        int stats = 0;
        try
        {
            string sql = $"SELECT posts_count FROM stats WHERE user_id = '{id}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                stats += reader.GetInt16(0);
            }

        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
        return stats;
    }
    public static int GetStat(int id)
    {
        string date = DateTime.Now.ToString("yyyy-M-dd");
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        int stats = 0;
        try
        {
            string sql = $"SELECT posts_count FROM stats WHERE user_id = '{id}' and datet = '{date}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                stats += reader.GetInt16(0);
            }

        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
        return stats;
    }
    public static void SetStatus(string status)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"Update config set Value = '{status}' where Title = 'Work_status'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            // Выполнить Command (Использованная для delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();
            Console.WriteLine("Row Count affected = " + rowCount);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public static void add_group(string url, string last_post, string city, string keywords, string category, string userId, int medium_age, int sex, int members, string title)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"INSERT INTO `groups`(`url`, `last_post`, `city`, `keywords`, `category`, `userId`, `medium_age`, `sex`, `members`, `title`) VALUES ('{url}','{last_post}','{city}','{keywords}','{category}','{userId}',{medium_age},{sex},{members},'{title}')";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            int rowCount = cmd.ExecuteNonQuery();
            Console.WriteLine("Row Count affected = " + rowCount);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public static Dictionary<int, City_struct> GetCities()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        var groupsDictionary = new Dictionary<int, City_struct>();
        try
        {
            string sql = "SELECT * FROM `cities` WHERE 1";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int groupId = reader.GetInt16(0);
                string Name = reader.GetString(1);
                int CityId = reader.GetInt32(2);

                groupsDictionary.Add(groupId,
                new City_struct
                {
                    Name = Name,
                    CityId = CityId,
                });
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
        return groupsDictionary;
    }
    public static void add_user(string ip, string port, string login, string password, string number, string pass)
    {
        var Proxy_struct = new Proxy_struct
        {
            ip = ip,
            port = port,
            login = login,
            password = password,
        };
        string proxy = JsonSerializer.Serialize(Proxy_struct);
        DateTime now = DateTime.Now;
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"INSERT INTO `accounts`(`number`,`password`,`proxy`,`script_id`,`status`,`buy_at`,`userId`) VALUES ('{number}','{pass}','{proxy}','0','Ready','{now}','{Config.userId}')";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            int rowCount = cmd.ExecuteNonQuery();

            Console.WriteLine("Row Count affected = " + rowCount);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;

        }
    }
    public static Dictionary<int, Links_struct> GetLinksList()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        var profileDictionary = new Dictionary<int, Links_struct>();
        try
        {
            string sql = $"SELECT * FROM links WHERE userId='{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int Id = reader.GetInt16(0);
                string? url = reader.GetString(1);
                string? shortId = reader.GetString(2);
                string? comment = reader.GetString(3);
                profileDictionary.Add(Id,
                    new Links_struct
                    {
                        url = url,
                        shortId = shortId,
                        comment = comment,
                    });
            }
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
        return profileDictionary;
    }
    public static void add_shortLink(string url, string short_id, string commentary)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"INSERT INTO `links`(`url`,`short_id`,`commentary`,`userId`) VALUES ('{url}','{short_id}','{commentary}','{Config.userId}')";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            int rowCount = cmd.ExecuteNonQuery();
            Console.WriteLine("Row Count affected = " + rowCount);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public static void SetWS(int userId, int status)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"Update campaigns set status = {status} where scriptId = '{userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            // Выполнить Command (Использованная для delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
    }
    public static void SetWSbyId(int userId, int status, string accounts = "")
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"Update campaigns set status = {status}, accounts= '{accounts}' where id = '{userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            // Выполнить Command (Использованная для delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
    }
    public static Dictionary<int, Scripts_struct> GetScripts()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        var groupsDictionary = new Dictionary<int, Scripts_struct>();

        try
        {
            string sql = $"SELECT * FROM `scripts` WHERE userId = '{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt16(0);
                string text = reader.GetString(1);
                string view = reader.GetString(2);
                string title = reader.GetString(3);

                groupsDictionary.Add(id,
                new Scripts_struct
                {
                    id = id,
                    text = text,
                    view = view,
                    title = title,
                });
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
        return groupsDictionary;
    }
    public static string GetScriptText(string title)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        string stats = "";
        try
        {
            string sql = $"SELECT text FROM scripts WHERE userId = '{Config.userId}' and title = '{title}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                stats += reader.GetString(0);
            }

        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
        return stats;
    }
    public static string GetScriptById(int scriptId)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        string stats = "";
        try
        {
            string sql = $"SELECT text FROM scripts WHERE id = '{scriptId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                stats += reader.GetString(0);
            }

        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
        return stats;
    }
    public static void UpdateScriptText(string text, string title)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"UPDATE scripts SET text= '{text}' WHERE userId = '{Config.userId}' and title = '{title}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            int rowCount = cmd.ExecuteNonQuery();


        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
    }
    public static void UpdateScriptById(string text, int id)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"UPDATE scripts SET text= '{text}' WHERE userId = '{Config.userId}' and id = '{id}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            int rowCount = cmd.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();

    }
    public static int CreateScript(string text)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        ulong id = 0;
        try
        {
            string sql = @$"INSERT INTO scripts(text,userId) VALUES ('{text}','{Config.userId}')";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            int rowCount = cmd.ExecuteNonQuery();

            string sql2 = @$"SELECT LAST_INSERT_ID();";
            MySqlCommand cmd2 = new MySqlCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = sql2;
            id = (ulong)cmd2.ExecuteScalar();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
        return Convert.ToInt32(id);
    }
    public static void RemoveAccount(string account_number)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"DELETE FROM `accounts` WHERE number = '{account_number}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            // Выполнить Command (Использованная для delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();
            Console.WriteLine("Row Count affected = " + rowCount);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public static void RemoveGroup(string url)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"DELETE FROM `groups` WHERE url = '{url}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            // Выполнить Command (Использованная для delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();
            Console.WriteLine("Row Count affected = " + rowCount);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public static void RemoveMyGroups()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"DELETE FROM `groups` WHERE userId = '{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            // Выполнить Command (Использованная для delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();
            Console.WriteLine("Row Count affected = " + rowCount);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public static void UpdateCampaign(string campTitle, string title, string description, string keywords, int sex, string age_1, string age_2, int speed, int groups)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"UPDATE campaigns SET " +
                $"`title`= '{title}'," +
                $"`description`='{description}'," +
                $"`keywords` = '{keywords}'," +
                $"`sex`='{sex}'," +
                $"`age_1`='{age_1}'," +
                $"`age_2`='{age_2}'," +
                $"`speed`='{speed}'," +
                $"`groups`='{groups}'" +
                $"WHERE `userId` = '{Config.userId}' and " +
                $"`title` = '{campTitle}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            int rowCount = cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
    }
    public static void CreateCampaign(string title, string description, string keywords, int sex, int scriptId, string age_1, string age_2, int speed, int groups)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"INSERT INTO `campaigns`(" +
                $"`title`, " +
                $"`description`, " +
                $"`keywords`, " +
                $"`sex`, " +
                $"`scriptId`, " +
                $"`userId`," +
                $"`age_1`, " +
                $"`age_2`, " +
                $"`speed`," +
                $"`groups`) " +
                $"VALUES (" +
                $"'{title}'," +
                $"'{description}'," +
                $"'{keywords}'," +
                $"'{sex}'," +
                $"'{scriptId}'," +
                $"'{Config.userId}'" +
                $",{age_1}" +
                $",{age_2}" +
                $",'{speed}'" +
                $",'{groups}')";

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            int rowCount = cmd.ExecuteNonQuery();
            Console.WriteLine("Row Count affected = " + rowCount);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public static int add_user(string login, string password, string email, string invite_code)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = @$"INSERT INTO users (username, password,email,invite_code) VALUES ('{login}','{password}','{email}','{invite_code}')";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            int rowCount = cmd.ExecuteNonQuery();
            Console.WriteLine("Row Count affected = " + rowCount);
        }
        catch (Exception e)
        {
            string err = e.ToString();
            if (err.Contains("Duplicate entry"))
            {
                return 2;
            }
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
            return 1;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
        return 0;
    }
    public static int GetWS(int scriptId)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        int status = 0;
        try
        {
            string sql = $"SELECT status FROM campaigns WHERE scriptId = '{scriptId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                status = reader.GetInt32(0);
            }

        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
        return status;
    }
    public static int GetWSbyId(int scriptId)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        int status = 0;
        try
        {
            string sql = $"SELECT status FROM campaigns WHERE id = '{scriptId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                status = reader.GetInt32(0);
            }

        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
        return status;
    }
    public static int GetDayStat()
    {
        DateTime dateNow = DateTime.Now;
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        int stats = 0;
        try
        {
            string sql = $"SELECT post_time FROM posts_stat WHERE userId = '{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string datee = reader.GetString(0);
                DateTime post = DateTime.Parse(datee);
                if (post.Day == dateNow.Day && post.Month == dateNow.Month)
                {
                    stats += 1;
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
        return stats;
    }
    /*    public static Dictionary<int, string> GetLinks(string userId)
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            var linksDict = new Dictionary<int, string>();
            try
            {
                string sql = $"SELECT * FROM `links` WHERE userId='{userId}'";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                using DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt16(0);
                    string link = reader.GetString(2);
                    linksDict.Add(id, link);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            conn.Close();
            return linksDict;
        }*/
    public static string checkToken(string userId)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        string stats = "";
        try
        {
            string sql = $"SELECT vk_token FROM users WHERE id = '{userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                stats += reader.GetString(0);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
        return stats;
    }
    public static void updatePassword(string old_password, string new_password)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"Update users set password = {new_password} where password = '{old_password}' and username='{Config.username}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            // Выполнить Command (Использованная для delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
    }
    public static void deleteToken()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"Update users set vk_token = '' WHERE username='{Config.username}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            // Выполнить Command (Использованная для delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
    }
    public static string checkInvite(string invite_code)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        string stats = "";
        try
        {
            string sql = $"SELECT free FROM invites WHERE code = '{invite_code}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                stats += reader.GetString(0);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
        return stats;
    }
    public static void updateInvite(string invite_code)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"Update invites set free = 'no' where code = '{invite_code}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            // Выполнить Command (Использованная для delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
    }
    public static Dictionary<int, string> GetLinks()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        var linksDict = new Dictionary<int, string>();
        try
        {
            //string sql = $"SELECT * FROM links WHERE userId='{Config.userId}'";
            string sql = $"SELECT * FROM links WHERE userId='{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string link = reader.GetString(2);
                linksDict.Add(id, link);
            }
        }
        catch (Exception)
        {

        }
        finally
        {
        }
        return linksDict;
    }
    public static long postsCount()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        long count = 0;
        MySqlCommand cmd = new();
        try
        {
            //string sql = $"SELECT COUNT(*) FROM posts_stat WHERE userId='{Config.userId}'";
            string sql = $"SELECT COUNT(*) FROM posts_stat WHERE userId='{Config.userId}'";
            cmd.Connection = conn;
            cmd.CommandText = sql;
            try
            {
                conn.Open();
                count = (long)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        catch (Exception Ex)
        {
            Console.WriteLine(Ex);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return count;
    }
    public static int addError(Exception ex, string msg = "")
    {
        if (msg.Length < 1)
        {
            msg = ex.ToString();
        }
        string error = ex.ToString();
        string errorPath = ex.StackTrace.ToString();
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = @$"INSERT INTO errors (datetime,userId,error,errorPath,msg) VALUES ('{DateTime.Now}','{Config.userId}','{error}','{errorPath}','{msg}')";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            int rowCount = cmd.ExecuteNonQuery();
            Console.WriteLine("Row Count affected = " + rowCount);
        }
        catch (Exception e)
        {
            string err = e.ToString();
            if (err.Contains("Duplicate entry"))
            {
                return 2;
            }
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
            return 1;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
        return 0;
    }
    public static void ClearStats()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"DELETE FROM `posts_stat` WHERE userId = '{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            // Выполнить Command (Использованная для delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();
            Console.WriteLine("Row Count affected = " + rowCount);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }
    }
    public static string getToken()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            //string sql = $"SELECT * FROM links WHERE userId='{Config.userId}'";
            string sql = $"SELECT * FROM links WHERE userId='{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string token = reader.GetString(2);
                string dateT = reader.GetString(3);
                if (utils.checkDate(dateT))
                {
                    return token;
                }
            }
        }
        catch (Exception)
        {

        }
        finally
        {
        }
        return null;
    }
    public static void TokenLimit()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        string until = DateTime.Now.ToString();
        try
        {
            string sql = $"Update `tokens_list` set `limit_until` = '{until}' where `token` = '{Config.token}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            // Выполнить Command (Использованная для delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
    }
    public static string getPassword()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        string stats = "";
        try
        {
            string sql = $"SELECT `password` FROM `users` WHERE `id` = '{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                stats += reader.GetString(0);
            }

        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
        return stats;
    }
    public static string getUserToken()
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        string stats = "";
        try
        {
            string sql = $"SELECT `vk_token` FROM `users` WHERE `id` = '{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                stats += reader.GetString(0);
            }

        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Console.Read();
        conn.Close();
        return stats;
    }
    public static void updatePassword(string newPassword)
    {
        MySqlConnection conn = DBUtils.GetDBConnection();
        conn.Open();
        try
        {
            string sql = $"Update `users` set `password` = '{newPassword}' where `id` = '{Config.userId}'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            // Выполнить Command (Использованная для delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        conn.Close();
    }
}
