namespace CCG.Shared.Api.Lobby
{
    public class DuelModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string HostId { get; set; }
        public DateTime? Closed { get; set; }
        public List<LobbyPlayerModel> Players { get; set; } = new();
    }
}