using CCG.Shared.Abstractions.Game.Runtime.Args;

namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeEffectModel : IRuntimeBaseModel
    {
        /// <summary>
        /// The unique runtime identifier of the owner of this effect.
        /// </summary>
        int RuntimeOwnerId { get; set; }
        /// <summary>
        /// An indicator of an effect, such as damage, recovery, number of repetitions, etc..
        /// </summary>
        int Value { get; set; }
        /// <summary>
        /// A measure of the life time of an effect, defined in moves. 1 turn = 1 unit.
        /// </summary>
        int Lifetime { get; set; }
        
        List<int> Targets { get; }
        List<IRuntimeEffectArg> Args { get; }
    }
}