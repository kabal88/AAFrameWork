using UnityEngine;

namespace AAFramework.Core
{
    public abstract class AbilityDescriptionContainer<T> : AbilityDescription where T : IAbility, new()
    {
        [SerializeField] private T ability = new T();
        
        public override IAbility GetAbility => ability;
    }
}