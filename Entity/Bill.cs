using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Bill
    {
        public int Id { get; set; }
        public float BillCost { get; set; }
        public string Explanation { get; set; }
        public bool IsPayed { get; set; }
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
    }
}
