using Newtonsoft.Json;
using System.Net;

namespace VkThread;
public class ShortLinkResponse
{
    public string response { get; set; }
}
public class ShortLink
{
    public string key { get; set; }
}
internal class api
{
    public static string request_api(string url)
    {
        WebRequest request = WebRequest.Create(url);
        request.Credentials = CredentialCache.DefaultCredentials;
        WebResponse response = request.GetResponse();
        using (Stream dataStream = response.GetResponseStream())
        {
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            response.Close();
            return responseFromServer;
        }
    }
    public static dynamic getLinkStats(string key, string interval = "day")
    {
        string url = $"https://api.vk.com/method/utils.getLinkStats?key={key}" +
                $"&interval={interval}&" +
                $"extended=1&" +
                $"interval=day&" +
                $"intervals_count=30&" +
                $"access_token={Config.token}&" +
                $"v=5.131";
        string req = request_api(url);
        dynamic stuff = JsonConvert.DeserializeObject(req);
        return stuff;
    }
    public static dynamic getLinkStatsDay(string key, string interval = "day")
    {
        string url = $"https://api.vk.com/method/utils.getLinkStats?key={key}" +
                $"&interval={interval}&" +
                $"extended=1&" +
                $"interval=day&" +
                $"intervals_count=1&" +
                $"access_token={Config.token}&" +
                $"v=5.131";
        string req = request_api(url);
        dynamic stuff = JsonConvert.DeserializeObject(req);
        return stuff;
    }
    public static string getShortLink(string link)
    {
        try
        {
            string url = $"https://api.vk.com/method/utils.getShortLink?" +
                $"access_token={Config.token}" +
                $"&url={link}" +
                "&v=5.131";
            string req = request_api(url);
            dynamic stuff = JsonConvert.DeserializeObject(req);
            string key = stuff.response.key;
            return key;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return "";
        }
    }
    public static dynamic get_group(string group_id)
    {
        Thread.Sleep(600);
        string url = $"https://api.vk.com/method/groups.getById?" +
            $"access_token={Config.token}" +
            $"&group_id={group_id}" +
            "&fields=can_post,activity,members_count" +
            "&v=5.131";
        string req = request_api(url);
        dynamic stuff = JsonConvert.DeserializeObject(req);
        dynamic memb = stuff.response;
        return !stuff.ContainsKey("response") ? "Limit" : memb;
    }
    public static dynamic get_group_members(string group_id, int offset = 0)
    {

        string url = $"https://api.vk.com/method/groups.getMembers?" +
            $"access_token={Config.token}" +
            $"&group_id={group_id}" +
            "&fields=bdate,city,sex" +
            $"&count=1000" +
            $"&offset={offset}" +
            "&v=5.131";
        string req = request_api(url);
        dynamic stuff = JsonConvert.DeserializeObject(req);
        dynamic memb = stuff.response.items;
        return memb;

    }
    public static dynamic get_group_lastPost(string group_id)
    {

        string url = $"https://api.vk.com/method/wall.get?" +
            $"access_token={Config.token}" +
            $"&owner_id=-{group_id}" +
            $"&count=2" +
            "&v=5.131";
        string req = request_api(url);
        dynamic stuff = JsonConvert.DeserializeObject(req);
        return stuff;

    }
    public static dynamic groups_search(string q)
    {
        Thread.Sleep(600);
        try
        {
            string url = $"https://api.vk.com/method/groups.search?" +
                $"access_token={Config.token}" +
                $"&q={q}" +
                "&sort=0" +
                $"&count=1000" +
                "&v=5.131";
            string req = request_api(url);
            dynamic stuff = JsonConvert.DeserializeObject(req);
            try
            {
                dynamic memb = stuff.response.items;
                return memb;
            }
            catch (Exception ex)
            {
                Database.addError(ex, stuff);
            }
        }
        catch (Exception ex)
        {
            Database.addError(ex);

        }

        return null;
    }
    public static dynamic getLinkStatsAll(string key, string interval = "day")
    {
        string url = $"https://api.vk.com/method/utils.getLinkStats?key={key}" +
            $"&interval={interval}&" +
            $"extended=1&" +
            $"interval=day&" +
            $"intervals_count=100&" +
            $"access_token={Config.service_key}&" +
            $"v=5.131";
        string req = request_api(url);
        dynamic stuff = JsonConvert.DeserializeObject(req);
        return stuff;
    }
    public static dynamic group_search(string q)
    {
        Thread.Sleep(600);
        try
        {
            string url = $"https://api.vk.com/method/groups.search?" +
                $"access_token={Config.token}" +
                $"&q={q}" +
                "&sort=0" +
                $"&count=1000" +
                "&v=5.131";
            string req = request_api(url);
            dynamic stuff = JsonConvert.DeserializeObject(req);
            dynamic group = stuff.response.items;
            return group;
        }
        catch (Exception)
        {
        }
        return null;
    }
    public static string profileInfo()
    {
        string token = Database.getUserToken();
        string fullname = "";
        try
        {
            string url = $"https://api.vk.com/method/account.getProfileInfo?" +
                $"access_token={Config.token}" +
                "&v=5.131";
            string req = request_api(url);
            dynamic stuff = JsonConvert.DeserializeObject(req);
            string firstname = stuff.response.first_name;
            string lastname = stuff.response.last_name;
            fullname = $"{firstname} {lastname}";
        }
        catch
        {

        }
        return fullname;
    }
}
