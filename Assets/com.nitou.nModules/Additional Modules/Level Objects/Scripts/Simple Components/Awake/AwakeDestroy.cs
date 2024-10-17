using UnityEngine;

namespace nitou.LevelObjects.SimpleComponents {

    /// <summary>
    /// �V�[���ړ��Ŕj�����Ȃ��I�u�W�F�N�g
    /// </summary>
    internal sealed class AwakeDestroy : AwakeBehaviour {

        /// <summary>
        /// �J�n����
        /// </summary>
        protected override void OnAwake() {
            Destroy(this.gameObject);
        }
    }
}
