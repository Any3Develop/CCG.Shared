using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class EffectVisualAttribute : Attribute
    {
        public EffectVisualId Value { get; }

        public EffectVisualAttribute(EffectVisualId value)
        {
            Value = value;
        }
    }
}