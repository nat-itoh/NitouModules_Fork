using System.Collections.Generic;
using UnityEngine;

// [�Q�l]
//  ���낭�܂��ӂ�: �t���O�Ǘ��@�\�̍����I�Q�[���J���ɕK�{�̋@�\�����삵�Ă݂悤 https://kurokumasoft.com/2022/04/28/unity-flag-management/

namespace nitou.FlagManagement {

    public sealed class FlagMasterTable {

        private Dictionary<string, bool> _flags = new Dictionary<string, bool>(){
            { "NewFeatureA", false },
            { "ExperimentalFeatureB", true }
        };

    }
}
