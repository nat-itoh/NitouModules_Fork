namespace nitou.SaveSystem{

    /// <summary>
    /// �f�[�^�̕ۑ��E�ǂݍ��݂̎�������S���C���^�[�t�F�[�X
    /// </summary>
    public interface IDataService{

        /// <summary>
        /// �f�[�^��ۑ�����
        /// </summary>
        bool SaveData<T>(string key, T data);

        /// <summary>
        /// �f�[�^��ǂݍ���
        /// </summary>
        T LoadData<T>(string key);
    }
}
