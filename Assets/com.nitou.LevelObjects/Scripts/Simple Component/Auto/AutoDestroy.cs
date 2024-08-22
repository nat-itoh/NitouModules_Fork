using UnityEngine;

namespace nitou.LevelObjects.SimpleComponents {

    /// <summary>
    /// �V�[���ړ��Ŕj�����Ȃ��I�u�W�F�N�g
    /// </summary>
    [DefaultExecutionOrder(GameConfigs.ExecutionOrder.FARST)]
    [DisallowMultipleComponent]
    public sealed class AutoDestroy : MonoBehaviour{

        private void Awake() {
            Destroy(this.gameObject);
        }
    }
}
