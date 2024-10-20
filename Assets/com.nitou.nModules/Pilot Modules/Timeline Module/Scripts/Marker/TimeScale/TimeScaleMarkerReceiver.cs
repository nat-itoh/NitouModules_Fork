using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace nitou.Timeline{

    public class TimeScaleMarkerReceiver : MonoBehaviour, INotificationReceiver {

        /// <summary>
        /// �ʒm���󂯂����̏���
        /// </summary>
        public void OnNotify(Playable origin, INotification notification, object context) {
            var marker = notification as TimeScaleMarker;
            if (marker == null) return;

            ChangeTimeScale(marker.TimeScale);
        }

        /// <summary>
        /// �^�C���X�P�[���̕ύX
        /// </summary>
        private void ChangeTimeScale(float timeScale) {
            Time.timeScale = timeScale;
        }
    }
}
