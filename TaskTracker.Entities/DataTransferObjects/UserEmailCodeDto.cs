namespace TaskTracker.Entities.DataTransferObjects
{
    public class UserEmailCodeDto : UserLogInByCodeDto
    {
        public string Code { get; set; }
    }
}
