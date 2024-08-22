#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

// [�Q�l]
//  �V�[�����ȒP�ɐ؂�ւ���G�f�B�^�g�� https://tyfkda.github.io/blog/2021/07/15/unity-scene-switcher.html
//  �V�[����؂�ւ���{�^����\������G�f�B�^�g�� https://kyoro-s.com/unity-13/
//  Unity���f�[�^��ۑ����邽�߂Ɏg���p�X�ɂ��� https://light11.hatenadiary.com/entry/2019/10/07/031405

namespace nitou.SceneSystem.EditorScripts {

    /// <summary>
    /// �ҏW���̃V�[���؂�ւ���e�Ղɂ��邽�߂̃E�C���h�E
    /// </summary>
    public class SceneSwitcherWindow : EditorWindow {

        // [NOTE]
        //  ���}���`�V�[���G�f�B�e�B���O�͑Ή����Ă��Ȃ�

        private List<SceneAsset> _scenes;

        // �`��p
        private Vector2 _scrollPos;

        // �f�[�^�ۑ���
        private static string FilePath => $"{Application.persistentDataPath}/_sceneLauncher.sav";


        /// ----------------------------------------------------------------------------
        // EditorWindow Method

        [MenuItem(
            MenuItemName.Prefix.EditorWindow + "Scene Switcher",
            priority = -1001
        )]
        static void Open() {
            GetWindow<SceneSwitcherWindow>("Scene Switcher");
        }

        private void OnEnable() {
            if (_scenes == null) {
                _scenes = new List<SceneAsset>();
                Load();
            }
        }

        private void OnGUI() {
            DrawHeader();
            GuiLine();
            DrawContents();
        }


        /// ----------------------------------------------------------------------------
        // Private Method (Drawing)

        private void DrawHeader() {
            using (new EditorGUILayout.HorizontalScope(GUI.skin.box)) {

                // �w��V�[���̒ǉ�
                var sceneAsset = EditorGUILayout.ObjectField(null, typeof(SceneAsset), false) as SceneAsset;
                if (sceneAsset != null && !_scenes.Contains(sceneAsset)) {
                    _scenes.Add(sceneAsset);
                    Save();
                }

                // ���݃V�[���̒ǉ�
                if (GUILayout.Button("Add current scene")) {
                    var scene = EditorSceneManager.GetActiveScene();
                    if (scene != null && scene.path != null &&
                        _scenes.Find(s => AssetDatabase.GetAssetPath(s) == scene.path) == null) {

                        // �V�[���A�Z�b�g���擾
                        var asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
                        if (asset != null && !_scenes.Contains(asset)) {
                            _scenes.Add(asset);
                            Save();
                        }
                    }
                }
            }
        }

        private void DrawContents() {

            using var scrollView = new EditorGUILayout.ScrollViewScope(_scrollPos); _scrollPos = scrollView.scrollPosition;

            // �V�[�����X�g�̕\��
            for (var i = 0; i < _scenes.Count; ++i) {

                var scene = _scenes[i];
                using (new EditorGUILayout.HorizontalScope()) {
                    
                    var path = AssetDatabase.GetAssetPath(scene);
                    if (GUILayout.Button("X", GUILayout.Width(20))) {
                        _scenes.Remove(scene);
                        Save();
                        --i;
                    } else {
                        if (GUILayout.Button("O", GUILayout.Width(20))) {
                            EditorGUIUtility.PingObject(scene);
                        }
                        if (GUILayout.Button(i > 0 ? "��" : "�@", GUILayout.Width(20)) && i > 0) {
                            _scenes[i] = _scenes[i - 1];
                            _scenes[i - 1] = scene;
                            Save();
                        }

                        // �V�[���{�^��
                        EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
                        if (GUILayout.Button(Path.GetFileNameWithoutExtension(path))) {
                            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {  // ���ύX���������ꍇ�ɕۑ����邩�̊m�F�p
                                EditorSceneManager.OpenScene(path);
                            }
                        }
                        EditorGUI.EndDisabledGroup();
                    }
                }
            }
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// �ݒ�f�[�^�̕ۑ�
        /// </summary>
        private void Save() {
            var guids = new List<string>();
            foreach (var scene in _scenes) {
                if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(scene, out string guid, out long _)) {
                    guids.Add(guid);
                }
            }

            var content = string.Join("\n", guids.ToArray());
            File.WriteAllText(FilePath, content);
        }

        /// <summary>
        /// �ݒ�f�[�^�̓ǂݍ���
        /// </summary>
        private void Load() {
            _scenes.Clear();
            if (File.Exists(FilePath)) {
                string content = File.ReadAllText(FilePath);
                foreach (var guid in content.Split(new char[] { '\n' })) {
                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
                    if (scene != null)
                        _scenes.Add(scene);
                }
            }
        }


        /// ----------------------------------------------------------------------------
        // Util Method

        private static void GuiLine(int height = 1) {
            GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(height) });
        }
    }

}
#endif