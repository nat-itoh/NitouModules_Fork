#if UNITY_EDITOR
using UnityEditor;

// [�Q�l]
//  hatena: �G�f�B�^�g���ŁuManager�v�I�Ȃ��̂Ɏg����ScriptableSingleton https://light11.hatenadiary.com/entry/2021/03/11/201303

namespace nitou.EditorShared {

    /// <summary>
    /// �G�f�B�^�p�̃v���t�@�����X�ݒ�f�[�^
    /// </summary>
    [FilePath(
        "MyPreferences.asset", 
        FilePathAttribute.Location.PreferencesFolder
    )]
    public class PreferencesSO : ScriptableSingleton<PreferencesSO> {

        public bool flag;
        public string text;

        /// ----------------------------------------------------------------------------

        public void Save() => Save(true);
    }
}
#endif