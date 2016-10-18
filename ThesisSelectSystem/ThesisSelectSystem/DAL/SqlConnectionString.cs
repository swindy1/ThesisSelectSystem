using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace ThesisSelectSystem.DAL
{
    public static class SqlConnectionString
    {
        public static string sqlConnectionString = ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    }
}