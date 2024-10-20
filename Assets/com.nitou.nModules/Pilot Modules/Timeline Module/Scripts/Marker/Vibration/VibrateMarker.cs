using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// [�Q�l]
//  qiita: Timeline����f�o�C�X�̐U���𐧌䂷�� https://qiita.com/nkjzm/items/27b2a6c9ed0cc844a83c

namespace nitou.Timeline{

    /// <summary>
    /// �U���C�x���g��ʒm����Marker
    /// </summary>
    [System.Serializable, DisplayName("Vibrate Marker")]
    public class VibrateMarker : Marker, INotification {

        public float Duration = 0.5f;
        [Range(0f, 1f)] public float Power = 0.5f;
        [Range(0f, 1f)] public float Frequency = 0.5f;

        /// <summary>
        /// �}�[�J�[�̎���ID
        /// </summary>
        public PropertyName id => new PropertyName("Vibration");
    }
}
