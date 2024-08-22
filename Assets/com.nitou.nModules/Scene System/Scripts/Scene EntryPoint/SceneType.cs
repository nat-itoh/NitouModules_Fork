using UnityEngine;

namespace nitou.SceneSystem{

    public enum SceneType {

        /// <summary>
        /// �v���C���[�������郁�C���̃��x��
        /// </summary>
        MainLevel,
        
        /// <summary>
        /// �t���I�ȃ��x��
        /// </summary>
        SubLevel,

        /// <summary>
        /// ���̑�
        /// </summary>
        Other,
    }


    /// <summary>
    /// <see cref="SceneType"/>��ΏۂƂ����g�����\�b�h
    /// </summary>
    public static class SceneTypeExtensions {

        /// <summary>
        /// ���x�����ǂ���
        /// </summary>
        public static bool IsLevel(this SceneType type) => 
            (type == SceneType.MainLevel) || (type == SceneType.SubLevel);

    }

}
