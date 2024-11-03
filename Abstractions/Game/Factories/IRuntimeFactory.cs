namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface IRuntimeFactory<out TRuntime, TRuntimeModel>
    {
        TRuntimeModel CreateModel(int? runtimeId, string ownerId, string dataId, bool notify = true);
        TRuntime Create(TRuntimeModel runtimeData, bool notify = true);
    }
}