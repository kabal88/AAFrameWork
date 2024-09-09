using System;
using UnityEngine;

namespace AAFramework.Unity.Identifiers
{
    public interface ICollisionProvider
    {
        public event Action<Collision> CollisionEnter;
        public event Action<Collision> CollisionExit;

        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;
    }
}