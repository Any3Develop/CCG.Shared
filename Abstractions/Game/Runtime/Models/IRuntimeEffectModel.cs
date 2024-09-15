namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeEffectModel : IRuntimeModelBase
    {
        int EffectOwnerId { get; set; }
        int Value { get; set; }
        int Lifetime { get; set; }
    }
}