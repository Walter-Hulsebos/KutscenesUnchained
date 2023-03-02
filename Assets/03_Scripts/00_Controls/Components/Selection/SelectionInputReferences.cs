using System;
using Unity.Collections;
using Unity.Entities;

namespace Game.Controls.Selection
{
    public readonly struct SelectionInputReferences : IComponentData
    {
        public readonly FixedString64Bytes primaryId;
        public readonly FixedString64Bytes addModifierId;
        public readonly FixedString64Bytes delModifierId;
        public readonly FixedString64Bytes cursorPositionId;

        public SelectionInputReferences
        (
            Guid primaryId,
            Guid addModifiedId, 
            Guid delModifiedId, 
            Guid cursorPositionId
        )
        {
            this.primaryId         = primaryId.ToString();
            this.addModifierId     = addModifiedId.ToString();
            this.delModifierId     = delModifiedId.ToString();
            this.cursorPositionId  = cursorPositionId.ToString();
        }
        
        public SelectionInputReferences
        (
            FixedString64Bytes primaryId,
            FixedString64Bytes addModifiedId, 
            FixedString64Bytes delModifiedId, 
            FixedString64Bytes cursorPositionId
        )
        {
            this.primaryId         = primaryId;
            this.addModifierId     = addModifiedId;
            this.delModifierId     = delModifiedId;
            this.cursorPositionId  = cursorPositionId;
        }
    }
}