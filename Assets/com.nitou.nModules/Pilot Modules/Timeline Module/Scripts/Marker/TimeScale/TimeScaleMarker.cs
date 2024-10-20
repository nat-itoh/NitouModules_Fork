using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// [�Q�l]
//  �e���V���[��: Timeline���烁�\�b�h���ĂԐV�@�\ Marker��Signal�ASignal Receiver https://tsubakit1.hateblo.jp/entry/2018/12/10/233146
//  github: Unity-Technologies/TimelineMarkerCustomization https://github.com/Unity-Technologies/TimelineMarkerCustomization/tree/master

namespace nitou.Timeline{

    /// <summary>
    /// �^�C���X�P�[����ύX����Marker
    /// </summary>
    [System.Serializable, DisplayName("TimeScale Marker")]
    public class TimeScaleMarker : Marker, INotification{

        [SerializeField] float _timeScale = 1f;
        public float TimeScale => _timeScale;

        /// <summary>
        /// �}�[�J�[�̎���ID
        /// </summary>
        public PropertyName id => new PropertyName("TimeScale");
    }
}
