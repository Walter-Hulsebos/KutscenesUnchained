using Unity.Entities;
using F32x2 = Unity.Mathematics.float2;
using Bool  = System.Boolean;

namespace Game.Controls.Selection
{
    public struct SelectionInputData : IComponentData
    {
        public Bool  primaryIsPressed;
        public Bool  primaryStartedPressThisFrame;
        public Bool  primaryStoppedPressThisFrame;
        
        public Bool  addModifierIsPressed;
        public Bool  delModifierIsPressed;
        
        public F32x2 cursorPosition;
    }
}