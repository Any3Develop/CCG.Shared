using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.Processors;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Common.Utils;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Events.Context.Objects;
using CCG.Shared.Game.Events.Context.Players;

namespace CCG.Shared.Game.Context.Processors
{
    public class WinConditionProcessor : IWinConditionProcessor
    {
        private IContext context;
        private IDisposables disposables;
        
        public WinConditionProcessor(IContext context)
        {
            this.context = context;
        }

        public void Start()
        {
            if (disposables != null || context == null)
                return;
            
            context.EventSource
                .Subscribe<AfterObjectChangedEvent>(data => OnObjectChanged(data.RuntimeObject))
                .AddTo(ref disposables);
        }

        public void End()
        {
            context = null;
            disposables?.Dispose();
            disposables = null;
        }

        private void OnObjectChanged(IRuntimeObject runtimeObject)
        {
            if (context?.RuntimeData is null or {IsEnded : true} || runtimeObject.IsAlive)
                return;

            var ownerId = runtimeObject.RuntimeModel.OwnerId;
            if (runtimeObject.Config.Type == ObjectType.Hero)
            {
                GameEnd(ownerId);
                return;
            }

            var aliveLeft = context.ObjectsCollection.Count(x => x.RuntimeModel.OwnerId == ownerId && x.IsAlive);
            if (aliveLeft <= 0)
                GameEnd(ownerId);
        }

        private void GameEnd(string looserId)
        {
            context.EventPublisher.Publish(new AfterPlayerLostEvent(looserId));
        }
    }
}