using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace nitou.Credit {

    [InlineEditor]
    public abstract class CreditData : ScriptableObject {

        /// ----------------------------------------------------------------------------
        // Field
        
        
        /// <summary>
        /// �v���W�F�N�g�Ŏg�p���Ă��邩�ǂ���
        /// </summary>
        [TitleGroup(CreditUtility.State), Indent]
        public bool isUsed;

        /// <summary>
        /// �R���e���c��
        /// </summary>
        [LabelText("Name")]
        [TitleGroup(CreditUtility.BasicGroup), Indent]
        [OnValueChanged("OnNameChange")]
        public string englishName = "unknown";

        /// <summary>
        /// URL
        /// </summary>
        [TitleGroup(CreditUtility.BasicGroup), Indent]
        public string url;

        /// <summary>
        /// �R���e���c��
        /// </summary>
        [TitleGroup(CreditUtility.AdditionalGroup), Indent]
        public string japaneseName;

        /// <summary>
        /// ��������
        /// </summary>
        [HideLabel]
        [TextArea(4,10)]
        [TitleGroup("Memo")]
        public string description;


        /// ----------------------------------------------------------------------------
        // Properity

        /// <summary>
        /// �^�C�v
        /// </summary>
        public abstract CreditType Type { get; }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �\���e�L�X�g�֕ϊ�����
        /// </summary>
        public override string ToString() {
            return $"<b>{japaneseName}</b>\n" + url;
        }


        /// ----------------------------------------------------------------------------
#if UNITY_EDITOR
        private void OnNameChange() {
            if (englishName.IsNullOrWhiteSpace()) {
                englishName = "unknown";
            }

            // �t�@�C�������X�V
            var path = AssetDatabase.GetAssetPath(this);
            AssetDatabase.RenameAsset(path, $"Credit_{Type.ToString()}_{englishName}");
        }
#endif
    }

}