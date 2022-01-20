namespace Data.LinkShortener.ViewModel.User
{
    public class LoginRegisterViewModel
    {
        public string LoginName { get; set; }
        public string LoginPassword { get; set; }
        public string ForgetName { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string RedirectUrl { get; set; }
        public bool IsLogin { get; set; }
        public bool IsPassword { get; set; }
        public bool IsRegister { get; set; }
        public bool HasMessage { get; set; }
    }
}
