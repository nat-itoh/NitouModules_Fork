using UnityEngine;

namespace nitou.LevelObjects.SimpleComponents {

    /// <summary>
    /// �V�[���J�n���Ɏq�I�u�W�F�N�g�����ׂč폜����R���|�[�l���g
    /// </summary>
    internal sealed class AwakeDestroyChildrenComponent : AwakeBehaviour{

        /// <summary>
        /// �J�n����
        /// </summary>
        protected override void OnAwake() {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
        }
    }
}
