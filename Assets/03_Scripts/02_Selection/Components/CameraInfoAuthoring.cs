using System;
using UnityEngine;
using Unity.Entities;

using F32   = System.Single;
using F32x2 = Unity.Mathematics.float2;

using Bool  = System.Boolean;

namespace Game.Selection
{
    public sealed class CameraInfoAuthoring : MonoBehaviour
    {
        [SerializeField] private F32   fieldOfView;
        [SerializeField] private Bool  useVerticalFovAxis;
        [SerializeField] private F32   aspectRatio;
        [SerializeField] private F32   nearClipPlaneDistance;
        [SerializeField] private F32   farClipPlaneDistance;
        [SerializeField] private F32x2 screenDimensions;
        
        //[SerializeField] private Camera mainCamera;

        private void Reset()
        {
            Camera __mainCamera = Camera.main;

            if (__mainCamera == null)
            {
                Debug.LogWarning(message: "No main camera found!", context: this);
            }
            else
            {
                fieldOfView           = __mainCamera.fieldOfView;
                useVerticalFovAxis    = true;
                aspectRatio           = __mainCamera.aspect;
                nearClipPlaneDistance = __mainCamera.nearClipPlane;
                farClipPlaneDistance  = __mainCamera.farClipPlane;
                screenDimensions      = new F32x2(__mainCamera.pixelWidth, __mainCamera.pixelHeight);
            }
        }

        private sealed class CameraInfoBaker : Baker<CameraInfoAuthoring>
        {
            public override void Bake(CameraInfoAuthoring authoring)
            {
                AddComponent(new CameraInfo
                {
                    fov                   = authoring.fieldOfView,
                    useVerticalFovAxis    = authoring.useVerticalFovAxis,
                    aspectRatio           = authoring.aspectRatio,
                    nearClipPlaneDistance = authoring.nearClipPlaneDistance,
                    farClipPlaneDistance  = authoring.farClipPlaneDistance,
                    screenDimensions      = authoring.screenDimensions
                });
            }
        }
    }
}