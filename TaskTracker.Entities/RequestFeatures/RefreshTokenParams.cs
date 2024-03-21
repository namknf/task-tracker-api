using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.RequestFeatures
{
    public class RefreshTokenParams
    {
        [Required]
        public required string RefreshToken { get; set; }

        public uint LifeTime { get; set; } = 5;
    }
}
