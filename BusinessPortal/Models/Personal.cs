using System.ComponentModel.DataAnnotations;

namespace BusinessPortal.Models
{
    public class Personal
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        
    }
}
