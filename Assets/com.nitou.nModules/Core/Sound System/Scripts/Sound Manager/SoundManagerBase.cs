using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nitou.Sound {

    /// <summary>
    /// AudioSource�����b�v�����}�l�[�W���[�N���X
    /// </summary>
    public abstract class SoundManagerBase : MonoBehaviour {

        protected float _volume = 0.5f;
        protected bool _isMuted = false;

        /// <summary>
        /// �{�����[��
        /// </summary>
        public virtual float Volume {
            get => _volume;
            set => SetVolume(value);
        }

        /// <summary>
        /// �~���[�g��Ԃ��ǂ���
        /// </summary>
        public virtual bool IsMuted {
            get => _isMuted;
            set => _isMuted = value;
        }

        /// <summary>
        /// ���������������Ă��邩�ǂ���
        /// </summary>
        public bool IsInitialized {get; protected set;}



        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// ����������
        /// </summary>
        internal abstract void Initialize();

        /// <summary>
        /// ���ʂ�ݒ肷��
        /// </summary>
        internal virtual void SetVolume(float value) {
            _volume = Mathf.Clamp01(value);
        }
    }

}