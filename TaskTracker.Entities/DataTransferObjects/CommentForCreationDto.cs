﻿namespace TaskTracker.Entities.DataTransferObjects
{
    public class CommentForCreationDto
    {
        public string CommentText { get; set; }

        public DateTime CreationDate { get => DateTime.Now; }
    }
}
