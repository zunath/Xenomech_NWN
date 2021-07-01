using System.Collections.Generic;

namespace Xenomech.Entity
{
    public class EntityList<T> : List<T>
        where T : EntityBase
    {
    }
}