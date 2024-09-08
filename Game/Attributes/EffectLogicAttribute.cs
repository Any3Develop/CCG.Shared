using CCG_Shared.Game.Data.Enums;

namespace CCG_Shared.Game.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class EffectLogicAttribute : Attribute
    {
        public LogicId Value { get; }

        public EffectLogicAttribute(LogicId value)
        {
            Value = value;
        }
    }
}