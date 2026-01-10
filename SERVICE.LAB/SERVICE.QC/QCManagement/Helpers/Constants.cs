using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QC.Helpers
{
    public static class Constants
    {
        private static readonly string connectionUrl = "WebApiDatabase";
        public static string ConnectionUrl { get => connectionUrl; }
    }
}