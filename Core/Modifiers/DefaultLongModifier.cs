using System;

namespace AAFramework.Core
{
    [Serializable]
    public sealed class DefaultLongModifier : BaseModifier<long>
    {
        public override long GetValue { get; set; }
        public override ModifierCalculationType GetCalculationType { get; set; }
        public override ModifierValueType GetModifierType { get; set; }
        public override Guid ModifierGuid { get; set; }
        public override int ModifierID { get; set; }

        public override void Modify(ref long currentMod)
        {
            currentMod = ModifiersCalculation.GetResult(currentMod, GetValue, GetCalculationType, GetModifierType);
        }
    }
}