#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

// [�Q�l]
//  �R�K�l�u���O: Animator �� Inspector �� Animator �E�B���h�E���J���{�^����ǉ�����G�f�B�^�g�� https://baba-s.hatenablog.com/entry/2022/03/18/090000

namespace nitou.Tools {
    using nitou.EditorShared;

    /// <summary>
    /// Animator�̃C���X�y�N�^�[�g��
    /// </summary>
    [CustomEditor(typeof(Animator))]
    public sealed class AnimatorInspector : Editor {

        // �I���W�i���̊g���N���X
        private static readonly Type BASE_EDITOR_TYPE = typeof(Editor)
            .Assembly
            .GetType("UnityEditor.AnimatorInspector");


        /// <summary>
        /// �C���X�y�N�^�`��
        /// </summary>
        public override void OnInspectorGUI() {
            var animator = (Animator)target;

            // �g�����̃C���X�y�N�^�[�\��
            using (new EditorGUILayout.HorizontalScope()) {
                if (GUILayout.Button("Animator Window")) {
                    EditorApplication.ExecuteMenuItem("Window/Animation/Animator");
                }
                if (GUILayout.Button("Animation Window")) {
                    EditorApplication.ExecuteMenuItem("Window/Animation/Animation");
                }
            }
            EditorGUILayout.Space();

            // �I���W�i���̃C���X�y�N�^�[�\��
            var editor = CreateEditor(animator, BASE_EDITOR_TYPE);
            editor.OnInspectorGUI();
        }
    }
}

#endif