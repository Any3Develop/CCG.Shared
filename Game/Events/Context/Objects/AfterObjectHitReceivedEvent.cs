using CCG.Shared.Game.Runtime.Args;

namespace CCG.Shared.Game.Events.Context.Objects
{
    public readonly struct AfterObjectHitReceivedEvent
    {
        public HitArgs Hit { get; }

        public AfterObjectHitReceivedEvent(HitArgs hit)
        {
            Hit = hit;
        }
    }
}