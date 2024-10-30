using UnityEngine;

namespace nitou.AnimationModule{

    /// <summary>
    /// (�J�n / �ҋ@ / �I��) ���[�V�����ō\�������V���v���ȃA�j���[�V�����Z�b�g�D
    /// </summary>
    public interface ISequentialAnimSet : IAnimSet{
        
        public AnimationClip StartClip { get; }
        public AnimationClip LoopClip { get; }
        public AnimationClip EndClip { get; }
    }
}
