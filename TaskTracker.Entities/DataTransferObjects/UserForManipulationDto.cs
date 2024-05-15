namespace TaskTracker.Entities.DataTransferObjects
{
    public abstract class UserForManipulationDto
    {
        public string? Password { get; set; }

        public string? EmailOrUserName { get; set; }
    }
}
