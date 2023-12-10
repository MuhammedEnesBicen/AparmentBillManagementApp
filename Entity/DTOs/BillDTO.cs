using System.ComponentModel.DataAnnotations;

namespace Entity.DTOs
{
    public class BillDTO
    {
        public int Id { get; set; }
        [Required]
        public float BillCost { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "You should enter at least 5 characters")]
        public string Explanation { get; set; }
        [Required]
        public bool IsPayed { get; set; }
        [Required]
        public DateTime BillDate { get; set; }
        [Required]
        public int ApartmentId { get; set; }
    }
}
