using UnityEngine;
using nitou.BachProcessor;

namespace nitou.Detecor {
    public partial class SphereSensor {

        /// <summary>
        /// <see cref="SphereSensor"/>�̍X�V������S���N���X
        /// </summary>
        public class SphereSensorSystem :
            SystemBase<SphereSensor, SphereSensorSystem>,
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

            void IEarlyUpdate.OnUpdate() {

                // Update
                Components.ForEach(c => c.OnUpdate(_results));
            }
        }

    }
}
