using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db;

namespace Data
{
    public static class DbContextFactory
    {
        private static IPmsContext Context { get; set;}

        public static IPmsContext Get()
        {
            var context = Context ?? new DataContext("DbPms");
            return context;
        }

        public static void Set(IPmsContext context)
        {
            if (context == null)
            {
                throw new NullReferenceException("context is null at set of DbContextFactory.");
            }
            Context = context;
        }
    }
}
