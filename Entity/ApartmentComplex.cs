using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class ApartmentComplex
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public List<Apartment> Apartments { get; set; }
    }
}
