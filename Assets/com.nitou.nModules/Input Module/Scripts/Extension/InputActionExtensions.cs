using System;
using UniRx;

// [�Q�l]
//  qiita: Unity�̐V����InputSystem��ReactiveProperty�ɕϊ����g�p���� https://qiita.com/Yuzu_Unity/items/1d0b39d17888fda552bd

namespace UnityEngine.InputSystem {

    /// <summary>
    /// InputAction�̊�{�I�Ȋg�����\�b�h�W
    /// </summary>
    public static class InputActionExtensions {

        /// ----------------------------------------------------------------------------
        #region As Observable

        /// <summary>
        /// "performed"�C�x���g��Observable�ɕϊ�����g�����\�b�h
        /// </summary>
        public static IObservable<InputAction.CallbackContext> PerformedAsObservable(this InputAction inputAction) {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => inputAction.performed += h,
                h => inputAction.performed -= h);
        }

        /// <summary>
        /// "canceled"�C�x���g��Observable�ɕϊ�����g�����\�b�h
        /// </summary>
        public static IObservable<InputAction.CallbackContext> CanceledAsObservable(this InputAction inputAction) {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => inputAction.canceled += h,
                h => inputAction.canceled -= h);
        }

        /// <summary>
        /// "started"�C�x���g��Observable�ɕϊ�����g�����\�b�h
        /// </summary>
        public static IObservable<InputAction.CallbackContext> StartedAsObservable(this InputAction inputAction) {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h => inputAction.started += h,
                h => inputAction.started -= h);
        }
        #endregion


        // ------------

        /// <summary>
        /// ReactiveProperty�ɕϊ�����g�����\�b�h
        /// </summary>
        public static ReadOnlyReactiveProperty<bool> GetButtonProperty(this InputAction inputAction) {

            return Observable.FromEvent<InputAction.CallbackContext>(
                    h => inputAction.performed += h,
                    h => inputAction.performed -= h)
                .Select(x => x.ReadValueAsButton())
                .ToReadOnlyReactiveProperty(false);
        }

        /// <summary>
        /// ReactiveProperty�ɕϊ�����g�����\�b�h (��Axis���͂���0�����������Ȃ�)
        /// </summary>
        public static ReadOnlyReactiveProperty<float> GetAxisProperty(this InputAction inputAction) {

            return Observable.FromEvent<InputAction.CallbackContext>(
                    h => inputAction.performed += h,
                    h => inputAction.performed -= h)
                .Select(x => x.ReadValue<float>())
                .ToReadOnlyReactiveProperty(0);
        }

        /// <summary>
        /// ReactiveProperty�ɕϊ�����g�����\�b�h (����Ƀ}�E�X�Ŏg�p)
        /// </summary>
        public static ReadOnlyReactiveProperty<float> GetDeltaAxisProperty(this InputAction inputAction) {

            // ��Delta���͂�Update��Ȃ̂ŕϊ�
            return Observable.EveryUpdate()
                .Select(_ => inputAction.ReadValue<float>())
                .ToReadOnlyReactiveProperty(0);
        }



    }
}