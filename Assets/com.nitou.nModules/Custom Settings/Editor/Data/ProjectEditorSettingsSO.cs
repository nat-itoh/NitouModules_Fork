#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace nitou.EditorShared {
    using nitou.Inspector;

    /// <summary>
    /// Editor�ŎQ�Ƃ���v���W�F�N�g�ŗL�̐ݒ�f�[�^
    /// </summary>
    [UnityEditor.FilePath(
        "ProjectSettings/MyProjectEditorSettings.asset",
        UnityEditor.FilePathAttribute.Location.ProjectFolder
    )]
    public class ProjectEditorSettingsSO : ScriptableSingleton<ProjectEditorSettingsSO> {

        [Title("Hierarchy")]
        [SerializeField, Indent] HierarchySettings _hierarchySettings;

        [Title("Project Window")]
        [SerializeField, Indent] ProjectWindowSettings _projectWindowSettings;


        public HierarchySettings Hierarchy => _hierarchySettings;

        public void Save() => Save(true);
    }


    /// ----------------------------------------------------------------------------
    #region Hierarchy Settings

    /// <summary>
    /// �q�G�����L�[�Ɋւ���ݒ�f�[�^
    /// </summary>

    [Serializable]
    public class HierarchySettings {

        /// <summary>
        /// Specify how to handle HierarchyObject at runtime.
        /// </summary>
        public enum HierarchyObjectMode {
            /// <summary>
            /// �ʏ�I�u�W�F�N�g�Ƃ��Ĉ���
            /// </summary>
            None = 0,
            /// <summary>
            /// play mode�ō폜����
            /// </summary>
            RemoveInPlayMode = 1,
            /// <summary>
            /// �r���h���ɍ폜����
            /// </summary>
            RemoveInBuild = 2
        }

        //[LabelText("Dammy Object Mode")]
        [SerializeField] HierarchyObjectMode _hierarchyObjectMode = HierarchyObjectMode.RemoveInBuild;
        [SerializeField] bool _showToggles;
        [SerializeField] bool _showComponentIcons;

        // �v���p�e�B
        public HierarchyObjectMode Mode => _hierarchyObjectMode;
        public bool ShowToggles => _showToggles;
        public bool ShowComponentIcons => _showComponentIcons;
    }
    #endregion


    /// ----------------------------------------------------------------------------
    #region Project Window Settings

    [Serializable]
    public class ProjectWindowSettings {
        public int test;
    }
    #endregion

}
#endif