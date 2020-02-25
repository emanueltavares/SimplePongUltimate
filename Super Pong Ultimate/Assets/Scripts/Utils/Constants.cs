using UnityEngine;

namespace Application.Utils
{
    public static class Constants
    {
        public static readonly int WallLayer = LayerMask.NameToLayer("Wall");
        public static readonly int PlayerLayer = LayerMask.NameToLayer("Player");
        public static readonly int BallLayer = LayerMask.NameToLayer("Ball");
    }
}