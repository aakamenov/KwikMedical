﻿using Nancy.Owin;
using Owin;

namespace Login
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}
