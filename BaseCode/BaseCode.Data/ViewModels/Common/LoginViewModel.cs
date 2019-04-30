namespace BaseCode.Data.ViewModels.Common
{
    public class LoginViewModel
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public bool isAdmin { get; set; }
    }
}
