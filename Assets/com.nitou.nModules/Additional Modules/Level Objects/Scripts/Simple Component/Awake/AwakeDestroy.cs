using UnityEngine;

namespace nitou.LevelObjects.SimpleComponents {

    /// <summary>
    /// �V�[���ړ��Ŕj�����Ȃ��I�u�W�F�N�g
    /// </summary>
    public sealed class AwakeDestroy : AwakeBehaviour {

        protected override void OnAwake() {
            Destroy(this.gameObject);
        }
    }
}
