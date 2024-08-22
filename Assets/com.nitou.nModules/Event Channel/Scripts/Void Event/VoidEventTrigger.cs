using UnityEngine;

// [NOTE] �������Demo�Ƃ��ɓ����R�[�h���� (2024.08.03)

namespace nitou.EventChannel{

    /// <summary>
    /// Raises an event on trigger collision
    /// </summary>
    [AddComponentMenu(
        ComponentMenu.Prefix.EventChannel + "Void Event Trigger"
    )]
    public class VoidEventTrigger : MonoBehaviour {

        private static string _playerTag = "Player";
        [SerializeField] VoidEventChannel _channel = null;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void OnTriggerEnter(Collider col){
            if (_channel == null) return;

            if (col.CompareTag(_playerTag)) {
                _channel.RaiseEvent();
            }
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �Ώۂ̃^�O��ݒ肷��
        /// </summary>
        public static void SetTagString(string tagName) {
            _playerTag = tagName;
        }


        /// ----------------------------------------------------------------------------
        // Editor Method
#if UNITY_EDITOR

        private void Reset() {

            // ���R���C�_�[��������ΓK���Ȃ��̂��Z�b�g
            if(!TryGetComponent<Collider>(out _)) {
                var col = gameObject.AddComponent<SphereCollider>();
                col.radius = 3f;
            }
        }
#endif
    }
}
