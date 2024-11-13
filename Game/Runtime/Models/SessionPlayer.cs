namespace CCG.Shared.Game.Runtime.Models
{
    public class SessionPlayer
    {
        public string Id { get; set; }
        public string DeckId { get; set; }
        public string HeroId { get; set; }
        public string[] DeckCards { get; set; }
    }
}