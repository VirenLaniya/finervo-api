using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Core.Primitives
{
    public interface IDomainEvent : INotification
    {
    }
}
