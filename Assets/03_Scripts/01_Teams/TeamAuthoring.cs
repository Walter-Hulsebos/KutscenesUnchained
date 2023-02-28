using UnityEngine;

using Unity.Entities;

using U08 = System.Byte;

namespace Game.Teams
{
    public sealed class TeamAuthoring : MonoBehaviour
    {
        [SerializeField] private U08 value;
        
        private sealed class TeamBaker : Baker<TeamAuthoring>
        {
            public override void Bake(TeamAuthoring authoring)
            {
                AddComponent(new Team {value = authoring.value});
            }
        }
    }
}