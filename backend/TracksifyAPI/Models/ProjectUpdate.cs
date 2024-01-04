using Microsoft.EntityFrameworkCore;

namespace TracksifyAPI.Models
{
    public class ProjectUpdate
    {
        public Guid ProjectUpdateId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime CheckOut { get; set; }
        public string? WorkDone { get; set; }
        public  Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Project? Project { get; set; }

    }
}
