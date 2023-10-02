namespace TaskTracker.Entities.DataTransferObjects
{
    public class UserForRegistrerDto : UserForManipulationDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}