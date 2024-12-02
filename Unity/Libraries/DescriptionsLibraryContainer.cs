using UnityEngine;
using UnityEngine.Serialization;

namespace Libraries
{
    [CreateAssetMenu(fileName = "DescriptionsLibraryContainer", menuName = "Descriptions/Descriptions Library")]
    public class DescriptionsLibraryContainer : ScriptableObject
    {
        [SerializeField] private DescriptionsLibrary descriptionsLibrary;

        public DescriptionsLibrary DescriptionsLibrary => descriptionsLibrary;
    }
}