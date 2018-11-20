using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalsService.Models;

namespace HospitalsService.Infrastructure
{
    public interface IHospitalStore
    {
        HospitalDetails GetClosestHospital(string location);
    }
}
