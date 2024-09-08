namespace CCG_Shared.Common.Network.Exceptions
{
    public class NotAuthrorizedException : Exception
    {
        public NotAuthrorizedException(string message = null) : base(message ?? "Not authorized."){}
    }
}