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
        private Controls _controls;

        protected override void OnCreate()
        {
            _controls = new Controls();
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
                __data.ValueRW.primaryValue        = _controls.FindAction(actionNameOrId: __refs.ValueRO.primaryId.ToString()).IsPressed();
                __data.ValueRW.secondaryValue      = _controls.FindAction(actionNameOrId: __refs.ValueRO.secondaryId.ToString()).IsPressed();
                __data.ValueRW.cursorPositionValue = (F32x2)_controls.FindAction(actionNameOrId: __refs.ValueRO.cursorPositionId.ToString()).ReadValue<Vector2>();
            }
        }
    }
}