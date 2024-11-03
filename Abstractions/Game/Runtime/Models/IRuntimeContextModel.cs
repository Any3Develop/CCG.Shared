namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeContextModel : IContextModel
    {
        public string Id { get; }
        public bool IsStarted => StartTime.HasValue;
        public bool IsReady => ReadyTime.HasValue;
        public bool IsEnded => EndTime.HasValue;
        public DateTime? StartTime { get; set; }
        public DateTime? ReadyTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}