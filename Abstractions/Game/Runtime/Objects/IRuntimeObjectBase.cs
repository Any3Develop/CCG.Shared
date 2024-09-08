﻿using System;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Abstractions.Game.Runtime.Objects
{
    public interface IRuntimeObjectBase : IDisposable
    {
        IRuntimeDataBase RuntimeData { get; }
        IEventsSource EventsSource { get; }
        IEventPublisher EventPublisher { get; }
    }
}