using CCG.Shared.Abstractions.Game.Context;

namespace CCG.Shared.Game.Context
{
    public class SharedSession : ISession
    {
        public string Id => Context?.RuntimeData?.Id;
        public IContext Context { get; }
        
        public SharedSession(IContext context)
        {
            Context = context;
        }

        public void Start()
        {
            Context.Start();
            Context.TurnProcessor.Start();
            Context.GameQueueCollector.Start();
            Context.ContextEventProcessor.Start();
            Context.WinConditionProcessor.Start();
            // TODO setup game and wait for an action from players
            // TODO Callbacks
        }

        public void Ready()
        {
            Context.Ready();
            // TODO make an action for start game
            // TODO Callbacks
        }

        public void End()
        {
            Context.End();
            Context.TurnProcessor.End();
            Context.GameQueueCollector.End();
            Context.ContextEventProcessor.End();
            Context.WinConditionProcessor.End();
            // TODO make an action for end the game, block all actions
            // TODO Callbacks
        }
    }
}