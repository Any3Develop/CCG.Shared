using CCG.Shared.Game.Data.Enums;

namespace CCG.Shared.Abstractions.Game.Runtime.Data
{
    public interface IRuntimeObjectData : IRuntimeDataBase
    {
        string DataId { get; }
        List<string> EffectIds { get; }
        List<IRuntimeEffectData> Applied { get; }
        List<IRuntimeStatData> Stats { get; }
        ObjectState PreviousState { get; set; }
        ObjectState State { get; set; }
    }
}