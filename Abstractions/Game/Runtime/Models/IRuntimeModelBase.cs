namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeModelBase : IContextModel
    {
        /// <summary>
        /// The unique identifier of the runtime entity.
        /// </summary>
        int Id { get; }
        /// <summary>
        /// The identifier of the shared configuration of the entity.
        /// </summary>
        string ConfigId { get; }
        /// <summary>
        /// Determines which player owns this runtime entity.
        /// </summary>
        string OwnerId { get; }
    }
}