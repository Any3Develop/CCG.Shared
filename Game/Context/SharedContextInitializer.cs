using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Game.Context
{
    public class SharedContextInitializer : IContextInitializer
    {
        public IContext Init(IContext context, string id, List<SessionPlayer> players)
        {
            var firstPlayerIndex = new Random().Next(0, players.Count);
            var firstPlayer = players[firstPlayerIndex];
            firstPlayer.IsFirst = true;
            players.RemoveAt(firstPlayerIndex);
            players.Insert(0, firstPlayer);
            
            var models = new List<IContextModel>
            {
                new RuntimeContextModel {Id = id, Players = players},
                new RuntimeIdModel(),
                new RuntimeOrderModel(),
                new RuntimeRandomModel(),
                context.TimerFactory.CreateModel(),
            };
            models.AddRange(players.Select((p, i) => context.PlayerFactory.CreateModel(p.Id, i)));
            return Init(context, models);
        }

        public IContext Init(IContext context, IReadOnlyCollection<IContextModel> models)
        {
            context.Sync(GetRequiredArgument<IRuntimeContextModel>(models));
            context.RuntimeIdProvider.Sync(GetRequiredArgument<IRuntimeIdModel>(models));
            context.RuntimeOrderProvider.Sync(GetRequiredArgument<IRuntimeOrderModel>(models));
            context.RuntimeRandomProvider.Sync(GetRequiredArgument<IRuntimeRandomModel>(models));
            context.RuntimeTimer = context.TimerFactory.Create(GetRequiredArgument<IRuntimeTimerModel>(models));

            foreach (var model in models.OfType<IRuntimePlayerModel>())
                context.PlayerFactory.Create(model, false);

            foreach (var model in models.OfType<IRuntimeObjectModel>())
                context.ObjectFactory.Create(model, false);

            return context;
        }
        
        private static T GetRequiredArgument<T>(params object[] args)
        {
            return (T) args.FirstOrDefault(x => x is T);
        }
    }
}