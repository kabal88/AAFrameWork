using System;
using UnityEngine;

namespace Tweens
{
    [Serializable]
    public class PunchParams : TweenParams
    {
        public Vector3 Punch;
        public int Vibrato = 10;
        public float Elasticity = 1f;
    }
}