namespace LoginApp.ViewModels
{
    public class SignInResult(bool isValid, string message, bool clearId, bool clearPassword)
    {
        public bool IsValid { get; set; } = isValid;
        public string Message { get; set; } = message;
        public bool ClearId { get; set; } = clearId;
        public bool ClearPassword { get; set; } = clearPassword;
    }
}
