using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Sirenix.OdinInspector;
using nitou.BachProcessor;

namespace nitou.Detecor{

    /// <summary>
    /// �������o�p�R���|�[�l���g�̊��N���X�D
    /// </summary>
    public abstract class DetectorBase : ComponentBase{

        // ���s�^�C�~���O
        [DisableInPlayMode]
        [EnumToggleButtons, HideLabel]
        [SerializeField, Indent] protected UpdateTiming Timing = UpdateTiming.Update;

        [Title("Hit Settings")]

        // �R���C�_�[�ۗL�I�u�W�F�N�g�̔���
        [DisableInPlayMode]
        [SerializeField, Indent] protected CachingTarget _cacheTargetType = CachingTarget.Collider;

        // �Ώۃ��C���[
        [DisableInPlayMode]
        [SerializeField, Indent] protected LayerMask _hitLayer;

        // �^�O�����L��
        [DisableInPlayMode]
        [SerializeField, Indent] protected bool _useHitTag = false;

        // �Ώۃ^�O
        [ShowIf("@_useHitTag")]
        [DisableInPlayMode]
        [SerializeField, Indent] protected string[] _hitTagArray;


        /// ----------------------------------------------------------------------------
#if UNITY_EDITOR
        protected virtual void Reset() {
            // ���背�C���[��"Default"
            _hitLayer = LayerMaskUtil.OnlyDefault();
        }
#endif
    }
}
