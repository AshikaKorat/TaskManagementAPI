using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Core.Models
{
    public class TB_Task
    {
        [Key]  // Marks this as the primary key
        public int Id { get; set; }  // public getter and setter

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}
