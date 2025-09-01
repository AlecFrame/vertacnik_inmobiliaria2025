using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vertacnik_inmobiliaria2025.Models
{
    public class RepositorioBase
    {
        protected readonly IConfiguration _config;
        protected readonly string connectionString;

        public RepositorioBase(IConfiguration configuration)
        {
            _config = configuration;
            connectionString = _config["ConnectionStrings:DefaultConnection"];
        }
    }
}