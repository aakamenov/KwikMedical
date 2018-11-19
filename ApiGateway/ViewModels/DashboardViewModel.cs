using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login.Models;

namespace ApiGateway.ViewModels
{
    public class DashboardViewModel
    {
        public AuthenticatedUser User { get; set; }

        public DashboardViewModel(AuthenticatedUser user)
        {
            User = user;
        }
    }
}
