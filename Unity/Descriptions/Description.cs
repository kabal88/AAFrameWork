using AAFramework.Unity.Identifiers;
using UnityEngine;

namespace Descriptions
{
    public abstract class Description: ScriptableObject
    {
        public abstract IDescription GetDescription { get; }
    }
}