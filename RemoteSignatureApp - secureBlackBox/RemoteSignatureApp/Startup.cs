﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RemoteSignatureApp.Startup))]
namespace RemoteSignatureApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
