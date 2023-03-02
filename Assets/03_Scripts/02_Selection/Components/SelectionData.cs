using Unity.Burst;
using Unity.Entities;

using F32x2 = Unity.Mathematics.float2;

namespace Game.Selection
{
    [BurstCompile]
    public struct SelectionData : IComponentData
    {
        public F32x2 cursorPositionStart;
    }
}
