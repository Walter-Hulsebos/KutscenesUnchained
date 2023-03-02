using Unity.Entities;
using static Unity.Entities.SystemAPI;
using Unity.Mathematics;
using static Unity.Mathematics.math;

using Drawing;

using Unity.Burst;
using Unity.Transforms;
using UnityEngine;
using F32   = System.Single;
using F32x2 = Unity.Mathematics.float2;
using F32x3 = Unity.Mathematics.float3;
using F32x4 = Unity.Mathematics.float4;
using Bool  = System.Boolean;

namespace Game.Selection
{
    using Utils;
    using Teams;
    using Controls.Selection;

    //TODO: Select single pawn
    //TODO: Select multiple pawns
    //TODO: Select add modifier.
    //TODO: Select del modifier.
    //TODO: Selection saving
    //TODO: Selection heuristics
    
    [BurstCompile]
    public partial struct SelectionSystem : ISystem
    {
        //private BuildPhysicsWorld _buildPhysicsWorld;
        private CommandBuilder _debugBuilder;

        //NOTE: [Walter] this could be stored in the selection data instead? But since it should be a constant, it's fine here.
        private const F32 MIN_SQ_DISTANCE_TO_DRAG = 0.4f;

        // public void OnCreate(ref SystemState state)
        // {
        //     //_buildPhysicsWorld = state.World.GetOrCreateSystem<BuildPhysicsWorld>();
        // }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (
                (
                    RefRW<SelectionData>      __selection, 
                    RefRO<SelectionInputData> __input,
                    RefRO<WorldTransform>     __worldTransform, 
                    RefRO<CameraInfo>         __cameraInfo, 
                    RefRO<Team>               __team
                ) in 
                Query<RefRW<SelectionData>, RefRO<SelectionInputData>, RefRO<WorldTransform>, RefRO<CameraInfo>, RefRO<Team>>())
            {
                if (__input.ValueRO.primaryStartedPressThisFrame)
                {
                    __selection.ValueRW.cursorPositionStart = __input.ValueRO.cursorPosition;
                }
                else if (__input.ValueRO.primaryIsPressed)
                {
                    F32x2 __selectionStart   = __selection.ValueRO.cursorPositionStart;
                    F32x2 __selectionCurrent = __input.ValueRO.cursorPosition;
        
                    Bool __isDragging = (distancesq(__selectionStart,  __selectionCurrent) >= MIN_SQ_DISTANCE_TO_DRAG);
                    
                    if (__isDragging)
                    {
                        UpdateHighlight
                        (
                            state:          ref state, 
                            selectionData:  __selection,
                            team:           __team,
                            selectionTopL:  min(__selectionStart, __selectionCurrent), //This is the top since the screen is flipped.
                            selectionBotR:  max(__selectionStart, __selectionCurrent), //This is the bottom since the screen is flipped.
                            cameraPosition: __worldTransform.ValueRO.Position,
                            cameraRotation: __worldTransform.ValueRO.Rotation,
                            cameraScreenSize:           __cameraInfo.ValueRO.screenDimensions,
                            cameraFarClipPlaneDistance: __cameraInfo.ValueRO.farClipPlaneDistance,
                            cameraFieldOfView:          __cameraInfo.ValueRO.fov,
                            useVerticalFovAxis:         __cameraInfo.ValueRO.useVerticalFovAxis
                        );
                    }
                }
                else if (__input.ValueRO.primaryStoppedPressThisFrame)
                {
                    
                }
            }
            
            //_debugBuilder.Dispose();
        }

        private void UpdateHighlight
        (
            ref SystemState      state,
            RefRW<SelectionData> selectionData, 
            RefRO<Team>          team,
            F32x2                selectionTopL, 
            F32x2                selectionBotR, 
            F32x3                cameraPosition, 
            quaternion           cameraRotation, 
            F32x2                cameraScreenSize,
            F32                  cameraFarClipPlaneDistance = 1000,
            F32                  cameraFieldOfView  = 60, 
            Bool                 useVerticalFovAxis = true
        )
        {
            F32x3 __topLeftWorldPosition = CameraUtils.ScreenPointToWorldPosition
            (
                screenPoint:          selectionTopL,
                cameraPosition:       cameraPosition,
                cameraRotation:       cameraRotation,
                farClipPlaneDistance: cameraFarClipPlaneDistance,
                screenSize:           cameraScreenSize,
                fov:                  cameraFieldOfView,
                useVerticalFovAxis:   useVerticalFovAxis
            );
            //_debugBuilder.SolidCircleXY(center: __topLeftWorldPosition, radius: 5f, color: Color.red);

            F32x3 __bottomRightWorldPosition = CameraUtils.ScreenPointToWorldPosition
            (
                screenPoint:          selectionBotR,
                cameraPosition:       cameraPosition,
                cameraRotation:       cameraRotation,
                farClipPlaneDistance: cameraFarClipPlaneDistance,
                screenSize:           cameraScreenSize,
                fov:                  cameraFieldOfView,
                useVerticalFovAxis:   useVerticalFovAxis
            );
            //_debugBuilder.SolidCircleXY(center: __bottomRightWorldPosition, radius: 5f, color: Color.blue);

            F32x3 __bottomLeftWorldPosition = new(x: __topLeftWorldPosition.x,     y: __bottomRightWorldPosition.y, z: __topLeftWorldPosition.z);
            F32x3 __topRightWorldPosition   = new(x: __bottomRightWorldPosition.x, y: __topLeftWorldPosition.y,     z: __bottomRightWorldPosition.z);

            //_debugBuilder.Line(a: __bottomLeftWorldPosition,  b: __topLeftWorldPosition,     color: Color.green);
            //_debugBuilder.Line(a: __topLeftWorldPosition,     b: __topRightWorldPosition,    color: Color.green);
            //_debugBuilder.Line(a: __topRightWorldPosition,    b: __bottomRightWorldPosition, color: Color.green);
            //_debugBuilder.Line(a: __bottomRightWorldPosition, b: __bottomLeftWorldPosition,  color: Color.green);
            
            //_debugBuilder.SolidBox(center: __topLeftWorldPosition, rotation: cameraRotation, size: new F32x3(1, 1, 1), color: Color.white);

            // RaycastInput __raycastInput = new()
            // {
            //     Start  = new F32x3(xy: selectionTopL, z: near),
            //     End    = new F32x3(xy: selectionBotR, z: far),
            //     Filter = new CollisionFilter
            //     {
            //         BelongsTo = ~0u,
            //         CollidesWith = ~0u,
            //         GroupIndex = 0
            //     }
            // };
            
            foreach ((RefRW<HighlightableTag> __highlightable, RefRW<Selectable> __selectable) in Query<RefRW<HighlightableTag>, RefRW<Selectable>>())
            {
                
            }
        }

        // private void UpdateSelection(ref SystemState state, RefRW<SelectionData> selectionData)
        // {
        //     foreach (RefRW<SelectableStateData> __selectableState in Query<RefRW<SelectableStateData>>())
        //     {
        //         
        //     }
        // }
    }
}
