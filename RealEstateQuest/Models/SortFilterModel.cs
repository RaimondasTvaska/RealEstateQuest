using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateQuest.Models
{
    public class SortFilterModel
    {
        public string FilterCompany { get; set; }
        public string FilterBroker { get; set; }
        
        public string Sort { get; set; }
    }
}
