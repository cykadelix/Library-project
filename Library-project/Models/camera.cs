using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class camera
    {
        [Key]
        [Required]
        public int serialnumber { get; set; }

        [Required(ErrorMessage = "Please enter the brand!")]
        public string? brand { get; set; }
        public string? description { get; set; }
        [Required(ErrorMessage = "Please enter the lumens!")]
        public int? lumens { get; set; }
        [Required(ErrorMessage = "Please enter the availability!")]
        public bool? availability { get; set; }

        [ForeignKey("media")]
        public int mediaid { get; set; }
        public int media { get; set; }

    }
}
