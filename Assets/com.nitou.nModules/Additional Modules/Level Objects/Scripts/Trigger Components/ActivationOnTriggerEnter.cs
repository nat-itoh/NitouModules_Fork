using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace nitou.LevelObjects{

    /// <summary>
    /// 
    /// </summary>
    public sealed class ActivationOnTriggerEnter : MonoBehaviour
    {
        [Title("Detection")]
        [SerializeField, Indent] string playerTag = "Player";
        
        [FoldoutGroup("Reaction")]
        [SerializeField, Indent] List<GameObject> _activatedObjList = new();

        [Space]

        [FoldoutGroup("Reaction")]
        [SerializeField, Indent] UnityEvent OnPlayerEnter;
        
        // ���������p
        private readonly ReactiveProperty<bool> _isActiveRP = new(false);



        /// ----------------------------------------------------------------------------
        // LifeCycle Events

        private void Awake() {
            _isActiveRP.Subscribe(isActive => ActivateTargets(isActive));
        }

        private void OnDestroy() {
            _isActiveRP.Dispose();
        }

        private void OnDisable() {
            _isActiveRP.Value = false;
        }

        /// <summary>
        /// �͈͓��ɓ������Ƃ��̏���
        /// </summary>
        private void OnTriggerEnter(Collider other) {

            if (other.CompareTag(playerTag)) {
                _isActiveRP.Value = true;
            }
        }

        /// <summary>
        /// �͈͊O�ɏo���Ƃ��̏���
        /// </summary>
        private void OnTriggerExit(Collider other) {

            if (other.CompareTag(playerTag)) {
                _isActiveRP.Value = false;
            }
        }


        /// ----------------------------------------------------------------------------
        // Private Methods

        private void ActivateTargets(bool isActive) {
            if (_activatedObjList.IsNullOrEmpty()) return;

            // �A�N�e�B�u�ݒ�
            _activatedObjList.WithoutNull()
                    .ForEach(o => o.SetActive(isActive));
        }
    }
}
