namespace VkThread.Elements
{
    public partial class Browser : Form
    {
        public Browser()
        {
            InitializeComponent();
        }

        private void Browser_Load(object sender, EventArgs e)
        {
            initBrowser();
        }
        private async Task initializated()
        {
            await webView.EnsureCoreWebView2Async(null);
        }
        private async void initBrowser()
        {
            await initializated();
            webView.CoreWebView2.Navigate($"https://oauth.vk.com/authorize?client_id={Config.app_id}&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends,offline&response_type=token&v=5.131&state=12");
        }

        private void webView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
        }

        private void webView_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
            string current_url = webView.Source.ToString();
            if (current_url.Contains("blank.html#access_token="))
            {
                string split1 = current_url.Split("blank.html#access_token=")[1];
                string code = split1.Split("&")[0];
                edit_config(code);
                update_key(code);
                this.Close();
            }
        }
        private static void edit_config(string secret_key)
        {
            Database.UpdateToken(secret_key);
            Database.addToken(Config.username, secret_key);
        }
        private static void update_key(string secret_key)
        {
            Config.token = secret_key;
        }

        private void webView21_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {

        }
    }
}
