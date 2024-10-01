using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace nitou.Audio {

    /// <summary>
    /// Resources�t�H���_����AudioClip���Ǘ�����N���X
    /// </summary>
    public class ResourcesAudioClipContainer {

        private readonly Dictionary<string, AudioClip> _bgmDic;
        private readonly Dictionary<string, AudioClip> _seDic;

        private const string BGM_PATH = "Audio/BGM";
        private const string SE_PATH = "Audio/SE";

        public ResourcesAudioClipContainer() {

            // ���\�[�X�t�H���_����SSE&BGM�̃t�@�C����ǂݍ��݃Z�b�g
            _bgmDic = Resources.LoadAll<AudioClip>(BGM_PATH).ToDictionary(clip => clip.name, clip => clip);
            _seDic = Resources.LoadAll<AudioClip>(SE_PATH).ToDictionary(clip => clip.name, clip => clip);
        }

        /// <summary>
        /// BGM�p�̃N���b�v���擾����
        /// </summary>
        public AudioClip GetBGM(string bgmName) {
            if (_bgmDic.TryGetValue(bgmName, out var bgmClip)) {
                return bgmClip;
            }
            Debug.LogWarning($"BGM {bgmName} �͑��݂��܂���");
            return null;
        }

        /// <summary>
        /// SE�p�̃N���b�v���擾����
        /// </summary>
        public AudioClip GetSE(string seName) {
            if (_seDic.TryGetValue(seName, out var seClip)) {
                return seClip;
            }
            Debug.LogWarning($"SE {seName} �͑��݂��܂���");
            return null;
        }
    }
}
