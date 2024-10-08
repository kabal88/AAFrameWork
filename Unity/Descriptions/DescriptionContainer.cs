﻿using AAFramework.Unity.Identifiers;
using Sirenix.OdinInspector;
using UnityEngine;
using Descriptions;


namespace DescriptionContainers
{
    public class DescriptionContainer<T> : Descriptions.Description where T : IDescription, new()
    {
        [SerializeField, HideLabel] protected T description;

        public override IDescription GetDescription => description;
    }
}