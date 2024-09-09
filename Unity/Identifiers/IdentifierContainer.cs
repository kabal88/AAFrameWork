using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AAFramework.Unity.Identifiers
{
    [CreateAssetMenu(fileName = "identifier", menuName = "Identifiers/Identifier")]
    public class IdentifierContainer : ScriptableObject, IIdentifier
    {
        [SerializeField, ReadOnly] private int _id;

        public int Id
        {
            get
            {
                if (_id == 0)
                    _id = IndexGenerator.GenerateIndex(name);
                return _id;
            }
        }
        
        private void OnValidate()
        {
            _id = IndexGenerator.GenerateIndex(name);
        }

        [Button]
        private void GenerateID()
        {
            _id = IndexGenerator.GenerateIndex(name);
        }
    }
}