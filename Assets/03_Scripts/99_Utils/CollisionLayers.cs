using System;

namespace Game.Utils
{
    [Flags]
    public enum CollisionLayers
    {
        Pawns     = 1 << 0,
        Selection = 1 << 1,
        Ground    = 1 << 2,
        
        All       = ~0
    }
}
