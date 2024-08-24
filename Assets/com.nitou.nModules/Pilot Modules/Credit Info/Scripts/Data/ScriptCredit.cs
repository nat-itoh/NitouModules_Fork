using UnityEngine;
using Sirenix.OdinInspector;

// [�Q�l]
//  _: �Ƃقق̃��C�Z���X���� https://www.tohoho-web.com/ex/license.html 

namespace nitou.Credit {

    [CreateAssetMenu(
        fileName = "Credit_Script",
        menuName = AssetMenu.Prefix.CreditInfo + "Script"
    )]
    public class ScriptCredit : CreditData {

        public enum LicenseFormat {
            MIT,
            BSD,
            Apache,
        }

        /// <summary>
        /// ���C�Z���X�`��
        /// </summary>
        [TitleGroup(CreditUtility.BasicGroup), Indent]
        public LicenseFormat license = LicenseFormat.MIT;

        /// <summary>
        /// ���s�N
        /// </summary>
        [TitleGroup(CreditUtility.BasicGroup), Indent]
        public int publicationYear = 2000;

        /// <summary>
        /// �����
        /// </summary>
        [TitleGroup(CreditUtility.BasicGroup), Indent]
        public string author = "";


        /// <summary>
        /// �^�C�v
        /// </summary>
        public override CreditType Type => CreditType.Script;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �\���e�L�X�g�֕ϊ�����
        /// </summary>
        public override string ToString() {
            return $"<b>{englishName}</b> / (c) {publicationYear} {author}\n" 
                + $"Released under the {license} license\n"
                + url;
        }
    }
}
