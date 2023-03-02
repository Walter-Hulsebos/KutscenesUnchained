using Unity.Entities;
using UnityEngine;

namespace Game.Selection
{
    public sealed class SelectionDataAuthoring : MonoBehaviour
    {
        private sealed class SelectionDataBaker : Baker<SelectionDataAuthoring>
        {
            public override void Bake(SelectionDataAuthoring authoring)
            {
                AddComponent(new SelectionData());
            }
        }
    }
}