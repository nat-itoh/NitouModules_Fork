using UnityEngine;

namespace nitou.FlagManagement{

    public interface IFlagMasterTable {

        /// <summary>
        /// �e�[�u��������������
        /// </summary>
        void Initialize();

        /// <summary>
        /// �e�[�u������ID��������
        /// </summary>
        FlagEntity FindById(string id);
    }
}
