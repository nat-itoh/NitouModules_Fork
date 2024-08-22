using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nitou.Sound {

    /// <summary>
    /// AudioListener�̃��b�v�N���X
    /// </summary>
    public class SoundSource {

        public string ID { get; private set; }
        public SoundType Type { get; private set; }
        public AudioSource Source { get; private set; }

        /// <summary>
        /// �N���b�v
        /// </summary>
        public AudioClip Clip => (Source != null) ? Source.clip : null;

        /// <summary>
        /// ����
        /// </summary>
        public float Volume {
            get => Source.volume;
            set => Source.volume = value;
        }

        /// <summary>
        /// �Đ������ǂ���
        /// </summary>
        public bool IsPlaying => (Source != null) ? Source.isPlaying : false;

        /// <summary>
        /// ���[�v�����ǂ���
        /// </summary>
        public bool IsLoop => (Source != null) ? Source.loop : false;

        /// <summary>
        /// �L���X�g
        /// </summary>
        /// <param name="sound"></param>
        public static implicit operator AudioSource(SoundSource sound) => sound.Source;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public SoundSource(AudioSource audioSource, SoundType soundType) {
            Source = audioSource;
            Type = soundType;
        }

        /// <summary>
        /// �T�E���h���Đ�����
        /// </summary>
        public void Play(AudioClip audioClip, bool loop = false, float spatialBlend = 0, float maxDistance = 256) {
            ID = System.Guid.NewGuid().ToString("N");
            Source.loop = loop;
            Source.clip = audioClip;
            Source.spatialBlend = spatialBlend;
            Source.maxDistance = maxDistance;
            Source.Play();
        }

        /// <summary>
        /// �T�E���h���~���܂�
        /// </summary>
        public void Stop() {
            ID = "";
            Source.Stop();
        }

    }

}