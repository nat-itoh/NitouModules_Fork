using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace nitou {

    /// <summary>
    /// <see cref="Scene"/>��ΏۂƂ����g�����\�b�h�W
    /// </summary>
    public static class SceneExtensions {

        /// ----------------------------------------------------------------------------
        // Public Methord

        /// <summary>
        /// �w��V�[�����̃��[�g����R���|�[�l���g���擾����
        /// </summary>
        public static bool TryGetComponentInSceneRoot<T>(this Scene scene, out T result) {

            if (!scene.IsValid()) {
                throw new ArgumentException("Scene is invalid.", nameof(scene));
            }

            // �V�[�����̃��[�g�I�u�W�F�N�g�����Ƀ`�F�b�N����
            foreach (GameObject rootObj in scene.GetRootGameObjects()) {
                if (rootObj.TryGetComponent(out result)) {
                    return true;
                }
            }

            // ��������Ȃ������ꍇ�C
            result = default;
            return false;
        }

        /// <summary>
        /// �w��V�[�����̃R���|�[�l���g���擾����
        /// </summary>
        public static bool TryGetComponentInScene<T>(this Scene scene, out T result, bool includeInactive = true) {

            if (!scene.IsValid()) {
                throw new ArgumentException("Scene is invalid.", nameof(scene));
            }

            // �V�[�����̃��[�g�I�u�W�F�N�g�����Ƀ`�F�b�N����
            foreach (GameObject rootObj in scene.GetRootGameObjects()) {
                result = rootObj.GetComponentInChildren<T>(includeInactive);
                if (result != null) {
                    return true;
                }
            }

            // ��������Ȃ������ꍇ�C
            result = default;
            return false;
        }

        /// <summary>
        /// �w��V�[�����̃R���|�[�l���g���擾����
        /// </summary>
        public static T GetComponentInScene<T>(this Scene scene, bool includeInactive = true) {
            return TryGetComponentInScene(scene, out T result, includeInactive)
                ? result
                : throw new InvalidOperationException($"Component of type '{typeof(T).Name}' is not found in scene '{scene.name}'.");
        }

        /// <summary>
        /// �w��V�[�����̃R���|�[�l���g���擾����
        /// </summary>
        public static T GetComponentInSceneOrDefault<T>(this Scene scene, bool includeInactive = true) {
            TryGetComponentInScene(scene, out T result, includeInactive);
            return result;
        }

    }
}
