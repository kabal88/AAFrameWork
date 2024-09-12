using System;
using UnityEngine;

namespace Tweens
{
    [Serializable]
    public class AnchorMoveParams : TweenParams
    {
        public Vector2 StartPosition;
        public Vector2 TargetPosition;
    }
}