using System;
using System.IO;
using System.Text;
using UnityEngine;

// [�Q�l]
//  youtube: �f�[�^�̉i���� - �悭����ԈႢ��������Ȃ���Q�[���̏�Ԃ�ۑ�����у��[�h | Unity �`���[�g���A�� https://www.youtube.com/watch?v=mntS45g8OK4&t=240s
//  qiita: ����������Unity�p�Z�[�u�f�[�^�Ǘ��N���X https://qiita.com/tocoteron/items/b865edaa0e3018cb5e55

namespace nitou.SaveSystem {
    using nitou.SaveSystem.Utils;

    /// <summary>
    /// 
    /// </summary>
    public abstract class DataServiceBase : IDataService {

        // �p�X
        private static readonly string fullPath = $"{ Application.persistentDataPath }";
        private static readonly string extension = "dat";

        // �Í���
        public readonly bool encrypted;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public DataServiceBase(bool encrypted) {
            this.encrypted = encrypted;
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �w�肵���p�X�Ƀf�[�^��ۑ�����
        /// </summary>
        public bool SaveData<T>(string key, T data) {

            string filePath = $"{ fullPath }/{ key }.{ extension }";
            try {
                if (File.Exists(filePath)) {
                    Debug.Log("Save data exists. Deleting old file and weiting a new one!");
                    File.Delete(filePath);
                }

                // �f�[�^�̕ۑ�
                if (encrypted) {
                    string json = ToJson<T>(data);

                    byte[] byteData = Encoding.UTF8.GetBytes(json);
                    byteData = Compressor.Compress(byteData);
                    byteData = Cryptor.Encrypt(byteData);

                    using (FileStream fileStream = File.Create(filePath)) {
                        fileStream.Write(byteData, 0, byteData.Length);
                    }
                } else {
                    File.WriteAllText(filePath, ToJson<T>(data));
                }
                return true;

            } catch (Exception e) {
                Debug_.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// �w�肵���p�X����f�[�^��ǂݍ���
        /// </summary>
        public T LoadData<T>(string key) {

            string filePath = $"{ fullPath }/{ key }.{ extension }";
            if (!File.Exists(filePath)) {
                Debug_.LogError($"Cannot load file at {filePath}. File does not exist!");
                throw new FileNotFoundException($"{filePath} dose not exist!");
            }

            try {
                // �f�[�^�̓ǂݍ���
                T data = default;
                if (encrypted) {
                    byte[] byteData = null;
                    using (FileStream fileStream = File.OpenRead(filePath)) {
                        byteData = new byte[fileStream.Length];
                        fileStream.Read(byteData, 0, byteData.Length);
                    }

                    byteData = Cryptor.Decrypt(byteData);
                    byteData = Compressor.Decompress(byteData);

                    string json = Encoding.UTF8.GetString(byteData);
                    data = FromJson<T>(json);
                } else {
                    data = FromJson<T>(File.ReadAllText(filePath));
                }
                return data;

            } catch (Exception e) {
                // �t�@�C�����ǂݎ��Ȃ�/�`�����قȂ�ꍇ,
                Debug_.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
                throw e;
            }

        }


        /// ----------------------------------------------------------------------------
        // Protected Method (�ϊ����\�b�h)

        protected abstract string ToJson<T>(T data);
        protected abstract T FromJson<T>(string json);
    }
}
