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
    /// <see cref="SceneType"/>�^�̊�{�I�Ȋg�����\�b�h�W
    /// </summary>
    public static class SceneTypeExtensions {

        /// <summary>
        /// ���x�����ǂ���
        /// </summary>
        public static bool IsLevel(this SceneType type) => 
            (type == SceneType.MainLevel) || (type == SceneType.SubLevel);

        /// <summary>
        /// �^�C�v�ɑΉ������J���[�֕ϊ�����
        /// </summary>
        public static Color ToColor(this SceneType type) {
            return type switch {
                SceneType.MainLevel => Colors.Orange,
                SceneType.SubLevel => Colors.Cyan,
                SceneType.Other => Colors.Gray,
                _ => throw new System.NotImplementedException()
            };
        }
    }

}
