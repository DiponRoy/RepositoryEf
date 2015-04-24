using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db
{
    public class PmsContextConfiguration : IPmsContextConfiguration
    {
        protected readonly DbContextConfiguration Configuration;
        public PmsContextConfiguration(DbContextConfiguration configuration)
        {
            Configuration = configuration;
        }

        public PmsContextConfiguration()
        {
            
        }

        public bool LazyLoadingEnabled
        {
            get
            {
                if (Configuration != null)
                {
                    return Configuration.LazyLoadingEnabled;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (Configuration != null)
                {
                    Configuration.LazyLoadingEnabled = value;
                }
            } 
        }
    }
}
