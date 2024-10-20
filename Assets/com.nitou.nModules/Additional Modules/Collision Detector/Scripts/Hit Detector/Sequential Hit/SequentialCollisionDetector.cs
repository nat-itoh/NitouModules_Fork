using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace nitou.Detecor {

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SequentialCollisionDetector : CollisionHitDetector {

        [Title("Position and timing")]

        [LabelText("Rate")]
        [SerializeField, Indent] NormalizedValue _normalizedValue;

        [ListDrawerSettings(IsReadOnly = true, DefaultExpandedState = true)]
        [SerializeField, Indent] List<DetectionBox> _dataList = new();


        /// <summary>
        /// ���K�����ꂽ�l�D
        /// �i��AnimationClip��NormalizedTime�𗬂����ޗp�j
        /// </summary>
        public NormalizedValue Rate {
            get => _normalizedValue;
            set => _normalizedValue = value;
        }

        /// <summary>
        /// ���o�Ɏg�p����R���C�_�[�Q�D
        /// </summary>
        public IReadOnlyList<DetectionBox> DataList => _dataList;


        /// ----------------------------------------------------------------------------
        // LifeCycle Events

        private void OnEnable() {
            SequentialCollisionDetectorSystem.Register(this, Timing);
            InitializeBufferOfCollidedCollision();
        }

        private void OnDisable() {
            SequentialCollisionDetectorSystem.Unregister(this, Timing);
        }

        /// <summary>
        /// �e�t���[���ł̌��o�J�n���̏����������D
        /// </summary>
        private void PrepareFrame() {
            _hitCollidersInThisFrame.Clear();
            _hitObjectsInThisFrame.Clear();
        }

        /// <summary>
        /// �X�V�����D
        /// </summary>
        private void OnUpdate(in Collider[] hitColliders) {

            // �e�R���C�_�[�����؂���
            foreach (var data in _dataList) {

                // �R���C�_�[���A�N�e�B�u��Ԃ��`�F�b�N
                if (!data.IsInTimeRange(_normalizedValue)) continue;

                // Perform collision detection.
                var count = CalculateBoxCast(hitColliders, data.volume);

                // Register objects that are not in contact from the list obtained with OverlapSphereNonAlloc(...).
                for (var hitIndex = 0; hitIndex < count; hitIndex++) {
                    var hit = hitColliders[hitIndex];

                    // Exclude own Collider from the collision targets.
                    //if (Owner != null && Owner.IsOwnCollider(hit)) continue;

                    // Get the object judged to have collided.
                    var hitObject = DetectionUtil.GetHitObject(hit, _cacheTargetType);

                    // ���Ƀq�b�g���o�ς݁C�܂��͎w��^�O�ł͂Ȃ�GameObject�̓X�L�b�v����
                    // However, if nothing is set in _hitTags, it won't be skipped.
                    if (_hitObjects.Contains(hitObject) || hitObject.ContainTag(_hitTagArray) == false) {
                        continue;
                    }

                    // Register the target.
                    _hitColliders.Add(hit);
                    _hitObjects.Add(hitObject);
                    _hitCollidersInThisFrame.Add(hit);
                    _hitObjectsInThisFrame.Add(hitObject);
                }

            }
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// Box�̈�Ō���������s���D
        /// </summary>
        private int CalculateBoxCast(Collider[] hitColliders, Shapes.Box box) {

            var worldPosition = box.GetWorldPosition(transform);
            var colRotation = box.GetWorldRotaion(transform);

            // 
            var count = Physics.OverlapBoxNonAlloc(
                center: worldPosition,
                halfExtents: box.size * 0.5f,
                results: hitColliders,
                orientation: colRotation,
                mask: _hitLayer,
                queryTriggerInteraction: QueryTriggerInteraction.Ignore);
            return count;
        }



        /// ----------------------------------------------------------------------------
        #region Scene editing

        [ShowInInspector, ReadOnly]
        public DetectionBox SelectedBox { get; private set; } = null;

        /// ----------------------------------------------------------------------------
        // Private Method

        [ButtonGroup("Control Buttons")]
        private void AddData() {
            _dataList.Add(new DetectionBox());
        }

        [ButtonGroup("Control Buttons"), EnableIf("@_dataList.Count >= 1")]
        private void RemoveData() {
            if (_dataList.Count < 1) return;

            _dataList.Remove(SelectedBox);
            SelectedBox = null;
        }

        [ButtonGroup("Control Buttons")]
        private void Sort() {
            if (_dataList.Count < 1) return;
            _dataList = _dataList.OrderBy(data => data.timeRange.x).ToList();
        }

        /// <summary>
        /// �Ώۂ̃{�b�N�X��I������
        /// </summary>
        internal void Select(DetectionBox target) {
            if (target == null) return;
            var index = _dataList.FindIndex(data => data == target);
            if (index >= 0) {
                SelectedBox = target;
            }
        }

        #endregion

    }
}
