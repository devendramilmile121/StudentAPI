using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAPI.Entities
{
    public class StudentResult
    {
        [Key]
        [Column("ResultId")]
        public int Id { get; set; }

        [ForeignKey("Students")]
        public int StudentId { get; set; }

        public double Percentage { get; set; }
        public virtual Student Students { get; set; }
    }
}
