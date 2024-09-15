namespace CCG.Shared.Game.Events.Output
{
    public class CardPositionChanged : GameEventBase
    {
        public int Id { get; set; }
        public int? Position { get; set; }
    }
}