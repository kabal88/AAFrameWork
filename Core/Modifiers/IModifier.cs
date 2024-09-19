using System;

namespace AAFramework.Core
{
    public interface IModifier
    {
        /// <summary>
        /// we should take it from identifier
        /// </summary>
        int ModifierID { get; set; }
        
        /// <summary>
        /// we can separate instances of modifiers by guid
        /// </summary>
        Guid ModifierGuid { get; }
        ModifierCalculationType GetCalculationType { get; }
        ModifierValueType GetModifierType { get; }
    }

    public interface IModifier<T> : IModifier where T : struct
    {
        T GetValue { get; }
        void Modify(ref T currentMod);
    }

    public abstract class BaseModifier<T> : IModifier<T> where T : struct
    {
        public abstract int ModifierID { get; set; }
        public abstract Guid ModifierGuid { get; set; }
        public abstract ModifierCalculationType GetCalculationType { get; set; }
        public abstract ModifierValueType GetModifierType { get; set; }
        public abstract T GetValue { get; set; }
        public abstract void Modify(ref T currentMod);
        
        public override bool Equals(object obj)
        {
            return obj is BaseModifier<T> modifier &&
                   ModifierGuid.Equals(modifier.ModifierGuid);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(ModifierGuid);
        }
    }
}