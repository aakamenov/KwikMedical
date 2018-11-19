using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login.Models;
using PatientsService.Models;

namespace ApiGateway.ViewModels
{
    public class SubmitEmergencyViewModel : DashboardViewModel
    {
        public PatientRecord PatientRecord { get; set; }

        public SubmitEmergencyViewModel(AuthenticatedUser user, PatientRecord record) : base(user)
        {
            PatientRecord = record;
        }
    }
}
