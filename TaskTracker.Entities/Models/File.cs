using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Entities.Models
{
    public class File : BaseModel
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public byte[] Data { get; set; }
    }
}
