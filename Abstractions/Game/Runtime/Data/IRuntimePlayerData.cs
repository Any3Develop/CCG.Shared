namespace CCG_Shared.Abstractions.Game.Runtime.Data
{
    public interface IRuntimePlayerData : IRuntimeDataBase
    {
        string DataId { get; set; }
        bool Ready { get; set; }
        List<IRuntimeStatData> Stats { get; }
        
    }
}