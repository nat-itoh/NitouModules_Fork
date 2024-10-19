using nitou.BachProcessor;
using UnityEngine;

namespace nitou.Detecor {

    public partial class TestSensor {


        public class TestSensorSystem :
            SystemBase<TestSensor, TestSensorSystem>,
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
                foreach (var component in Components) {
                    component.PrepareFrame();
                }

                // Update collision detection using Physics
                foreach (var component in Components) {
                    component.OnUpdate(in _results);
                }

                //// Raise events related to collided colliders
                //foreach (var component in Components) {
                //    if (component.IsHitInThisFrame) {
                //        component.RaiseOnHitEvent(component._hitObjectsInThisFrame);
                //    }
                //}
            }


            /// ----------------------------------------------------------------------------
            // Private Method



        }

    }
}
