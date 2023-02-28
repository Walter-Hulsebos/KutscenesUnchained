using System;
using Unity.Collections;
using Unity.Entities;

namespace Game.Controls.Selection
{
    public readonly struct SelectionInputReferences : IComponentData
    {
        public readonly FixedString64Bytes primaryId;
        public readonly FixedString64Bytes secondaryId;
        public readonly FixedString64Bytes cursorPositionId;

        public SelectionInputReferences(Guid primaryId, Guid secondaryId, Guid cursorPositionId)
        {
            this.primaryId         = primaryId.ToString();
            this.secondaryId       = secondaryId.ToString();
            this.cursorPositionId  = cursorPositionId.ToString();
        }
        
        public SelectionInputReferences(FixedString64Bytes primaryId, FixedString64Bytes secondaryId, FixedString64Bytes cursorPositionId)
        {
            this.primaryId         = primaryId;
            this.secondaryId       = secondaryId;
            this.cursorPositionId  = cursorPositionId;
        }
    }
}