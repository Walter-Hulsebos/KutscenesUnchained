using UnityEngine;
using Unity.Entities;

namespace Game.Selection
{
    public sealed class HighlightableTagAuthoring : MonoBehaviour
    {
        private sealed class HighlightableTagBaker : Baker<HighlightableTagAuthoring>
        {
            public override void Bake(HighlightableTagAuthoring authoring)
            {
                AddComponent(new HighlightableTag());
            }
        }
    }
}