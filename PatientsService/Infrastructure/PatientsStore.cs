using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using PatientsService.Models;

namespace PatientsService.Infrastructure
{
    public class PatientsStore : IPatientsStore
    {
        private const string DB_NAME = "Patients.db";
        private readonly object @lock = new object();

        public Task<bool> AddPatient(PatientRecord patient)
        {
            var tcs = new TaskCompletionSource<bool>();

            lock (@lock)
            {
                using (var db = new LiteDatabase(DB_NAME))
                {
                    var patients = db.GetCollection<PatientRecord>();

                    var existsQuery = Query.Contains("NHSNumber", patient.NHSNumber);
                    var exists = patients.Exists(existsQuery);

                    if (exists)
                    {
                        tcs.SetResult(false);

                        return tcs.Task;
                    }

                    patients.Insert(patient);
                }
            }

            tcs.SetResult(true);
            return tcs.Task;
        }

        public Task<PatientRecord> GetPatient(string nhsNumber)
        {
            var tcs = new TaskCompletionSource<PatientRecord>();

            lock (@lock)
            {
                using (var db = new LiteDatabase(DB_NAME))
                {
                    var patients = db.GetCollection<PatientRecord>();

                    var existsQuery = Query.Contains("NHSNumber", nhsNumber);

                    var patient = patients.FindOne(existsQuery);
                    tcs.SetResult(patient);
                }
            }

            return tcs.Task;
        }

        public async Task<bool> UpdatePatient(PatientRecord patient)
        {
            var record = await GetPatient(patient.NHSNumber);

            if(record is null)
                return false;

            lock (@lock)
            {
                using (var db = new LiteDatabase(DB_NAME))
                {
                    var patients = db.GetCollection<PatientRecord>();

                    return patients.Update(record);
                }
            }
        }
    }
}
