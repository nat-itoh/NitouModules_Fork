#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System.Text;

// [�Q�l]
//  youtube: Custom Editor Windows Made Easy with Odin Inspector https://www.youtube.com/watch?v=erWEG-6hx7g

namespace nitou.Credit.Editor {
    using nitou.EditorShared;

    public class CreditInfoEditorWindow : OdinMenuEditorWindow {

        /// ----------------------------------------------------------------------------
        // Field & Properity

        private CreateNewCreditData _createNewCreditData;
        private CreditTextCreater _creditTextCreater;

        private string RootFolderPath => PathUtil.Combine(this.GetAssetParentFolderPath(2), "Scriptable Objects");


        /// ----------------------------------------------------------------------------
        // Editor Method

        [MenuItem( ToolBarMenu.Prefix.EditorWindow + "Credit Info")]
        private static void OpenWindow() => GetWindow<CreditInfoEditorWindow>().Show();

        protected override void OnDestroy() {
            base.OnDestroy();

            if (_createNewCreditData != null) {
                DestroyImmediate(_createNewCreditData.data);
            }
        }

        /// <summary>
        /// �T�C�h���j���[�̍\��
        /// </summary>
        protected override OdinMenuTree BuildMenuTree() {

            var tree = new OdinMenuTree();

            // �V�K�f�[�^�̍쐬
            _createNewCreditData = new CreateNewCreditData(this);
            tree.Add("Add New Data", _createNewCreditData, SdfIconType.PlusSquareFill);

            // �f�[�^�ꗗ�̕\��
            tree.AddAllAssetsAtPath("Credit Data", RootFolderPath, typeof(CreditData), includeSubDirectories: true);

            //
            _creditTextCreater = new CreditTextCreater(this);
            tree.Add("Convert to Text", _creditTextCreater, SdfIconType.ChatSquareTextFill);

            return tree;
        }

        /// <summary>
        /// GUI�`��̒�`
        /// </summary>
        protected override void OnBeginDrawEditors() {
            if (MenuTree == null) return;
            OdinMenuTreeSelection selected = this.MenuTree.Selection;

            // �c�[���o�[�̕`��
            SirenixEditorGUI.BeginHorizontalToolbar();
            {
                GUILayout.FlexibleSpace();

                // �폜�{�^��
                if (SirenixEditorGUI.ToolbarButton("Delete Current")) {
                    CreditData asset = selected.SelectedValue as CreditData;
                    string path = AssetDatabase.GetAssetPath(asset);
                    AssetDatabase.DeleteAsset(path);
                    AssetDatabase.SaveAssets();
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// �N���W�b�g�̎�ނ��Ƃ̐e�t�H���_�����擾����
        /// </summary>
        private static string GetCreditDataFolderName(CreditType type) =>
            type switch {
                CreditType.Font => "Font Credit",
                CreditType.Image => "Image Credit",
                CreditType.Model => "Model Credit",
                CreditType.Sound => "Sound Credit",
                CreditType.Script => "Script Credit",
                _ => throw new NotImplementedException(),
            };


        // ----------------------------------------------------------------------------
        #region TreeMenue    

        /// <summary>
        /// �V�K�f�[�^���쐬���郁�j���[
        /// </summary>
        public class CreateNewCreditData {

            /// ----------------------------------------------------------------------------
            // Field & Properity

            private readonly CreditInfoEditorWindow context;

            // �N���W�b�g�̎��            
            [OnValueChanged("CreateInstance")]
            [BoxGroup] public CreditType creditType;

            [Space(20)]

            [InlineEditor(objectFieldMode: InlineEditorObjectFieldModes.Hidden)]
            public CreditData data;


            /// ----------------------------------------------------------------------------
            // Public Method

            /// <summary>
            /// �R���X�g���N�^
            /// </summary>
            public CreateNewCreditData(CreditInfoEditorWindow context) {
                this.context = context;
                CreateInstance();
            }


            /// ----------------------------------------------------------------------------
            // Private Method

            /// <summary>
            /// �w�肵����ނ̃N���W�b�g�f�[�^�𐶐�����
            /// </summary>
            private void CreateInstance() {
                //Debug.Log("Create data");

                var classType = creditType.GetClassType();
                var newData = ScriptableObject.CreateInstance(classType) as CreditData;

                if (data != null) {
                    newData.englishName = data.englishName;
                    newData.url = data.url;
                    newData.isUsed = data.isUsed;
                    newData.description = data.description;
                }

                data = newData;
            }

            /// <summary>
            /// �A�Z�b�g�Ƃ��ĐV�K�f�[�^��ۑ�����
            /// </summary>
            [Button("Add New Credit Data")]
            private void SaveNewData() {
                var assetName = $"{data.Type}_{data.englishName}.asset";
                var assetPath = PathUtil.Combine(
                    context.RootFolderPath,
                    GetCreditDataFolderName(creditType),
                    assetName
                    );

                if (Directory.Exists(assetPath)) {
                    Debug_.LogWarning($"�w�肵���t�@�C����{assetName}�́C���ɑ��݂��܂�.");
                    return;
                }

                AssetDatabase.CreateAsset(data, assetPath);
                AssetDatabase.SaveAssets();
                data = null;
                CreateInstance();
            }

        }


        /// <summary>
        /// �N���W�b�g�f�[�^����\���e�L�X�g���쐬���郁�j���[
        /// </summary>
        public class CreditTextCreater {

            /// ----------------------------------------------------------------------------
            // Field & Properity

            private readonly CreditInfoEditorWindow context;

            public bool useFontCredit;
            public bool useImageCredit;
            public bool useModelCredit;
            public bool useSoundCredit;
            public bool useScriptCredit;

            [TextArea(5, 20), HideLabel]
            public string message;


            /// ----------------------------------------------------------------------------
            // Public Method

            /// <summary>
            /// �R���X�g���N�^
            /// </summary>
            public CreditTextCreater(CreditInfoEditorWindow context) {
                this.context = context;
            }

            /// <summary>
            /// 
            /// </summary>
            [Button]
            public void UpdateText() {

                var sb = new StringBuilder();

                if (useFontCredit) {
                    sb.Append("========================\n");
                    sb.Append(" Font \n");
                    sb.Append("========================\n\n");

                    var datalist = LoadCreditData(CreditType.Font);
                    sb.Append(datalist.Convert());
                }

                if (useImageCredit) {
                    sb.Append("========================\n");
                    sb.Append(" Image \n");
                    sb.Append("========================\n\n");

                    var datalist = LoadCreditData(CreditType.Image);
                    sb.Append(datalist.Convert());
                }


                if (useSoundCredit) {
                    sb.Append("========================\n");
                    sb.Append(" Sound \n");
                    sb.Append("========================\n\n");

                    var datalist = LoadCreditData(CreditType.Sound);
                    sb.Append(datalist.Convert());
                }

                if (useScriptCredit) {
                    sb.Append("========================\n");
                    sb.Append(" Script \n");
                    sb.Append("========================\n\n");

                    var datalist = LoadCreditData(CreditType.Script);
                    sb.Append(datalist.Convert());
                }


                // 
                message = sb.ToString();
            }

            /// ----------------------------------------------------------------------------
            // Private Method

            /// <summary>
            /// �w�肵����ނ̃N���W�b�g�f�[�^��ǂݍ���
            /// </summary>
            private IReadOnlyList<CreditData> LoadCreditData(CreditType type) {

                var folderPath = PathUtil.Combine(
                    context.RootFolderPath,
                    GetCreditDataFolderName(type)
                    );

                var assets = NonResources.LoadAll<CreditData>(folderPath)
                    .Where(x => x.isUsed)
                    .OrderBy(x => x.englishName)
                    .ToList();
                return assets;
            }



        }

        #endregion

    }
}

#endif