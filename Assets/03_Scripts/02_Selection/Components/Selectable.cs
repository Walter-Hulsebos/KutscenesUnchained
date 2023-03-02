using Unity.Entities;

namespace Game.Selection
{
    public struct Selectable : IComponentData
    {
        public SelectionState value;
    }
}