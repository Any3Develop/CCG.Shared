using CCG.Shared.Game.Enums;

namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeObjectModel : IRuntimeBaseModel
    {
        List<string> EffectIds { get; }
        List<IRuntimeEffectModel> Applied { get; }
        List<IRuntimeStatModel> Stats { get; }
        ObjectState PreviousState { get; set; }
        ObjectState State { get; set; }
    }
}