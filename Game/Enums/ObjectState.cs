﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CCG.Shared.Game.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ObjectState
    {
        Created = 0,
        InDeck,
        InHand,
        InTable
    }
}