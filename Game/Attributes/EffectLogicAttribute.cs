using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class EffectLogicAttribute : Attribute
    {
        public EffectLogicId Value { get; }

        public EffectLogicAttribute(EffectLogicId value)
        {
            Value = value;
        }
    }
}