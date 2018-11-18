using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientsService.Models;

namespace PatientsService.Infrastructure
{
    public interface IPatientsStore
    {
        Task<PatientRecord> GetPatient(string nhsNumber);
        Task<bool> AddPatient(PatientRecord patient);
        Task<bool> UpdatePatient(PatientRecord patient);
    }
}
