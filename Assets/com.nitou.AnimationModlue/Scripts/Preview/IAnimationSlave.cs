using UnityEngine;

namespace nitou.AnimationModule{

    /// <summary>
    /// AnimationClip�Đ��ɉ����ċ쓮����C���^�[�t�F�[�X
    /// </summary>
    public interface IAnimationSlave {

        /// <summary>
        /// ���݂̍Đ�����
        /// </summary>
        public float NormalizedTime { get; set; }
    }
}
