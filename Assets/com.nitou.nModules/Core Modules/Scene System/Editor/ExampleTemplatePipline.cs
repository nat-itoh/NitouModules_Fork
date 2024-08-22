#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneTemplate;

// [�Q�l]
//  LIGHT11: �V�[���̐��`������Scene Template�@�\�̎g�����܂Ƃ� https://light11.hatenadiary.com/entry/2022/06/08/193509

namespace nitou.SceneSystem.EditorScripts {

    public class ExampleTemplatePipline : ISceneTemplatePipeline {

        /// <summary>
        /// �L���ȃe���v���[�g�����肷��
        /// </summary>
        public bool IsValidTemplateForInstantiation(SceneTemplateAsset sceneTemplateAsset) {
            Debug_.Log($"{nameof(IsValidTemplateForInstantiation)} - sceneTemplateAsset: {sceneTemplateAsset.name}");
            return true;
        }

        /// <summary>
        /// �V�[�����쐬���ꂽ�O�̃R�[���o�b�N
        /// </summary>
        public void BeforeTemplateInstantiation(SceneTemplateAsset sceneTemplateAsset, bool isAdditive, string sceneName) {
            Debug.Log($"{nameof(BeforeTemplateInstantiation)} - isAdditive: {isAdditive} sceneName: {sceneName}");
        }

        /// <summary>
        /// �V�[�����쐬������̃R�[���o�b�N
        /// </summary>
        public void AfterTemplateInstantiation(SceneTemplateAsset sceneTemplateAsset, Scene scene, bool isAdditive, string sceneName) {
            Debug.Log($"{nameof(AfterTemplateInstantiation)} - scene: {scene} isAdditive: {isAdditive} sceneName: {sceneName}");
        }
    }
}

#endif