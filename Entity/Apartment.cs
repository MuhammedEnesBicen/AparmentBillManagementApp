using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Apartment
    {
        public int Id { get; set; }

        [Range(0, 20,ErrorMessage ="You should enter a number between 0 - 20")]        
        public int Floor { get; set; }
        
        [Range(0, 100)]
        public int Number { get; set; }

        [Required]
        [MinLength(1)]
        [StringLength(10)]
        public string BlockName { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; }//like (2+1) or (3+1)
    }
}
