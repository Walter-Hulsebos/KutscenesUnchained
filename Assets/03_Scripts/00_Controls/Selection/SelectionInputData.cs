using Unity.Entities;
using F32x2 = Unity.Mathematics.float2;
using Bool  = System.Boolean;

namespace Game.Controls.Selection
{
    public struct SelectionInputData : IComponentData
    {
        public Bool  primaryValue;
        public Bool  secondaryValue;
        public F32x2 cursorPositionValue;
    }
}