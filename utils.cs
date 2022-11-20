using System.Collections;
using System.Net;
using System.Text;

namespace VkThread;
internal class utils
{

    public static Hashtable emotions;
    public static void CreateEmotions()
    {
        emotions = new Hashtable(15);
        emotions.Add("&#9989;", Properties.Resources.emoji_9989);
        emotions.Add("&#128113;", Properties.Resources.emoji_128113);
        emotions.Add("&#128101;", Properties.Resources.emoji_128101);
        emotions.Add("&#10002;", Properties.Resources.emoji_10002);
    }

    public static Hashtable unemotions;
    public static void RemoveEmotions()
    {
        unemotions = new Hashtable(6);
        unemotions.Add(
            "27f7de73734f1e961002ff81350c877dbb9f2031850230305ffa2dd9b258c8fe546bd0cc444eec",
            "&#9989;");
        unemotions.Add(
            "147dfd4161e0e481debf86d59196c7b58d79ade5d1e3bbce55c34767652abe951f4bc5757c0f47",
            "&#128101;");
        unemotions.Add(
            "35ff5a2ccef25a965059e76ba9d8a8da3b4bee1315e721b3a50a53209412ce85ab0e3e61abebe0",
            "&#128113;");
        unemotions.Add(
            "2f9720541512606e2d4c6711173d75e5d5ad77df90ed39fdfc7191b5f297dfff18fffe86aa0503",
            "&#10002;");
    }
    public static DateTime DateTimeFromUnixTime(string date)
    {
        long unixTimeStamp = long.Parse(date);
        return DateTimeOffset
             .FromUnixTimeSeconds(unixTimeStamp)
             .UtcDateTime;
    }
    public static bool check_proxy(string ip, string port, string username, string password)
    {
        try
        {
            HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("http://www.vk.com");
            WebProxy myProxy = new WebProxy();
            string proxyAddress = $"http://{ip}:{port}";
            Uri newUri = new Uri(proxyAddress);
            myProxy.Address = newUri;
            myProxy.Credentials = new NetworkCredential(username, password);
            myWebRequest.Proxy = myProxy;
            HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            return myWebResponse.StatusCode == HttpStatusCode.OK;
        }
        catch
        {
            return false;
        }
    }

    public static Dictionary<string, int> getStatViews(string linkId)
    {

        dynamic resp = api.getLinkStats(linkId, "day");
        dynamic stats = resp.response.stats;
        Dictionary<string, int> tags = new Dictionary<string, int>();
        foreach (dynamic stat in stats)
        {
            string date = stat.timestamp;
            DateTime timestamp = utils.DateTimeFromUnixTime(date);
            int views = stat.views;

            tags.Add(timestamp.ToString(), views);

        }
        return tags;

    }
    public static dynamic getStat(string linkId)
    {
        dynamic resp = api.getLinkStats(linkId, "day");
        dynamic stats = resp.response.stats;
        return stats;
    }
    public static string sha256(string randomString)
    {
        var crypt = new System.Security.Cryptography.SHA256Managed();
        var hash = new System.Text.StringBuilder();
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
        foreach (byte theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }
        return hash.ToString();
    }
    public static void changeToken()
    {
        Database.TokenLimit();
        string token = Database.getToken(); ;
        Config.token = token;
    }
    public static bool checkDate(string dateUntil)
    {
        DateTime date = DateTime.Now;
        DateTime until = DateTime.Parse(dateUntil);
        TimeSpan diff = date - until;
        return diff.Days > 1;
    }
}