using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Core.Models
{
    public class TB_Task
    {
        [Key]  // Marks this as the primary key
        public int Id { get; set; }  // public getter and setter

        [Required]
        [StringLength(100)]  // Sets maximum length to 100
        public string Name { get; set; }
        [StringLength(500)]  // Sets maximum length to 100
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}
