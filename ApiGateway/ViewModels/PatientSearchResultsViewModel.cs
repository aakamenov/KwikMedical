using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PatientsService.Models;
using Login.Models;

namespace ApiGateway.ViewModels
{
    public class PatientSearchResultsViewModel : DashboardViewModel
    {
        public IEnumerable<PatientRecord> Results { get; set; }

        public PatientSearchResultsViewModel(AuthenticatedUser user) : base(user) { }
    }
}
