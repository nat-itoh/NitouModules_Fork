using UnityEngine;

namespace nitou.LevelObjects.SimpleComponents {

    /// <summary>
    /// �V�[���J�n���Ɏq�I�u�W�F�N�g�����ׂč폜����R���|�[�l���g
    /// </summary>
    [DefaultExecutionOrder(GameConfigs.ExecutionOrder.FARST)]
    [DisallowMultipleComponent]
    public sealed class AutoDestroyChildrenComponent : MonoBehaviour{

        private void Awake() {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
        }
    }
}
