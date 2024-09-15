using CCG.Shared.Game.Config.Enums;

namespace CCG.Shared.Game.Attributes
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