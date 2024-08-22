using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace nitou.Sound {
    public partial class Sound {

        /// <summary>
        /// �{�C�X�}�l�[�W���[
        /// </summary>
        private class VoiceManager : SoundManagerBase {

            /// ----------------------------------------------------------------------------
            // Field & Properity

            private AudioSource[] _sourceArray = null;

            // �`�����l����
            const int VOICE_CHANNEL = 4;


            /// ----------------------------------------------------------------------------
            // MonoBehaviour Method

            private void Awake() {
                Initialize();
            }


            /// ----------------------------------------------------------------------------
            // Private Method


            /// ----------------------------------------------------------------------------
            // Public Method

            /// <summary>
            /// VOICE���Đ�����
            /// </summary>
            public void Play(AudioClip clip) {
                // �N���b�v����̏ꍇ�C
                if (clip == null) { return; }

                // �Đ�
                if (TryGetSource(out var source)) {
                    source.PlayOneShot(clip);
                }
                // �����g�p�̃\�[�X�������ꍇ�C
                else {
                    Debug.LogWarning("There are no idle audio source.");
                }
            }

            /// <summary>
            /// �S�Ă�SE���~����
            /// </summary>
            public void StopAll() {
                foreach (var source in _sourceArray) {
                    source.Stop();
                }
            }




            /// <summary>
            /// ����������
            /// </summary>
            internal override void Initialize() {
                if (IsInitialized) return;

                _sourceArray = new AudioSource[VOICE_CHANNEL];
                for (int i = 0; i < VOICE_CHANNEL; i++) {
                    var audioSouece = gameObject.AddComponent<AudioSource>();
                    audioSouece.spatialBlend = 1;
                    audioSouece.volume = Volume;
                    _sourceArray[i] = audioSouece;
                }

                // �t���O�X�V
                IsInitialized = true;
            }

            /// <summary>
            /// �I�[�f�B�I�\�[�X�̐ݒ���X�V����
            /// </summary>
            internal override void SetVolume(float value) {
                _volume = Mathf.Clamp01(value);
                foreach (var source in _sourceArray) {
                    source.volume = _volume;
                }
            }


            /// ----------------------------------------------------------------------------
            // Private Method

            /// <summary>
            /// ��Đ����̃I�[�f�B�I�\�[�X���擾����
            /// </summary>
            private bool TryGetSource(out AudioSource source) {
                source = _sourceArray.First(s => !s.isPlaying);
                return source != null;
            }

            /// <summary>
            /// �w�肵���I�[�f�B�I�\�[�X���擾����
            /// </summary>
            private AudioSource GetSource(int channel) =>
                (0 <= channel && channel < VOICE_CHANNEL) ? _sourceArray[channel] : null;


            /// ----------------------------------------------------------------------------
            // Static Method

            public static VoiceManager Create() {
                var manager = new GameObject("[Voice Manager]").AddComponent<VoiceManager>();
                manager.Initialize();
                return manager;
            }
        }

    }
}