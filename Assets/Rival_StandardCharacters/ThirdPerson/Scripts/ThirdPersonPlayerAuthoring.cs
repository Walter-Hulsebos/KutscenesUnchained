using UnityEngine;
using Unity.Entities;

[DisallowMultipleComponent]
public sealed class ThirdPersonPlayerAuthoring : MonoBehaviour
{
    [SerializeField] private GameObject ControlledCharacter;
    [SerializeField] private GameObject ControlledCamera;

    private class Baker : Baker<ThirdPersonPlayerAuthoring>
    {
        public override void Bake(ThirdPersonPlayerAuthoring authoring)
        {
            AddComponent(new ThirdPersonPlayer
            {
                ControlledCharacter = GetEntity(authoring.ControlledCharacter),
                ControlledCamera = GetEntity(authoring.ControlledCamera),
            });
            AddComponent(new ThirdPersonPlayerInputs());
        }
    }
}