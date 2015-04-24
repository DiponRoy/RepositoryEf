using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Db
{
    public class PmsDbEntityEntry<T> : IPmsDbEntityEntry<T> where T : class
    {
        protected readonly DbEntityEntry<T> Entry;

        public PmsDbEntityEntry(DbEntityEntry<T> entry)
        {
            Entry = entry;
        }

        public PmsDbEntityEntry(EntityState state)
        {
            _state = state;
        }

        private EntityState _state;

        public EntityState State
        {
            get
            {
                if (Entry != null)
                {
                    return Entry.State;
                }
                else
                {
                    return _state;
                }
            }
            set
            {
                if (Entry != null)
                {
                    Entry.State = value;
                }
                else
                {
                    _state = value;
                }
            }
        }
    }
}
