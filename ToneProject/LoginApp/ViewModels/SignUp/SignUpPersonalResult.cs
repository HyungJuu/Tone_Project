namespace LoginApp.ViewModels.SignUp
{
    public class SignUpPersonalResult(bool isValid, string nameStatus, string birthStatus)
    {
        public bool IsValid { get; set; } = isValid;
        public string NameStatus { get; set; } = nameStatus;
        public string BirthStatus { get; set; } = birthStatus;
    }
}
