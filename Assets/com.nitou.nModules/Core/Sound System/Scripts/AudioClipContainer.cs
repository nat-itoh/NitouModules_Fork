using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nitou.Sound{

    public class AudioClipContainer{

        // �SAudioClip��ێ�
        private Dictionary<string, AudioClip> _clipDicti;

        /// <summary>
        /// �܂܂��N���b�v��
        /// </summary>
        public int Count => _clipDicti.Count;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public AudioClipContainer(string path) {

            _clipDicti = new Dictionary<string, AudioClip>();
            foreach (AudioClip clip in Resources.LoadAll(path)) {
                _clipDicti[clip.name] = clip;
            }
        }

        /// <summary>
        /// �N���b�v���擾����
        /// </summary>
        public AudioClip GetClip(string clipName) {
            if (!_clipDicti.ContainsKey(clipName)) {
                Debug.Log(clipName + "�Ƃ������O��clip������܂���");
                return null;
            }
            return _clipDicti[clipName];
        }
    }
}
