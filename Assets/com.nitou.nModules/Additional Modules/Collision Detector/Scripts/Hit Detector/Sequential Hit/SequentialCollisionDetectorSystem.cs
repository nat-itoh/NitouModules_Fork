using UnityEngine;
using System.Linq;
using nitou.BachProcessor;

namespace nitou.Detecor{
    public partial class SequentialCollisionDetector {

        /// <summary>
        /// <see cref="SequentialCollisionDetector"/>�̍X�V������S���N���X
        /// </summary>
        private class SequentialCollisionDetectorSystem :
            SystemBase<SequentialCollisionDetector, SequentialCollisionDetectorSystem>,
            IEarlyUpdate {

            private readonly Collider[] _results = new Collider[CAPACITY];

            // 1�x�Ɍ��o�ł���R���W�����̍ő吔.
            private const int CAPACITY = 50;

            /// <summary>
            /// �V�X�e���̎��s����
            /// </summary>
            int ISystemBase.Order => 0;


            /// ----------------------------------------------------------------------------
            // LifeCycle Events

            private void OnDestroy() {
                UnregisterAllComponents();
            }

            /// <summary>
            /// �X�V����
            /// </summary>
            void IEarlyUpdate.OnUpdate() {

                // Initialize components
                Components.ForEach(c => c.PrepareFrame());

                // Update collision detection using Physics
                Components.ForEach(c => c.OnUpdate(in _results));

                // Raise events related to collided colliders
                Components.Where(c => c.IsHitInThisFrame)
                    .ForEach(c => c.RaiseOnHitEvent(c._hitObjectsInThisFrame));
            }
        }

    }
}
