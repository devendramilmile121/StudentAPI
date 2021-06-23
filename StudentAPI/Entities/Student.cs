using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAPI.Entities
{
    public class Student
    {
        [Key]
        [Column("StudentId")]
        public int Id { get; set; }
        public int RoleNumber { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        public string State { get; set; }
        public int CountryId { get; set; }
    }
}
