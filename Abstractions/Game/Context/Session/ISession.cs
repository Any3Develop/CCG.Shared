﻿using CCG.Shared.Game.Context.Session;

namespace CCG.Shared.Abstractions.Game.Context.Session
{
    public interface ISession
    {
        SessionPlayerModel[] Players { get; }
        IContext Context { get; }
        string Id { get; }
        void Build(params object[] args);
    }
}