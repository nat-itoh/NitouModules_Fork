using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
#if UNITY_EDITOR
using System.ComponentModel;
#endif

namespace nitou.Timeline {
    
    [System.Serializable]
#if UNITY_EDITOR
    [DisplayName("Color Gradation Clip")]
#endif
    public class ColorClip : PlayableAsset, ITimelineClipAsset {

        public Gradient gradient;

        /// <summary>
        /// �f�t�H���g�̃N���b�v����
        /// </summary>
        public override double duration => TimelineConfig.DEFAULT_CLIP_LENGTH;

        /// <summary>
        /// �N���b�v�̐U�镑��
        /// </summary>
        public ClipCaps clipCaps {
            get =>
                ClipCaps.Blending |
                ClipCaps.Extrapolation |
                ClipCaps.ClipIn;
        }


        /// ----------------------------------------------------------------------------
        // Override Method

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
            
            var playable = ScriptPlayable<ColorClipBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            behaviour.Clip = this;
            
            return playable;
        }
    }
}
