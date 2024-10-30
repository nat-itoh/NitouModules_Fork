using UnityEngine;
using Animancer;
using Sirenix.OdinInspector;

namespace nitou.AnimationModule{

    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class SequentialAnimSet : ISequentialAnimSet{

        [DarkBox]
        [SerializeField, Indent] AnimationClip _startClip;
        [DarkBox]
        [SerializeField, Indent] AnimationClip _loopClip;
        [DarkBox]
        [SerializeField, Indent] AnimationClip _endClip;


        /// <summary>
        /// �J�n�A�j���[�V����
        /// </summary>
        public AnimationClip StartClip => _startClip;

        /// <summary>
        /// ���[�v�A�j���[�V����
        /// </summary>
        public AnimationClip LoopClip => _loopClip;

        /// <summary>
        /// �I���A�j���[�V����
        /// </summary>
        public AnimationClip EndClip => _endClip;
    }
}
