using Unity.Entities;

using U08 = System.Byte;

namespace Game.Teams
{
    public struct Team : IComponentData
    {
        public U08 value;
    }
}