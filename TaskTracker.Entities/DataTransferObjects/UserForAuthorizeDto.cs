namespace TaskTracker.Entities.DataTransferObjects
{
    public class UserForAuthorizeDto : UserForManipulationDto
    {
        public string? EmailOrUserName { get; set; }
    }
}
