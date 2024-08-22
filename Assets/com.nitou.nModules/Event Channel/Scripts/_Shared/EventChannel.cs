using UnityEngine;

// [�Q�l]
//  youtube: Devlog 2�b�X�N���v�^�u���I�u�W�F�N�g���g�����Q�[���A�[�L�e�N�`�� https://www.youtube.com/watch?v=WLDgtRNK2VE

namespace nitou.EventChannel.Shared {
    using nitou.Inspector;

    /// <summary>
    /// �C�x���g�`�����l���p�̂�������ƂȂ�Scriptable Object
    /// </summary>
    public abstract class EventChannel : ScriptableObject {


#if UNITY_EDITOR
#pragma warning disable 0414
        // ������
        [Multiline]
        [SerializeField] private string _description = default;
#pragma warning restore 0414
#endif
        public event System.Action OnEventRaised = delegate { };


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �C�x���g�̔���
        /// </summary>
        [Button()]
        public void RaiseEvent() => OnEventRaised.Invoke();
    }


    /// <summary>
    /// �C�x���g�`�����l���p�̂�������ƂȂ�Scriptable Object
    /// </summary>
    public abstract class EventChannel<Type> : ScriptableObject {

#if UNITY_EDITOR
#pragma warning disable 0414
        // ������
        [Multiline]
        [SerializeField] private string _description = default;
#pragma warning restore 0414
#endif
        public event System.Action<Type> OnEventRaised = delegate { };


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �C�x���g�̔���
        /// </summary>
        [Button()]
        public void RaiseEvent(Type value) {
            if (value == null) {
                Debug.LogWarning($"[{name}] �C�x���g������null�ł�");
                return;
            }
            OnEventRaised.Invoke(value);
        }
    }
}