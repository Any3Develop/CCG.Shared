using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Game.Context
{
    public class SharedContextInitializer : IContextInitializer
    {
        public IContext Init(IContext context, string sessionId, List<SessionPlayer> players)
        {
            InitBase(context, 
                new RuntimeContextModel {Id = sessionId, Players = players},
                new RuntimeIdModel(),
                new RuntimeOrderModel(),
                new RuntimeRandomModel(),
                context.TimerFactory.CreateModel());

            for (var i = 0; i < players.Count; i++)
            {
                var player = players[i];
                context.PlayerFactory.Create(context.PlayerFactory.CreateModel(player.Id, i));
                context.ObjectFactory.Create(context.ObjectFactory.CreateModel(player.Id, player.HeroId));

                foreach (var model in player.DeckCards.Select(id => context.ObjectFactory.CreateModel(player.Id, id)))
                    context.ObjectFactory.Create(model);
            }
            
            return context;
        }

        public IContext Restore(IContext context, IContextModel[] models)
        {
            InitBase(context, models);
            context.PlayerFactory.Restore(models.OfType<IRuntimePlayerModel>());
            context.ObjectFactory.Restore(models.OfType<IRuntimeObjectModel>());
            return context;
        }

        private static void InitBase(IContext context, params IContextModel[] models)
        {
            context.Sync(GetRequiredArgument<IRuntimeContextModel>(models));
            context.RuntimeIdProvider.Sync(GetRequiredArgument<IRuntimeIdModel>(models));
            context.RuntimeOrderProvider.Sync(GetRequiredArgument<IRuntimeOrderModel>(models));
            context.RuntimeRandomProvider.Sync(GetRequiredArgument<IRuntimeRandomModel>(models));
            context.RuntimeTimer = context.TimerFactory.Create(GetRequiredArgument<IRuntimeTimerModel>(models));
        }
        
        private static T GetRequiredArgument<T>(IEnumerable<IContextModel> args)
        {
            return (T) args.FirstOrDefault(x => x is T);
        }
    }
}