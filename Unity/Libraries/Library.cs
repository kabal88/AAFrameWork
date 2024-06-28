using System;
using System.Collections.Generic;
using System.Linq;
using Descriptions;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Libraries
{
    [Serializable]
    public partial class Library
    {
        [SerializeField] private List<Description> Descriptions = new();
        
#if UNITY_EDITOR

        /// <summary>
        /// Work ONLY from Editor. Use after adding new Description to project. 
        /// </summary>
        [Button(ButtonSizes.Large), GUIColor(0.5f, 0.8f, 1f),
         PropertyTooltip("Click after adding new Description to project.")]
        public void CollectAllDescriptions()
        {
            Descriptions = new SOProvider<Description>().GetCollection().ToList();
        }

#endif
        
    }
}