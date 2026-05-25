using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Primitives
{
    public abstract class Entity
    {
        protected Entity(Guid id) => Id = id;

        public Guid Id { get; private set; }
    }
}
