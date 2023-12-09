using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Tenant
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        [MinLength(2)]
        public string LastName { get; set; }
        [Required]
        [MinLength(11,ErrorMessage ="length of Identity number must be 11")]
        [MaxLength(11, ErrorMessage = "length of Identity number must be 11")]
        public string IdentityNumber { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string? LicensePlate { get; set; }
    }
}
