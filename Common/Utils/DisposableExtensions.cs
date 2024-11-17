namespace CCG.Shared.Common.Utils
{
    public interface IDisposables : IDisposable
    {
        IDisposables Add(IDisposable disposable);
    }

    public class Disposables : IDisposables
    {
        private readonly List<IDisposable> disposables = new();

        public IDisposables Add(IDisposable disposable)
        {
            disposables.Add(disposable);
            return this;
        }

        public void Dispose()
        {
            var mem = disposables.ToArray();
            disposables.Clear();
            foreach (var disposable in mem)
                disposable?.Dispose();
        }
    }
    
    public static class DisposableExtensions
    {
        public static IDisposables CreateDisposables()
        {
            return new Disposables();
        }

        public static T AddTo<T>(this T disposable, ref IDisposables disposables) where T : IDisposable
        {
            disposables ??= CreateDisposables();
            disposables.Add(disposable);
            return disposable;
        }

        public static T AddTo<T>(this T disposable, IDisposables disposables) where T : IDisposable
        {
            if (disposable != null && disposables != null)
                disposables.Add(disposable);

            return disposable;
        }

        public static IDisposables AsDisposables<T>(this T disposable) where T : IDisposable
        {
            if (disposable is IDisposables disposables)
                return disposables;

            return CreateDisposables().Add(disposable);
        }
    }
}