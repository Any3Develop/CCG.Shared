namespace CCG.Shared.Game.Context.Session
{
    public class SessionPlayerModel
    {
        public string Id { get; set; }
        public string DeckId { get; set; }
        public string HeroId { get; set; } // TODO
        public string[] DeckCards { get; set; }
    }
}