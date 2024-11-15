using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Context;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Game.Factories
{
    public class SessionFactory : ISessionFactory
    {
        private readonly IContextFactory contextFactory;
        private readonly IContextInitializer contextInitializer;

        public SessionFactory(
            IContextFactory contextFactory,
            IContextInitializer contextInitializer)
        {
            this.contextFactory = contextFactory;
            this.contextInitializer = contextInitializer;
        }
        
        public ISession Create(string id, List<SessionPlayer> players)
        {
            var context = contextFactory.CreateContext();
            contextInitializer.Init(context, id, players);
            return CreateInternal(context);
        }

        public ISession Create(IContextModel[] models)
        {
            var context = contextFactory.CreateContext();
            contextInitializer.Init(context, models);
            return CreateInternal(context);
        }

        protected virtual ISession CreateInternal(IContext context)
        {
            return new SharedSession(context);
        }
    }
}