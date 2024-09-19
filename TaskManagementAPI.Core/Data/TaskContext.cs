using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Core.Models;

namespace TaskManagementAPI.Core.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        public DbSet<TB_Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TB_Task>().HasKey(t => t.Id);  // Ensure a primary key is defined for Task
        }
    }
}
