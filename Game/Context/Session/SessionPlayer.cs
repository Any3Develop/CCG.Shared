namespace CCG.Shared.Game.Context.Session
{
    public class SessionPlayer
    {
        public string Id { get; set; }
        public string DeckId { get; set; }
        public string HeroId { get; set; }
        public string[] DeckCards { get; set; }
    }
}