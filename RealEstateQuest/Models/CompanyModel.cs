using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateQuest.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseFlatNumber { get; set; }
        List<int> Brokers { get; set; }
    }
}
