using UnityEngine;

namespace nitou.LevelObjects{

    /// <summary>
    /// ���x�����̃C���^���N�g�\�ȃI�u�W�F�N�g�D
    /// </summary>
    public interface IInteractable{

        /// <summary>
        /// �D�揇�ʁD
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// 
        /// </summary>
        Vector3 Position { get; set; }

        void Intaract();
    }
}
