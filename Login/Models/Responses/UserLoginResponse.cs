using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KwikMedical.Shared.Models.Responses;

namespace Login.Models.Responses
{
    public class UserLoginResponse : Response
    {
        public bool Success { get; set; }
        public AuthenticatedUser User { get; set; }
    }
}
