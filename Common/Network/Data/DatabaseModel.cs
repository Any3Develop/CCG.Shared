﻿using CCG.Shared.Game.Data;

namespace CCG.Shared.Common.Network.Data
{
    public class DatabaseModel
    {
        public ObjectData[] Objects { get; set; }
        public EffectData[] Effects { get; set; }
        public StatData[] Stats { get; set; }
        public PlayerData[] Players { get; set; }
    }
}