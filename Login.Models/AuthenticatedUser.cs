﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Login.Models
{
    public class AuthenticatedUser
    {
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthenticatedUser() { }

        public AuthenticatedUser(string username, string token)
        {
            Username = username;
            Token = token;
        }
    }
}
