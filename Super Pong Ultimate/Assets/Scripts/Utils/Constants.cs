using UnityEngine;

namespace Application.Utils
{
    public static class Constants
    {
        public static readonly int WallLayer = LayerMask.NameToLayer("Wall");
        public static readonly int PaddleLayer = LayerMask.NameToLayer("Paddle");
        public static readonly int PaddleTriggerLayer = LayerMask.NameToLayer("PaddleTrigger");
        public static readonly int BallLayer = LayerMask.NameToLayer("Ball");
    }
}