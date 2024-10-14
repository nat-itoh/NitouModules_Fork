using System;
using UnityEngine;

namespace nitou.Networking{

    [Serializable]
    public class TimeResponse : HttpResponse{

        public string datetime;  // API����擾���������̃t�H�[�}�b�g
        public string timezone;
        public string utc_offset;

        // ���̃��\�b�h�Ń��X�|���X�f�[�^���p�[�X
        public DateTime GetDateTime() {
            if (DateTime.TryParse(datetime, out DateTime parsedDateTime)) {
                return parsedDateTime;
            }
            return DateTime.MinValue;  // �p�[�X���s���̃f�t�H���g�l
        }

        public override string ToString() {
            return $"Timezone: {timezone}, DateTime: {datetime}, UTC Offset: {utc_offset}";
        }
    }
}
