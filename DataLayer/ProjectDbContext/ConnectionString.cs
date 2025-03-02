using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ProjectDbContext
{
    public static class ConnectionString
    {
        public static string Value { get; } = "Server=localhost\\SQLEXPRESS;Database=PetExchange;Trusted_Connection=True;TrustServerCertificate=True;";
    }
}
