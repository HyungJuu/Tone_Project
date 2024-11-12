namespace LoginApp.Models
{
    public class SignUpAccountResult(bool isValid, string idStatus, string pwdStatus, string pwdCheckStatus)
    {
        public bool IsValid { get; set; } = isValid;
        public string IdStatus { get; set; } = idStatus;
        public string PwdStatus { get; set; } = pwdStatus;
        public string PwdCheckStatus { get; set; } = pwdCheckStatus;
    }
}
