using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace nitou.Sound {
    public partial class Sound {

        /// <summary>
        /// BGM�}�l�[�W���[
        /// </summary>
        private class BgmManager : SoundManagerBase {

            /// ----------------------------------------------------------------------------
            // Field & Properity

            private SoundSource _soundSource = null;

            /// <summary>
            /// �Đ�����BGM
            /// </summary>
            public AudioClip CurrentBGM { get; private set; }

            /// <summary>
            /// ���ɍĐ�����BGM
            /// </summary>
            public AudioClip NextBGM { get; private set; }

            /// <summary>
            /// �Đ������ǂ���
            /// </summary>
            public bool IsPlaying => _soundSource.IsPlaying;

            /// <summary>
            /// �t�F�[�h�A�E�g�����ǂ���
            /// </summary>
            public bool IsFadeOuting { get; private set; }

            // BGM���t�F�[�h����̂ɂ����鎞��
            public const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
            public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
            private float _bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;


            /// ----------------------------------------------------------------------------
            // MonoBehaviour Method

            private void Awake() {
                Initialize();
            }

            private void Update() {
                if (!IsFadeOuting) return;

                //���X�Ƀ{�����[���������Ă����A�{�����[����0�ɂȂ�����{�����[����߂����̋Ȃ𗬂�
                _soundSource.Volume -= Time.deltaTime * _bgmFadeSpeedRate;
                if (_soundSource.Volume <= 0) {
                    Stop();
                    _soundSource.Volume = _volume;

                    if (NextBGM != null) {
                        Play(NextBGM, true);
                    }
                }
            }

            /// ----------------------------------------------------------------------------
            // Public Method

            /// <summary>
            /// BGM���Đ�����
            /// </summary>
            public void Play(AudioClip clip, bool isLoop = true) {
                // �N���b�v����̏ꍇ�C
                if (clip == null) {
                    Stop();
                    return;
                }

                // �����Ȃ��w�肳�ꂽ�ꍇ�͏������Ȃ�
                if (CurrentBGM == clip) {
                    return;
                }

                // �Đ����łȂ���΁C���̂܂ܗ���
                if (!IsPlaying) {
                    _soundSource.Play(clip, isLoop);
                    CurrentBGM = clip;
                }
                // �Đ����Ȃ�t�F�[�h�A�E�g�����Ă��痬��
                else {
                    IsFadeOuting = true;
                    NextBGM = clip;
                }

            }

            /// <summary>
            /// BGM���~����
            /// </summary>
            public void Stop() {
                CurrentBGM = null;
                IsFadeOuting = false;
                _soundSource.Stop();
            }

            /// <summary>
            /// BGM���|�[�Y����
            /// </summary>
            public void Pause() {
                _soundSource.Source.Pause();
            }

            /// <summary>
            /// BGM�̃|�[�Y����������
            /// </summary>
            public void UnPause() {
                _soundSource.Source.UnPause();
            }


            /// ----------------------------------------------------------------------------
            // Private Method

            /// <summary>
            /// ����������
            /// </summary>
            internal override void Initialize() {
                if (IsInitialized) return;

                var audioSouece = gameObject.AddComponent<AudioSource>();
                audioSouece.spatialBlend = 0;                               // ��2D�T�E���h��L����
                audioSouece.volume = Volume;
                _soundSource = new SoundSource(audioSouece, SoundType.BGM);

                // �t���O�X�V
                IsInitialized = true;
            }

            /// <summary>
            /// �I�[�f�B�I�\�[�X�̐ݒ���X�V����
            /// </summary>
            internal override void SetVolume(float value) {
                _volume = Mathf.Clamp01(value);
                _soundSource.Volume = _volume;
            }


            /// ----------------------------------------------------------------------------
            // Static Method

            public static BgmManager Create() {
                var manager = new GameObject("[BGM Manager]").AddComponent<BgmManager>();
                manager.Initialize();
                return manager;
            }
        }

    }
}