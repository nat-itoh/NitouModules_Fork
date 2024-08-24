#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

// [�Q�l]
//  qiita: Unity�œƎ��̐ݒ��UI��񋟂ł���SettingsProvider�̏Љ�Ɛݒ�t�@�C���̕ۑ��ɂ��� https://qiita.com/sune2/items/a88cdee6e9a86652137c

namespace nitou.EditorShared {

    public class PreferencesProvider : SettingsProvider{

        // �ݒ�̃p�X (����1�K�w�́uPreferences�v�ɂ���)
        private const string SettingPath = SettingsProviderKey.Preference + "My Preferences";

        private Editor _editor;


        /// <summary>
        /// ���̃��\�b�h���d�v�ł�
        /// �Ǝ���SettingsProvider��Ԃ����ƂŁA�ݒ荀�ڂ�ǉ����܂�
        /// </summary>
        [SettingsProvider]
        public static SettingsProvider CreateSettingProvider() {
            return new PreferencesProvider(SettingPath, SettingsScope.User, null);
        }


        /// ----------------------------------------------------------------------------

        public PreferencesProvider(string path, SettingsScope scopes, IEnumerable<string> keywords) : base(path, scopes, keywords) {}

        public override void OnActivate(string searchContext, VisualElement rootElement) {

            var preferences = PreferencesSO.instance;
            
            // ��ScriptableSingleton��ҏW�\�ɂ���
            preferences.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;

            // �ݒ�t�@�C���̕W���̃C���X�y�N�^�[�̃G�f�B�^�𐶐�
            Editor.CreateCachedEditor(preferences, null, ref _editor);
        }


        public override void OnGUI(string searchContext) {

            EditorGUI.BeginChangeCheck();

            // �ݒ�t�@�C���̕W���C���X�y�N�^��\��
            _editor.OnInspectorGUI();

            //EditorGUILayout.LabelField("�e�X�g����");

            if (EditorGUI.EndChangeCheck()) {
                PreferencesSO.instance.Save();
            }
        }
    }

}
#endif