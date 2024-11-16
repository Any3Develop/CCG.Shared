using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeContextModel : IContextModel
    {
        string Id { get; }
        bool IsStarted => StartTime.HasValue;
        bool IsReady => ReadyTime.HasValue;
        bool IsEnded => EndTime.HasValue;
        DateTime? StartTime { get; set; }
        DateTime? ReadyTime { get; set; }
        DateTime? EndTime { get; set; }
        List<SessionPlayer> Players { get; set; }
    }
}