using System.Diagnostics.CodeAnalysis;
using Game.Controls.Selection;
using Unity.Entities;
using UnityEngine;
using static Unity.Entities.SystemAPI;

using F32   = System.Single;
using F32x2 = Unity.Mathematics.float2;

using I32   = System.Int32;

namespace Game.Controls
{
    [SuppressMessage(category: "ReSharper", checkId: "RedundantExtendsListEntry")]
    public partial class InputGathererSystem : SystemBase
    {
        private ControlMap _controls;

        protected override void OnCreate()
        {
            _controls = new ControlMap();
            _controls.Enable();
        }

        protected override void OnDestroy()
        {
            _controls.Disable();
        }

        protected override void OnUpdate()
        {
            UpdateSelectionInputs();
        }

        private void UpdateSelectionInputs()
        {
            foreach ((RefRO<SelectionInputReferences> __refs, RefRW<SelectionInputData> __data) in Query<RefRO<SelectionInputReferences>, RefRW<SelectionInputData>>())
            {
                __data.ValueRW.primaryIsPressed               = _controls.FindAction(actionNameOrId: __refs.ValueRO.primaryId.ToString()).IsPressed();
                __data.ValueRW.primaryStartedPressThisFrame   = _controls.FindAction(actionNameOrId: __refs.ValueRO.primaryId.ToString()).WasPressedThisFrame();
                __data.ValueRW.primaryStoppedPressThisFrame   = _controls.FindAction(actionNameOrId: __refs.ValueRO.primaryId.ToString()).WasReleasedThisFrame();
                
                __data.ValueRW.addModifierIsPressed           = _controls.FindAction(actionNameOrId: __refs.ValueRO.addModifierId.ToString()).IsPressed();
                __data.ValueRW.delModifierIsPressed           = _controls.FindAction(actionNameOrId: __refs.ValueRO.delModifierId.ToString()).IsPressed();
                __data.ValueRW.cursorPosition          = (F32x2)_controls.FindAction(actionNameOrId: __refs.ValueRO.cursorPositionId.ToString()).ReadValue<Vector2>();
            }
        }
    }
}