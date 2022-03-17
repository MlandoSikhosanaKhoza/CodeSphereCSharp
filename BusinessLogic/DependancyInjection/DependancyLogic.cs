using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    
    public class DependancyLogic
    {
        public IConfiguration _Configuration { get; }
        public DependancyLogic(IConfiguration Config)
        {
            _Configuration = Config;
        }
        public void InjectionServices(IServiceCollection services) {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
