﻿using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Events.Output
{
    public class EffectAdded : GameEvent
    {
        public IRuntimeEffectModel RuntimeModel { get; set; }
    }
}