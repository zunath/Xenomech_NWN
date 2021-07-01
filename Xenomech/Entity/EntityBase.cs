using System;

namespace Xenomech.Entity
{
    public abstract class EntityBase
    {
        public Guid ID { get; set; }
        public DateTime DateCreated { get; set; }
        public abstract string KeyPrefix { get; }

        protected EntityBase()
        {
            ID = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;
        }
    }
}