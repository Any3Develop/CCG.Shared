﻿using CCG_Shared.Abstractions.Game.Runtime.Stats;

namespace CCG_Shared.Game.Events.Context.Stats
{
    public readonly struct AfterStatChangedEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public AfterStatChangedEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}