using Microsoft.EntityFrameworkCore;

namespace TracksifyAPI.Data
{
    public class TracksifyDataContext : DbContext
    {
        public TracksifyDataContext(DbContextOptions<TracksifyDataContext> options) : base(options)
        {
            
        }

        //Create Database tables here using DbSet
    }
}
