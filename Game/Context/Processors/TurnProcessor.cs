using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.Processors;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Common.Utils;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Events.Context.Timer;

namespace CCG.Shared.Game.Context.Processors
{
    public class TurnProcessor : ITurnProcessor
    {
        private IContext context;
        private IDisposables disposables;

        public TurnProcessor(IContext context)
        {
            this.context = context;
        }
        
        public void Start()
        {
            context.EventSource
                .Subscribe<TimerTurnChangedEvent>(data => HandleTurn(data.OwnerId, data.PrevOwnerId))
                .AddTo(ref disposables);
        }

        public void End()
        {
            disposables?.Dispose();
            disposables = null;
            context = null;
        }

        private void HandleTurn(string turnOwner, string prevOwner)
        {
            ChangePlayerMoves(prevOwner, false);
            
            HandleEndTurn(prevOwner, turnOwner);
            HandleStartTurn(turnOwner, prevOwner);

            ChangePlayerMana(turnOwner);
            ChangePlayerMoves(turnOwner, true);
        }

        private void HandleEndTurn(string turnOwner, string prevOwner)
        {
            
        }

        private void HandleStartTurn(string turnOwner, string prevOwner)
        {
            
        }

        private void ChangePlayerMana(string playerId)
        {
            var config = context.Config.Table;
            var timer = context.RuntimeTimer.RuntimeModel;
            var player = context.PlayersCollection.Get(playerId);
            var manaByFirstTurn = player.RuntimeModel.IsFirst 
                ? config.FirstPlayerTurnMana 
                : config.OtherPlayersTurnMana;
            
            var value = timer.Round + manaByFirstTurn;
            
            player.StatsCollection.Get(StatType.Mana).Override(value, value);
        }

        private void ChangePlayerMoves(string playerId, bool enable)
        {
            var entitiesStats = context.ObjectsCollection
                .GetAll<IRuntimeObject>(ObjectState.Table, playerId, asQuery: true)
                .Select(x => x.StatsCollection.Get(StatType.Move))
                .Where(x => x != null);
            
            foreach (var stat in entitiesStats)
            {
                if (enable)
                {
                    stat.RiseToMax();
                    continue;
                }
                
                stat.SetValue(0);
            }
        }
    }
}