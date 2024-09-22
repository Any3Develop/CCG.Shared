namespace CCG.Shared.Api.Lobby
{
    public class SessionModel
    {
        public string Id { get; set; }
        public List<LobbyPlayerModel> Players { get; set; }
    }
}