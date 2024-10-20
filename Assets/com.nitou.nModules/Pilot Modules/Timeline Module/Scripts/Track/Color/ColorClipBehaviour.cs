using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace nitou.Timeline {

    public class ColorClipBehaviour : PlayableBehaviour {


        public ColorClip Clip { get; set; }
        public Color OutputColor { get; private set; }


        /// ----------------------------------------------------------------------------
        // �O���t�̊J�n�E�I����

        public override void OnGraphStart(Playable playable) {

        }

        public override void OnGraphStop(Playable playable) {

        }


        /// ----------------------------------------------------------------------------
        // �N���b�v�̐����E�j����

        public override void OnBehaviourPlay(Playable playable, FrameData info) {

        }

        public override void OnBehaviourPause(Playable playable, FrameData info) {

        }


        /// ----------------------------------------------------------------------------
        // �N���b�v�̎��s��

        public override void PrepareFrame(Playable playable, FrameData info) { }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData) {

            var t = playable.GetTime();
            var d = playable.GetDuration();     // ��playable.GetDuration()����ClipCaps.loop�Ł����Ԃ����炵��
            var a = (float)(t / d);
            OutputColor = Clip.gradient.Evaluate(a);
        }
    }
}
