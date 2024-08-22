using System.Collections.Generic;
using UnityEngine;

namespace nitou.EventChannel.Shared {


    /// <summary>
    /// �C�x���g���X�i�[�̊��N���X
    /// </summary>
    public abstract class EventListener<TChannel> : MonoBehaviour
        where TChannel : EventChannel {

        // 
        public TChannel Channel = null;
        public event System.Action OnEventRaised = delegate { };

        /// <summary>
        /// �`�����l�����Z�b�g����Ă��邩�ǂ���
        /// </summary>
        public bool HaveChannel => Channel != null;


        /// ----------------------------------------------------------------------------
        // Public Method

        private void OnEnable() {
            if (Channel == null) return;
            Channel.OnEventRaised += Respond;
        }

        private void OnDisable() {
            if (Channel == null) return;
            Channel.OnEventRaised -= Respond;
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �C�x���g���Ύ��̃��X�|���X
        /// </summary>
        public void Respond() => OnEventRaised.Invoke();
    }



    /// <summary>
    /// �C�x���g���X�i�[�̊��N���X
    /// </summary>
    public abstract class EventListener<Type, TChannel> : MonoBehaviour
        where TChannel : EventChannel<Type> {

        // 
        public TChannel Channel = null;
        public event System.Action<Type> OnEventRaised = delegate { };

        /// <summary>
        /// �`�����l�����Z�b�g����Ă��邩�ǂ���
        /// </summary>
        public bool HaveChannel => Channel != null;


        /// ----------------------------------------------------------------------------
        // Public Method

        private void OnEnable() {
            if (Channel == null) return;
            Channel.OnEventRaised += Respond;
        }

        private void OnDisable() {
            if (Channel == null) return;
            Channel.OnEventRaised -= Respond;
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �C�x���g���Ύ��̃��X�|���X
        /// </summary>
        public void Respond(Type value) => OnEventRaised.Invoke(value);     // ��null�`�F�b�N��Channel���ōs��
    }

}
