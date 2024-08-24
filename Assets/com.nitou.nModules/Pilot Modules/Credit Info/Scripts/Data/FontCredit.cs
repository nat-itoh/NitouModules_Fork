using UnityEngine;
using Sirenix.OdinInspector;

namespace nitou.Credit {

    [CreateAssetMenu(
        fileName = "Credit_Font", 
        menuName = AssetMenu.Prefix.CreditInfo + "Font"
    )]
    public class FontCredit : CreditData {

        /// <summary>
        /// �����Җ�
        /// </summary>
        [TitleGroup(CreditUtility.AdditionalGroup), Indent]
        public string authorName;


        /// <summary>
        /// �^�C�v
        /// </summary>
        public override CreditType Type => CreditType.Font;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �\���e�L�X�g�֕ϊ�����
        /// </summary>
        public override string ToString() {
            return $"<b>{japaneseName}</b>  by {authorName} �l\n" + url;
        }

    }

}