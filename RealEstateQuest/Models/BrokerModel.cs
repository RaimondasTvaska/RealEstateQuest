using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateQuest.Models
{
    public class BrokerModel
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Surname { get; set; }
        public string FullName
        {
            get
            {
                return First_Name + " " + Surname;
            }
        }
        public string CompanyName { get; set; }
    }
}
