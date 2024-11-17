using Newtonsoft.Json;

namespace CCG.Shared.Common.Utils
{
    public static class StringExtensions
    {
        public static string AsJsonFormat(this object source, Formatting formating = Formatting.Indented)
        {
            return source switch
            {
                null => $"StringExtensions.{nameof(AsJsonFormat)} : {nameof(NullReferenceException)}",
                string strObj => strObj,
                _ => JsonConvert.SerializeObject(source, formating)
            };
        }
    }
}