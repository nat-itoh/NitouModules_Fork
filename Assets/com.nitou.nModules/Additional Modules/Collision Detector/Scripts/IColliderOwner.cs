using UnityEngine;

namespace nitou.Detecor {

    /// <summary>
    /// <see cref="Collider"/>�̊Ǘ��҂ł��邱�Ƃ������C���^�[�t�F�[�X
    /// </summary>
    public interface IColliderOwner{

        /// <summary>
        /// Checks whether a collider belongs to the character.
        /// </summary>
        public bool IsOwnCollider(Collider col);

    }
}
