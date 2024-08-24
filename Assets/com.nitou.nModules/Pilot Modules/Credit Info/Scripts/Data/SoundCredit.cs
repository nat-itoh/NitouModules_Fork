using UnityEngine;

namespace nitou.Credit{

    [CreateAssetMenu(
        fileName = "Credit_Sound", 
        menuName = AssetMenu.Prefix.CreditInfo + "Sound"
    )]
    public class SoundCredit : CreditData{

        /// ----------------------------------------------------------------------------
        // Properity

        /// <summary>
        /// �^�C�v
        /// </summary>
        public override CreditType Type => CreditType.Sound;

    }
}
