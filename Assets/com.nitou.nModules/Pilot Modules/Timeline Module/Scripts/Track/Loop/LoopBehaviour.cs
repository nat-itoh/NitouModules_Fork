using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace nitou.Timeline {

    [System.Serializable]
    public class LoopBehaviour : PlayableBehaviour {

        public PlayableDirector director { get; set; }

        /// <summary>
        /// �I���t���O���Ǘ�����R���|�[�l���g
        /// </summary>
        public ILoopController controller { get; set; }

        /// <summary>
        /// �N���b�v�I�����̏���
        /// </summary>
        public override void OnBehaviourPause(Playable playable, FrameData info) {
            if (controller == null) return;

            // �I���g���K�[�������Ă���΁C���[�v�𔲂���
            if (controller.ExitLoopTrigger == true) {
                controller.ExitLoopTrigger = false;
                return;
            }

            // �Đ����Ԃ����Z�b�g
            director.time -= playable.GetDuration();
        }
    }

}