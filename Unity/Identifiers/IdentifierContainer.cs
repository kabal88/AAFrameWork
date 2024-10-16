using System;
using Helpers;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace AAFramework.Unity.Identifiers
{
    [CreateAssetMenu(fileName = "identifier", menuName = "Identifiers/Identifier")]
    public class IdentifierContainer : ScriptableObject, IIdentifier
    {
        [SerializeField, ReadOnly] private int id;

        public static bool operator ==(IdentifierContainer lhs, IdentifierContainer rhs) => lhs.EqualsIdentifiers(rhs);
        public static bool operator !=(IdentifierContainer lhs, IdentifierContainer rhs) => !lhs.EqualsIdentifiers(rhs);
        
        public static implicit operator int(IdentifierContainer lhs) => lhs.Id;
        
        public int Id
        {
            get
            {
                if (id == 0)
                    id = IndexGenerator.GenerateIndex(name);
                return id;
            }
        }
        
        public override bool Equals(object obj)
        {
            return obj is IdentifierContainer container &&
                   Id == container.Id;
        }

        public bool Equals(IdentifierContainer other)
        {
            if (other == null)
                return false;

            return other.Id == Id;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Id);
        }
        
        private void OnValidate()
        {
            id = IndexGenerator.GenerateIndex(name);
        }

        [Button]
        private void GenerateID()
        {
            id = IndexGenerator.GenerateIndex(name);
        }
    }
}