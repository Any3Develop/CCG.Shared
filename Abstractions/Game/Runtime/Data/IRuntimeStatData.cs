﻿namespace CCG_Shared.Abstractions.Game.Runtime.Data
{
    public interface IRuntimeStatData : IRuntimeDataBase
    {
        string DataId { get; }
        int RuntimeOwnerId { get; set; }
        int Max { get; set; }
        int Value { get; set; }
    }
}