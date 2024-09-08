using CCG_Shared.Game.Data.Enums;

namespace CCG_Shared.Game.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class EffectVisualAttribute : Attribute
    {
        public VisualId Value { get; }

        public EffectVisualAttribute(VisualId value)
        {
            Value = value;
        }
    }
}