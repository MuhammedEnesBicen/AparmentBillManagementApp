using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Manager
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string IdentityNumber { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public int ApartmentComplexId { get; set; }
        public ApartmentComplex ApartmentComplex { get; set; }
    }
}
