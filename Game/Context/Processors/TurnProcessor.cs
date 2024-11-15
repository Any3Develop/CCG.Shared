using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Events.Context.Timer;
using CCG.Shared.Game.Utils.Disposables;

namespace CCG.Shared.Game.Context.Processors
{
    public class TurnProcessor
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
            ChangePlayerMoves(prevOwner, 0);
            
            HandleEndTurn(prevOwner, turnOwner);
            HandleStartTurn(turnOwner, prevOwner);
            
            ChangePlayerMoves(turnOwner, 0);
        }

        private void HandleEndTurn(string turnOwner, string prevOwner)
        {
            
        }

        private void HandleStartTurn(string turnOwner, string prevOwner)
        {
            
        }

        private void ChangePlayerMoves(string playerId, int value)
        {
            var tableEntities = context.ObjectsCollection.GetAll<IRuntimeObject>(ObjectState.InTable, playerId);
            foreach (var runtimeObject in tableEntities)
            {
                var stat = runtimeObject.StatsCollection.Get(StatType.Move);
                stat.SetValue(value);
            }
        }
    }
}