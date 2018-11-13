using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KwikMedical.Shared.Models.Responses;

namespace Login.Models.Responses
{
    public class AuthResponse : Response
    {
        public bool Success { get; set; }
    }
}
