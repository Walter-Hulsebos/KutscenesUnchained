using System.Runtime.CompilerServices;
using Unity.Collections;
using static System.Runtime.CompilerServices.MethodImplOptions;
using UnityEngine.Device;
using Unity.Mathematics;
using static Unity.Mathematics.math;
using Unity.Physics;

using F32   = System.Single;
using F32x2 = Unity.Mathematics.float2;
using F32x3 = Unity.Mathematics.float3;
using F32x4 = Unity.Mathematics.float4;

using I32   = System.Int32;

using Bool  = System.Boolean;

using Ray = Unity.Physics.Ray;

namespace Game.Utils
{
    public static class CameraUtils
    {
        /// <summary> Calculates the world position of a screen point </summary>
        [MethodImpl(AggressiveInlining)]
        public static F32x3 ScreenPointToWorldPosition(F32x2 screenPoint, F32x3 cameraPosition, quaternion cameraRotation, F32 farClipPlaneDistance, F32x2 screenSize, F32 fov, Bool useVerticalFovAxis = true)
        {
            F32x3 __cameraRight   = mul(cameraRotation, new F32x3(x: 1, y: 0, z: 0));
            F32x3 __cameraUp      = mul(cameraRotation, new F32x3(x: 0, y: 1, z: 0));
            F32x3 __cameraForward = mul(cameraRotation, new F32x3(x: 0, y: 0, z: 1));
            
            F32x3 __worldPoint = cameraPosition + __cameraForward * farClipPlaneDistance;
            
            F32 __aspectRatio = screenSize.x / screenSize.y;
            F32 __fovRadians  = radians(fov);
            F32 __fovAxisLength = useVerticalFovAxis 
                ? farClipPlaneDistance * tan(__fovRadians * 0.5f) 
                : farClipPlaneDistance * tan(__fovRadians * 0.5f) * __aspectRatio;

            F32x2 __screenPoint01  = screenPoint / screenSize;
            F32x2 __screenPointNDC = __screenPoint01 * 2.0f - 1.0f;
            
            F32x2 __screenPointFOVAxis  = __screenPointNDC * __fovAxisLength;
            F32x3 __screenPointFOVAxis3 = new(xy: __screenPointFOVAxis, z: 0.0f);
            
            __worldPoint += __cameraRight * __screenPointFOVAxis3.x;
            __worldPoint += __cameraUp    * __screenPointFOVAxis3.y;
            
            return __worldPoint;
        }
        
        [MethodImpl(AggressiveInlining)]
        public static NativeArray<Ray> ScreenPointsToRays(NativeArray<F32x2> screenPoints, F32x3 cameraPosition, quaternion cameraRotation, F32 farClipPlaneDistance, F32x2 screenSize, F32 fov, Bool useVerticalFovAxis = true)
        {
            NativeArray<Ray> __rays = new(screenPoints.Length, Allocator.Temp);
            
            F32x3 __cameraRight   = mul(cameraRotation, new F32x3(x: 1, y: 0, z: 0));
            F32x3 __cameraUp      = mul(cameraRotation, new F32x3(x: 0, y: 1, z: 0));
            F32x3 __cameraForward = mul(cameraRotation, new F32x3(x: 0, y: 0, z: 1));
            
            F32x3 __worldPoint = cameraPosition + __cameraForward * farClipPlaneDistance;
            
            F32 __aspectRatio = screenSize.x / screenSize.y;
            F32 __fovRadians  = radians(fov);
            F32 __fovAxisLength = useVerticalFovAxis 
                ? farClipPlaneDistance * tan(__fovRadians * 0.5f) 
                : farClipPlaneDistance * tan(__fovRadians * 0.5f) * __aspectRatio;

            for (I32 __index = 0; __index < screenPoints.Length; __index += 1)
            {
                F32x2 __screenPoint = screenPoints[__index];
                
                F32x2 __screenPoint01  = __screenPoint / screenSize;
                F32x2 __screenPointNDC = __screenPoint01 * 2.0f - 1.0f;

                F32x2 __screenPointFOVAxis = __screenPointNDC * __fovAxisLength;
                F32x3 __screenPointFOVAxis3 = new(xy: __screenPointFOVAxis, z: 0.0f);

                __worldPoint += __cameraRight * __screenPointFOVAxis3.x;
                __worldPoint += __cameraUp * __screenPointFOVAxis3.y;

                __rays[__index] = new Ray
                {
                    Origin       = cameraPosition,
                    Displacement = normalize(__worldPoint - cameraPosition)
                };
            }

            return __rays;
        }

        [MethodImpl(AggressiveInlining)]
        public static Ray ScreenPointToRay(F32x2 screenPoint, F32x3 cameraPosition, quaternion cameraRotation, F32 farClipPlaneDistance, F32x2 screenSize, F32 fov, Bool useVerticalFovAxis = true)
        {   
            return new Ray
            {
                Origin        = cameraPosition, 
                Displacement  = normalize(ScreenPointToWorldPosition(screenPoint, cameraPosition, cameraRotation, farClipPlaneDistance, screenSize, fov, useVerticalFovAxis) - cameraPosition)
            };
        }

        [MethodImpl(AggressiveInlining)]
        public static RaycastInput ScreenPointToRaycastInput(F32x2 screenPoint, F32x3 cameraPosition, quaternion cameraRotation, F32 farClipPlaneDistance, F32x2 screenSize, F32 fov, CollisionFilter filter, Bool useVerticalFovAxis = true)
        {
            return new RaycastInput
            {
                Start  = cameraPosition, 
                End    = ScreenPointToWorldPosition(screenPoint, cameraPosition, cameraRotation, farClipPlaneDistance, screenSize, fov, useVerticalFovAxis),
                Filter = filter
            };
        }
    }
}