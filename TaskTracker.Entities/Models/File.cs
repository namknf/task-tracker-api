﻿namespace TaskTracker.Entities.Models
{
    public class File : BaseModel
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public byte[] Data { get; set; }
    }
}
