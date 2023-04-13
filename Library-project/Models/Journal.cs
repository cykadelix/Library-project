using Library_project.Data.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_project.Models
{
    public class journal
    {
        
        [ForeignKey("media")]
        public int mediaid { get; set; }
        public media media { get; set; }
        

        [Key]
        public int jouranalid { get; set; }
        public string title { get; set; }
        
        

        [NotMapped]
        public Location? location { get; set; }

        
        public string researchers { get; set; }
        public string subject { get; set; }
        
        public int length { get; set; }
        public DateOnly releasedate { get; set; }
        public bool isavailable { get; set; }
        


    }
}
