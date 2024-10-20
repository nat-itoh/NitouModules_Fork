#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEditor.Timeline;

// [�Q�l]
//  qiita: Unity2019.2����g����悤�ɂȂ���TrackEditor�Ńg���b�N�̌����ڂ��g������ https://qiita.com/jukey17/items/110534b3e13a57c57a89

namespace nitou.Timeline.Editor {

    [CustomTimelineEditor(typeof(ColorTrack))]
    public class ColorTrackEditor : TrackEditor {

        private Texture2D _iconTexture;
        private const string FILE_PATH = "CustomTrack-Icon";


        /// ----------------------------------------------------------------------------
        // ClipEditor Method

        /// <summary>
        /// �g���b�N�̌����ڂ��g������I�v�V�����f�[�^��Ԃ����\�b�h
        /// </summary>
        public override TrackDrawOptions GetTrackOptions(TrackAsset track, Object binding) {
            track.name = "ColorTrack";

            if (!_iconTexture) {
                _iconTexture = Resources.Load<Texture2D>(FILE_PATH);
            }

            var options = base.GetTrackOptions(track, binding);
            options.trackColor = Color.magenta;
            options.icon = _iconTexture;
            return options;
        }




    }

}
#endif