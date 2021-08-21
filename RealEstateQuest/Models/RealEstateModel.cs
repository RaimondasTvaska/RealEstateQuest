﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateQuest.Models
{
    public class RealEstateModel
    {
        public ApartmentModel ApartmentAddInformation { get; set; }
        public List<ApartmentModel> Apartments { get; set; }
        public CompanyModel CompanyAddInformation { get; set; }
        public List<CompanyModel> Companies{ get; set; }
        public List<BrokerModel> Brokers { get; set; }
    }
}