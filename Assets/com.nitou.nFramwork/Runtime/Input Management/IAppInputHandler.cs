using UnityEngine;

namespace nitou.GameSystem{

    /// <summary>
    /// �A�v���P�[�V�����̓��͂������C���^�[�t�F�[�X
    /// </summary>
    public interface IAppInputHandler : System.IDisposable{

        // Player
        public void EnablePlayer();
        public void DisablePlayer();

        // UI
        public void EnableUI();
        public void DisableUI();

        public void DisableAll();
    }
}
