using Unity.Entities;

using F32   = System.Single;
using F32x2 = Unity.Mathematics.float2;

using Bool  = System.Boolean;

namespace Game.Selection
{
    public struct CameraInfo : IComponentData
    {
        public F32    fov;
        public Bool   useVerticalFovAxis;
        public F32    aspectRatio;
        public F32    nearClipPlaneDistance;
        public F32    farClipPlaneDistance;
        public F32x2  screenDimensions;
    }
}
