using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace nitou.GameSystem {

    public class AppInputHandler : IAppInputHandler{

        private readonly InputActionAsset _uiInput;
        private readonly InputActionAsset _playerInput;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public AppInputHandler(InputActionAsset uiInPut, InputActionAsset  playerInput) {
            _uiInput = uiInPut;
            _playerInput = playerInput;
            
            DisableAll();
        }
        
        /// <summary>
        /// �I������
        /// </summary>
        public void Dispose() {
            DisableAll();
        }


        /// ----------------------------------------------------------------------------
        // Public Method (�A�N�e�B�u�ݒ�)

        /// <summary>
        /// UI�����L���ɂ���
        /// </summary>
        public void EnableUI() {
            _uiInput.Enable();
        }

        /// <summary>
        /// �v���C���[�����L���ɂ���
        /// </summary>
        public void EnablePlayer() {
            _playerInput.Enable();
        }

        /// <summary>
        /// UI����𖳌��ɂ���
        /// </summary>
        public void DisableUI() {
            _uiInput.Disable();
        }

        /// <summary>
        /// �v���C���[����𖳌��ɂ���
        /// </summary>
        public void DisablePlayer() {
            _playerInput.Disable();
        }

        /// <summary>
        /// ���͑���𖳌��ɂ���
        /// </summary>
        public void DisableAll() {
            DisableUI();
            DisablePlayer();
        }
    }
}
