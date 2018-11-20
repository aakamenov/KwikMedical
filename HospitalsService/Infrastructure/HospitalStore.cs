using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalsService.Models;

namespace HospitalsService.Infrastructure
{
    public class HospitalStore : IHospitalStore
    {
        private static List<HospitalDetails> Hospitals { get; set; }

        static HospitalStore()
        {
            Hospitals = new List<HospitalDetails>();

            Hospitals.Add(new HospitalDetails()
            {
                Id = 1,
                Location = "135768979",
                Name = Guid.NewGuid().ToString()
            });
            Hospitals.Add(new HospitalDetails()
            {
                Id = 2,
                Location = "345123123",
                Name = Guid.NewGuid().ToString()
            });
            Hospitals.Add(new HospitalDetails()
            {
                Id = 3,
                Location = "12354765876",
                Name = Guid.NewGuid().ToString()
            });
            Hospitals.Add(new HospitalDetails()
            {
                Id = 4,
                Location = "354515765",
                Name = Guid.NewGuid().ToString()
            });
            Hospitals.Add(new HospitalDetails()
            {
                Id = 5,
                Location = "6456456546",
                Name = Guid.NewGuid().ToString()
            });
            Hospitals.Add(new HospitalDetails()
            {
                Id = 6,
                Location = "12312312",
                Name = Guid.NewGuid().ToString()
            });
            Hospitals.Add(new HospitalDetails()
            {
                Id = 7,
                Location = "34534543",
                Name = Guid.NewGuid().ToString()
            });
            Hospitals.Add(new HospitalDetails()
            {
                Id = 8,
                Location = "12321535433123",
                Name = Guid.NewGuid().ToString()
            });
            Hospitals.Add(new HospitalDetails()
            {
                Id = 9,
                Location = "123123123",
                Name = Guid.NewGuid().ToString()
            });
            Hospitals.Add(new HospitalDetails()
            {
                Id = 10,
                Location = "123124",
                Name = Guid.NewGuid().ToString()
            });
        }

        public HospitalDetails GetClosestHospital(string location)
        {
            var random = new Random().Next(Hospitals.Count - 1);

            return Hospitals[random];
        }
    }
}
