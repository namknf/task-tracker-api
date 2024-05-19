namespace TaskTracker.Entities.DataTransferObjects
{
    public class UserForPasswordUpdateDto
    {
        public string NewPassword { get; set; }

        public string ConfirmationCode { get; set; }
    }
}
