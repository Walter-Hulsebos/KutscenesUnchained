using UnityEngine;
using Unity.Entities;

namespace Game.Selection
{
    public sealed class SelectableTagAuthoring : MonoBehaviour
    {
        private sealed class SelectableTagBaker : Baker<SelectableTagAuthoring>
        {
            public override void Bake(SelectableTagAuthoring authoring)
            {
                AddComponent(new SelectableTag());
            }
        }
    }
}