using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Entities.RequestFeatures
{
    public class RefreshTokenParams
    {
        public string RefreshToken { get; set; }

        public uint LifeTime { get; set; } = 5;
    }
}
