using System.Linq;
using UniRx;
using UnityEngine;
using Sirenix.OdinInspector;

namespace nitou.Detecor {

    /// <summary>
    /// ���̂�
    /// </summary>
    public partial class SphereSensor : DetectorBase {

        [Title("Area Shape")]
        [SerializeField, Indent] private float _radius = 2f;

        // ���������p
        private readonly ReactiveCollection<GameObject> _targetObjects = new();

        /// <summary>
        /// �Ώۂ����݂��邩�ǂ���
        /// </summary>
        public bool ExistTargets => _targetObjects.Count > 0;

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyReactiveCollection<GameObject> TargetObjects => _targetObjects;


        /// ----------------------------------------------------------------------------
        // LifeCycle Events

        private void Awake() {
            // 
            _targetObjects.ObserveAdd()
                .Where(e => e.Value.GetComponent<ISensorDetectable>() != null)
                .Subscribe(e => e.Value.GetComponent<ISensorDetectable>().OnEnter());

            // 
            _targetObjects.ObserveRemove()
                .Where(e => e.Value.GetComponent<ISensorDetectable>() != null)
                .Subscribe(e => e.Value.GetComponent<ISensorDetectable>().OnExit());
        }

        private void OnEnable() {
            SphereSensorSystem.Register(this, Timing);
            InitializeBufferOfCollidedCollision();      
        }

        private void OnDisable() {
            SphereSensorSystem.Unregister(this, Timing);
            InitializeBufferOfCollidedCollision();      
        }

        private void OnDestroy() {
            _targetObjects?.Dispose();
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void OnUpdate(in Collider[] hitColliders) {

            // Perform collision detection.
            var count = Physics.OverlapSphereNonAlloc(transform.position, _radius, hitColliders, _hitLayer, QueryTriggerInteraction.Ignore);

            // 
            var hitObjectsInThisFram = hitColliders
                .Take(count)
                .WithoutNull()
                .Select(col => DetectionUtil.GetHitObject(col, _cacheTargetType));

            // ����������
            _targetObjects.SynchronizeWith(hitObjectsInThisFram);
        }


        /// ----------------------------------------------------------------------------
        // Private Methods

        /// <summary>
        /// ���X�g������������.
        /// </summary>
        protected void InitializeBufferOfCollidedCollision() {
            _targetObjects.Clear();
        }


        /// ----------------------------------------------------------------------------
#if UNITY_EDITOR
        /// <summary>
        /// ��I�����̃M�Y���\��
        /// </summary>
        private void OnDrawGizmos() {

            var position = transform.position;
            var color = ExistTargets ? Colors.GreenYellow : Colors.White;

            // Range
            var offset = (0.1f * _radius);
            Gizmos_.DrawWireCylinder(position, _radius, offset * 2f, color);

            if (_targetObjects.IsNullOrEmpty()) return;

            // Target
            foreach (var obj in _targetObjects) {
                Gizmos_.DrawSphere(obj.transform.position, 0.1f, Colors.Gray);
            }

        }

        /// <summary>
        /// �I�����̃M�Y���\��
        /// </summary>
        private void OnDrawGizmosSelected() {
            var position = transform.position;
            var color = ExistTargets ? Colors.GreenYellow : Colors.White;

            //Gizmos_.DrawSphere(position, _radius, color.WithAlpha(0.2f));
        }
#endif
    }
}
