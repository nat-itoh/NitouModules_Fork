using System;

namespace nitou.Credit {

    /// <summary>
    /// �N���W�b�g�̎��
    /// </summary>
    public enum CreditType {
        
        /// <summary>
        /// �t�H���g
        /// </summary>
        Font,

        /// <summary>
        /// �摜
        /// </summary>
        Image,

        /// <summary>
        /// �T�E���h
        /// </summary>
        Sound,

        /// <summary>
        /// ���f��
        /// </summary>
        Model,

        /// <summary>
        /// �X�N���v�g�iOSS�j
        /// </summary>
        Script,    
    }


    public static class CreditTypeUtility {

        /// <summary>
        /// �w�肵���N���W�b�g�̌^�����擾����
        /// </summary>
        public static Type GetClassType(this CreditType type) {
            return type switch {
                CreditType.Font => typeof(FontCredit),
                CreditType.Image => typeof(ImageCredit),
                CreditType.Sound => typeof(SoundCredit),
                CreditType.Model => typeof(ModelCredit),
                CreditType.Script => typeof(ScriptCredit),
                _ => throw new NotImplementedException()
            };

        }
    }

}