namespace CCG.Shared.Game.Config
{
    public class TableConfig
    {
        public int MaxInTableCount { get; private set; }
        public int FirstPlayerTurnMana { get; private set; } = 0;
        public int OtherPlayersTurnMana { get; private set; } = 1;
        public int MaxPlayers { get; private set; } = 2;
    }
}