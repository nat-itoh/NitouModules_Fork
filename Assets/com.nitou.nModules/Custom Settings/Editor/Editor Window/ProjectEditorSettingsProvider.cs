#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace nitou.EditorShared {

    public class ProjectEditorSettingsProvider : SettingsProvider {

        // �ݒ�̃p�X (����1�K�w�́uPreferences�v�ɂ���)
        private const string SettingPath = SettingsProviderKey.ProjectSettings +"Editor";

        private Editor _editor;


        /// <summary>
        /// ���̃��\�b�h���d�v�ł�
        /// �Ǝ���SettingsProvider��Ԃ����ƂŁA�ݒ荀�ڂ�ǉ����܂�
        /// </summary>
        [SettingsProvider]
        public static SettingsProvider CreateSettingProvider() {
            // ����O������keywords�́A�������ɂ��̐ݒ荀�ڂ����������邽�߂̃L�[���[�h
            return new ProjectEditorSettingsProvider(SettingPath, SettingsScope.Project, null);
        }


        /// ----------------------------------------------------------------------------

        public ProjectEditorSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords) : base(path, scopes, keywords) { }

        public override void OnActivate(string searchContext, VisualElement rootElement) {

            var preferences = ProjectEditorSettingsSO.instance;

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
                ProjectEditorSettingsSO.instance.Save();
            }
        }
    }
}
#endif