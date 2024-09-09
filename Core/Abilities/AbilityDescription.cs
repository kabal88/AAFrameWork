using AAFramework.Unity.Identifiers;
using Interfaces;
using UnityEngine;

namespace AAFramework.Core
{
    public abstract class AbilityDescription : ScriptableObject, IIdentifier
    {
        [SerializeField] private AbilitiesIdentifier identifier;
        public int Id => identifier.Id;
        public abstract IAbility GetAbility { get;}
    }
}