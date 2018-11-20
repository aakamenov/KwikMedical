using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalsService.Models
{
    public class HospitalDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string IpAddress { get; set; }
    }
}
