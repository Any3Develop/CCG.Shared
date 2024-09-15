using CCG.Shared.Game.Config.Enums;

namespace CCG.Shared.Game.Attributes
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