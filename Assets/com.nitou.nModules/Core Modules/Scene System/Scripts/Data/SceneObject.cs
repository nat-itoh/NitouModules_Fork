using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// [�Q�l]
// �@�R�K�l�u���O: Inspector �ŕϐ��ɃV�[���t�@�C����ݒ�ł���悤�ɂ���uSceneObject�v https://baba-s.hatenablog.com/entry/2017/11/14/110000

namespace nitou.SceneSystem {

    /// <summary>
    /// �C���X�y�N�^�[�ŃV�[���t�@�C����ݒ�ł���悤�ɂ��邽�߂̃N���X
    /// </summary>
    [Serializable]
    public sealed class SceneObject {

        [SerializeField] string _sceneName;

        // �ϊ�
        public static implicit operator string(SceneObject sceneObject) {
            return sceneObject._sceneName;
        }
        public static implicit operator SceneObject(string sceneName) {
            return new SceneObject() { _sceneName = sceneName };
        }
    }
}


/// ----------------------------------------------------------------------------
#if UNITY_EDITOR
namespace nitou.SceneSystem.EditorScripts {

    [CustomPropertyDrawer(typeof(SceneObject))]
    internal class SceneObjectEditor : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            var sceneObj = GetSceneObject(property.FindPropertyRelative("_sceneName").stringValue);
            var newScene = EditorGUI.ObjectField(position, label, sceneObj, typeof(SceneAsset), false);
            if (newScene == null) {
                var prop = property.FindPropertyRelative("_sceneName");
                prop.stringValue = "";
            } else {
                if (newScene.name != property.FindPropertyRelative("_sceneName").stringValue) {
                    var scnObj = GetSceneObject(newScene.name);
                    if (scnObj == null) {
                        Debug.LogWarning("The scene " + newScene.name + " cannot be used. To use this scene add it to the build settings for the project.");
                    } else {
                        var prop = property.FindPropertyRelative("_sceneName");
                        prop.stringValue = newScene.name;
                    }
                }
            }
        }

        /// <summary>
        /// �Ώۂ̃V�[���A�Z�b�g���擾����
        /// </summary>
        protected SceneAsset GetSceneObject(string sceneObjectName) {
            if (string.IsNullOrEmpty(sceneObjectName)) return null;

            // BuildSettings�Ɋ܂܂��V�[�����猟��
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++) {
                EditorBuildSettingsScene scene = EditorBuildSettings.scenes[i];
                if (scene.path.IndexOf(sceneObjectName) != -1) {
                    return AssetDatabase.LoadAssetAtPath(scene.path, typeof(SceneAsset)) as SceneAsset;
                }
            }

            Debug.Log("Scene [" + sceneObjectName + "] cannot be used. Add this scene to the 'Scenes in the Build' in the build settings.");
            return null;
        }
    }

}
#endif