namespace TaskTracker.Entities.DataTransferObjects
{
    public class UserForRegistrerDto : UserForManipulationDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }
    }
}