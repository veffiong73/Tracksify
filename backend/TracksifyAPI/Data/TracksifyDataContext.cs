using Microsoft.EntityFrameworkCore;
using TracksifyAPI.Models;

namespace TracksifyAPI.Data
{
    public class TracksifyDataContext : DbContext
    {
        public TracksifyDataContext(DbContextOptions<TracksifyDataContext> options) : base(options)
        {
            
        }

        //Create Database tables here using DbSet

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUpdate> ProjectUpdates { get; set; }
    }
}
