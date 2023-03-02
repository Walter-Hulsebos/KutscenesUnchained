using UnityEngine;
using UnityEngine.InputSystem;

using Unity.Entities;

namespace Game.Controls.Selection
{
    public sealed class SelectionInputAuthoring : MonoBehaviour
    {
        [SerializeField] private InputActionReference primary;
        [SerializeField] private InputActionReference addModifer;
        [SerializeField] private InputActionReference delModifer;
        [SerializeField] private InputActionReference cursorPosition;
        
        private sealed class SelectionInputBaker : Baker<SelectionInputAuthoring>
        {
            public override void Bake(SelectionInputAuthoring authoring)
            {
                AddComponent(new SelectionInputData());
                
                AddComponent(new SelectionInputReferences
                (
                    primaryId:        authoring.primary.action.id,
                    addModifiedId:    authoring.addModifer.action.id,
                    delModifiedId:    authoring.delModifer.action.id,
                    cursorPositionId: authoring.cursorPosition.action.id
                ));
            }
        }
    }
}