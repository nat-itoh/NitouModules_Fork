using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Sirenix.OdinInspector;
using nitou.BachProcessor;

// [�Q�l] 
//  qiita: ���܂���Collider��isTrigger���������Ĕ��肷����@��m�����̂Ń��� https://qiita.com/tsujihaneta/items/ec656c2092e06a881ca8

namespace nitou.Detecor {

    /// <summary>
    /// �������o�p�R���|�[�l���g�̊��N���X�D
    /// ��̓I�Ȍ��o�����͔h���N���X���ɒ�`����D
    /// </summary>
    public abstract class CollisionHitDetector : DetectorBase {

        /// <summary>
        /// �Փ˃R���C�_�[�̃��X�g.
        /// �v�f�C���f�b�N�X�� <see cref="_hitObjects" />�ƑΉ����Ă���.
        /// </summary>
        protected readonly List<Collider> _hitColliders = new();

        /// <summary>
        /// �Փ˃R���C�_�[��ۗL����I�u�W�F�N�g�̃��X�g�D
        /// ���ۗL�I�u�W�F�N�g�̔����<see cref="_cacheTargetType"/>�Ɉˑ�����D
        /// </summary>
        protected readonly List<GameObject> _hitObjects = new();

        /// <summary>
        /// ���݂̃t���[���ŏՓ˂����R���C�_�[�̃��X�g�D
        /// </summary>
        protected readonly List<Collider> _hitCollidersInThisFrame = new();

        /// <summary>
        /// List of GameObjects hit during the current frame.
        /// ���ۗL�I�u�W�F�N�g�̔����<see cref="_cacheTargetType"/>�Ɉˑ�����D
        /// </summary>
        protected readonly List<GameObject> _hitObjectsInThisFrame = new();


        // Event Streem
        private readonly Subject<List<GameObject>> _onHitObjectsSubject = new();


        /// ----------------------------------------------------------------------------
        // Property

        /// <summary>
        /// �q�b�g���Ă��邩�ǂ����D
        /// </summary>
        public bool IsHit => _hitColliders.Count > 0;

        /// <summary>
        /// ���݂̃t���[���Ńq�b�g���Ă��邩�ǂ����D
        /// </summary>
        public bool IsHitInThisFrame => _hitCollidersInThisFrame.Count > 0;

        /// <summary>
        /// �͈͓��̏Փ˃I�u�W�F�N�g�̃��X�g�D
        /// </summary>
        public IReadOnlyList<GameObject> HitObjects => _hitObjects;

        /// <summary>
        /// ���݂̃t���[���Ńq�b�g�����I�u�W�F�N�g�̃��X�g�D
        /// </summary>
        public IReadOnlyList<GameObject> HitObjectsInThisFrame => _hitObjectsInThisFrame;

        /// <summary>
        /// �R���W���������o���ꂽ�Ƃ��̃C�x���g�ʒm
        /// </summary>
        public System.IObservable<List<GameObject>> OnHitObjects => _onHitObjectsSubject;


        /// ----------------------------------------------------------------------------
        // LifeCycle Events

        protected virtual void OnDestroy() {
            _onHitObjectsSubject.OnCompleted();
            _onHitObjectsSubject.Dispose();
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// <see cref="_hitObjects" /> �Ɋ܂܂��I�u�W�F�N�g�ɑΉ�����Collider���擾����D
        /// </summary>
        public Collider GetColliderForGameObject(GameObject obj) {
            var index = _hitObjects.IndexOf(obj);
            return index == -1 ? null : _hitCollidersInThisFrame[index];
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        /// <summary>
        /// ���X�g������������.
        /// </summary>
        protected void InitializeBufferOfCollidedCollision() {
            _hitColliders.Clear();
            _hitObjects.Clear();
            _hitObjectsInThisFrame.Clear();
            _hitCollidersInThisFrame.Clear();
        }

        /// <summary>
        /// �C�x���g�𔭉΂���
        /// </summary>
        protected void RaiseOnHitEvent(List<GameObject> objects) {
            _onHitObjectsSubject.OnNext(objects);
        }
    }

}
