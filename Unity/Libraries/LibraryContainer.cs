using UnityEngine;
using UnityEngine.Serialization;

namespace Libraries
{
    [CreateAssetMenu(fileName = "LibraryContainer", menuName = "Descriptions/Library")]
    public class LibraryContainer : ScriptableObject
    {
        [FormerlySerializedAs("library")] [SerializeField] private DescriptionsLibrary descriptionsLibrary;

        public DescriptionsLibrary DescriptionsLibrary => descriptionsLibrary;
    }
}