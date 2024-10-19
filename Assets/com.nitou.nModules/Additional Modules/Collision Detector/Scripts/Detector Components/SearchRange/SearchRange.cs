using System.Linq;
using UniRx;
using UnityEngine;
using nitou.BachProcessor;
using Sirenix.OdinInspector;

namespace nitou.Detecor {

    /// <summary>
    /// 
    /// </summary>
    public partial class SearchRange : DetectorBase {

        // ���a
        [SerializeField] private float _radius = 2f;

        // ���������p
        private readonly ReactiveCollection<GameObject> _hitObjects = new();
        Transform _transform;


        /// <summary>
        /// 
        /// </summary>
        public int Count => _hitObjects.Count;

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyReactiveCollection<GameObject> HitObjects => _hitObjects;


        /// ----------------------------------------------------------------------------
        // LifeCycle Events

        private void Awake() {
            _transform = transform;

            // test
            HitObjects.ObserveCountChanged()
                .Subscribe(_ => Debug_.ListLog(HitObjects.ToList(), Colors.Red));
        }

        private void OnEnable() {
            SearchRangeSystem.Register(this, Timing);
            InitializeBufferOfCollidedCollision();      // �L���b�V���̃N���A
        }

        private void OnDisable() {
            SearchRangeSystem.Unregister(this, Timing);
            InitializeBufferOfCollidedCollision();      // �L���b�V���̃N���A
        }

        private void OnDestroy() {
            _hitObjects?.Dispose();
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void OnUpdate(in Collider[] hitColliders) {

            // Perform collision detection.
            var count = Physics.OverlapSphereNonAlloc(_transform.position, _radius, hitColliders, _hitLayer, QueryTriggerInteraction.Ignore);

            // 
            var hitObjectsInThisFram = hitColliders
                .Take(count)
                .WithoutNull()
                .Select(col => DetectionUtil.GetHitObject(col, _cacheTargetType));

            // ����������
            _hitObjects.SynchronizeWith(hitObjectsInThisFram);

        }


        /// ----------------------------------------------------------------------------
        // Private Methods

        /// <summary>
        /// ���X�g������������.
        /// </summary>
        protected void InitializeBufferOfCollidedCollision() {
            _hitObjects.Clear();
        }


        /// ----------------------------------------------------------------------------
#if UNITY_EDITOR
        private void OnDrawGizmos() {

            var position = transform.position;

            // Range
            var color = Count > 0 ? Colors.Green : Colors.GreenYellow;
            Gizmos_.DrawWireSphere(position, _radius, color);

            //{
            //    var sceneViewCamera = Camera.current;
            //    if (sceneViewCamera != null) {
            //        // �V�[���r���[�J�����̉�]���擾
            //        Quaternion cameraRotation = sceneViewCamera.transform.rotation;

            //        // �J���������ɉ~����]
            //        Gizmos.matrix = Matrix4x4.TRS(position, cameraRotation, Vector3.one);
            //        Gizmos_.DrawWireCircle(transform.position, _radius, Colors.Red);

            //        // �s������Z�b�g
            //        Gizmos.matrix = Matrix4x4.identity;
            //    }
            //}


            if (_hitObjects.IsNullOrEmpty()) return;

            // Target
            foreach (var obj in _hitObjects) {
                Gizmos_.DrawSphere(obj.transform.position, 0.1f, Colors.Gray);
            }

        }
#endif
    }
}
