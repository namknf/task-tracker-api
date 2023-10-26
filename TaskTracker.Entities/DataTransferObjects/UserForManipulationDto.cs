namespace TaskTracker.Entities.DataTransferObjects
{
    public abstract class UserForManipulationDto
    {
        public string? Password { get; set; }

        public string? Email { get; set; }
    }
}
