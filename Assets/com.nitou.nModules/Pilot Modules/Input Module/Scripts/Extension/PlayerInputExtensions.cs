using System;
using UniRx;

// [�Q�l]
//  github: AnnulusGames/ReactiveInputSystem https://github.com/AnnulusGames/ReactiveInputSystem/blob/main/README_JA.md

namespace UnityEngine.InputSystem {

    /// <summary>
    /// PlayerAction�̊�{�I�Ȋg�����\�b�h�W
    /// </summary>
    public static class PlayerInputExtensions {

        /// ----------------------------------------------------------------------------
        #region As Observable

        /// <summary>
        /// "onActionTriggerd"�C�x���g��Observable�ɕϊ�����g�����\�b�h
        /// </summary>
        public static IObservable<InputAction.CallbackContext> OnActionTriggeredAsObservable(this PlayerInput playerInput) {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => playerInput.onActionTriggered += h,
                h => playerInput.onActionTriggered -= h);
        }

        /// <summary>
        /// "onControlsChanged"�C�x���g��Observable�ɕϊ�����g�����\�b�h
        /// </summary>
        public static IObservable<PlayerInput> OnControlsChangedAsObservable(this PlayerInput playerInput) {
            return Observable.FromEvent<PlayerInput>(
                h => playerInput.onControlsChanged += h,
                h => playerInput.onControlsChanged -= h);
        }

        /// <summary>
        /// "onDeviceLost"�C�x���g��Observable�ɕϊ�����g�����\�b�h
        /// </summary>
        public static IObservable<PlayerInput> OnDeviceLostAsObservable(this PlayerInput playerInput) {
            return Observable.FromEvent<PlayerInput>(
                h => playerInput.onDeviceLost += h,
                h => playerInput.onDeviceLost -= h);
        }

        /// <summary>
        /// "onDeviceRegained"�C�x���g��Observable�ɕϊ�����g�����\�b�h
        /// </summary>
        public static IObservable<PlayerInput> OnDeviceRegainedAsObservable(this PlayerInput playerInput) {
            return Observable.FromEvent<PlayerInput>(
                h => playerInput.onDeviceRegained += h,
                h => playerInput.onDeviceRegained -= h);
        }
        #endregion

    }

}