using System.Linq;
using UniRx;
using UnityEngine;

namespace nitou.Sound {

    /// <summary>
    /// �e��T�E���h�@�\���Ăяo�����߂�Static���\�b�h�N���X
    /// </summary>
    public sealed partial class Sound {

        // �C���X�^���X
        public static Sound Instance =>_instance ?? (_instance = new Sound());
        private static Sound _instance = null;

        // Level Objects
        private BgmManager Bgm { get; set; }
        private SeManager Se { get; set; }
        private VoiceManager Voice { get; set; }


        /// ----------------------------------------------------------------------------
        // Private Method (�Z�b�g�A�b�v)

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        private Sound() {
            if (_instance != null) return;
            Initialize();
        }

        /// <summary>
        /// ����������
        /// </summary>
        private void Initialize() {

            var rootObj = new GameObject("[Audio Manager]");
            rootObj.DontDestroyOnLoad();

            // BGM�}�l�[�W���[
            Bgm = BgmManager.Create();
            Bgm.transform.SetParent(rootObj.transform);

            // SE�}�l�[�W���[
            Se = SeManager.Create();
            Se.transform.SetParent(rootObj.transform);

            // Voice�}�l�[�W���[
            Voice = VoiceManager.Create();
            Voice.transform.SetParent(rootObj.transform);
        }


        /// ----------------------------------------------------------------------------
        // Static Method (BGM�Đ�)

        /// <summary>
        /// BGM���Đ�����
        /// </summary>
        public static void PlayBGM(AudioClip audioClip, bool isLoop = true) {
            Instance.Bgm.Play(audioClip, isLoop);
        }

        /// <summary>
        /// BGM���~����
        /// </summary>
        public static void StopBGM() {
            Instance.Bgm.Stop();
        }

        /// <summary>
        /// BGM���|�[�Y����
        /// </summary>
        public static void PauseBGM() {
            Instance.Bgm.Pause();
        }

        /// <summary>
        /// BGM�̃|�[�Y����������
        /// </summary>
        public static void UnPauseBGM() {
            Instance.Bgm.UnPause();
        }


        /// ----------------------------------------------------------------------------
        // Static Method (SE�Đ�)

        /// <summary>
        /// SE���Đ�����
        /// </summary>
        public static void PlaySE(AudioClip audioClip) {
            Instance.Se.Play(audioClip);
        }

        /// <summary>
        /// �S�Ă�SE���~����
        /// </summary>
        public static void StopSE() {
            Instance.Se.StopAll();
        }


        /// ----------------------------------------------------------------------------
        // Static Method (Voice�Đ�)


        /// ----------------------------------------------------------------------------
        // Static Method (Volume)

        /// <summary>
        /// BGM�̃{�����[����ݒ肷��
        /// </summary>
        public static void SetBgmVolume(float value) {
            Instance.Bgm.SetVolume(value);
        }

        /// <summary>
        /// SE�̃{�����[����ݒ肷��
        /// </summary>
        public static void SetSeVolume(float value) {
            Instance.Se.SetVolume(value);
        }

        /// <summary>
        /// Voice�̃{�����[����ݒ肷��
        /// </summary>
        public static void SetVoiceVolume(float value) {
            Instance.Voice.SetVolume(value);
        }
    }

}