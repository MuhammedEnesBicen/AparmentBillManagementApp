using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public string? LicensePlate { get; set; }
    }
}
