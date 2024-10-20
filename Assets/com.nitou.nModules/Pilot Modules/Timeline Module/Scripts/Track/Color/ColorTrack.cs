using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
#if UNITY_EDITOR
using System.ComponentModel;
#endif

// [�Q�l]
//  ����Tips: Timeline�̃J�X�^���g���b�N����уN���b�v���쐬���Č����ڂ��L���C�ɂ��Ă݂� https://tips.hecomi.com/entry/2022/03/28/235336

namespace nitou.Timeline {

    [TrackClipType(typeof(ColorClip))]
    [TrackBindingType(typeof(Renderer))]
#if UNITY_EDITOR
    [DisplayName("Color Gradation Track")]
#endif
    public class ColorTrack : TrackAsset {

        /// ----------------------------------------------------------------------------
        // Override Method

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
            
            return ScriptPlayable<ColorTrackMixer>.Create(graph,inputCount);
        }

    }
}
