using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class EmployeeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Designation { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public int EmployeeAge { get; set; }

        [Required]
        //[MaxLength(10)]
        public Int64 EmployeePhone { get; set; }

    }
}
