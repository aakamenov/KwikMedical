using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientsService.Models
{
    public class PatientRecord
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NHSNumber { get; set; }
        public string Address { get; set; }
        public string MedicalDescription { get; set; }
    }
}
