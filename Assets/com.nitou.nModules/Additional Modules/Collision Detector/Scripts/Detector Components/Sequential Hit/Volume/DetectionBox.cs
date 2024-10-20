using UnityEngine;
using Sirenix.OdinInspector;

namespace nitou.Detecor {

    /// <summary>
    /// �����o�̃^�C�~���O�Ɣ͈͂Ɋւ���f�[�^
    /// </summary>
    [System.Serializable]
    public class DetectionBox {

        /// <summary>
        /// �����o���s���^�C�~���O (Fixed time)
        /// </summary>
        [MinMaxSlider(0, 1)] public Vector2 timeRange;

        /// <summary>
        /// �����o�͈̔́D
        /// </summary>
        public Shapes.Box volume;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public DetectionBox() {
            timeRange = new Vector2(0.2f, 0.8f);
            volume = new Shapes.Box(Vector3.zero, Quaternion.identity, Vector3.one);
        }

        /// <summary>
        /// �w�莞�Ԃ����o�^�C�~���O�����ǂ����m�F����
        /// </summary>
        public bool IsInTimeRange(float fixedTime) {
            return timeRange.x <= fixedTime && fixedTime <= timeRange.y;
        }
    }
}
