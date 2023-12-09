using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Type { get; set; }//(2+1) vs
        public int? TenantId { get; set; }
        public Tenant? Tenant { get; set; }
    }
}
