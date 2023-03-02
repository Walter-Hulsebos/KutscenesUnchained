using UnityEngine;
using Unity.Entities;

namespace Game.Selection
{
    public sealed class SelectableAuthoring : MonoBehaviour
    {
        private sealed class SelectableTagBaker : Baker<SelectableAuthoring>
        {
            public override void Bake(SelectableAuthoring authoring)
            {
                AddComponent(new Selectable
                {
                    value = SelectionState.Deselected
                });
            }
        }
    }
}