using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Manager
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Identity Number")]
        public string IdentityNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public int ApartmentComplexId { get; set; }
        public ApartmentComplex ApartmentComplex { get; set; }
    }
}
