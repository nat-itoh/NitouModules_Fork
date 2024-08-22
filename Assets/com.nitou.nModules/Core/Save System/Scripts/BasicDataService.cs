using System;
using System.IO;
using System.Text;
using UnityEngine;

// [�Q�l]
//  Hatena: Json�t�@�C���𗘗p�����Z�[�u�@�\�̎��� https://kiironomidori.hatenablog.com/entry/unity_save_json

namespace nitou.SaveSystem {

    public class BasicDataService : DataServiceBase {

        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public BasicDataService(bool encrypted) : base(encrypted){}


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �f�[�^��ۑ�����
        /// </summary>
        protected override string ToJson<T>(T data) => JsonUtility.ToJson(data, true);

        /// <summary>
        /// �f�[�^��ǂݍ���
        /// </summary>
        protected override T FromJson<T>(string json) => JsonUtility.FromJson<T>(json);
    }
}
