﻿using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.SessionState;
using FubuMVC.StructureMap.Bootstrap;

namespace Kokugen.Web
{
    public class Global : KokugenFubuStructureMapApplication
    {

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}