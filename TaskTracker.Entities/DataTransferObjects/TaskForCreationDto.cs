﻿namespace TaskTracker.Entities.DataTransferObjects
{
    public class TaskForCreationDto : TaskForManipulationDto
    {
        public DateTime CreationDate { get => DateTime.Now; }

        public Guid TaskStatusId { get => new ("DD10800A-4924-4EB4-B95B-04F694DAF9AA"); }

        public List<ParticipantDto> Participants { get; set; }
    }
}
