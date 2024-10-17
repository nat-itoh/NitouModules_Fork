using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace nitou.LevelActors {
       
    /// <summary>
    /// �L�����N�^�[�ړ������̎Q��
    /// </summary>
    [System.Serializable]
    public class MovementReference : MonoBehaviour {

        //[DisplayAsString, HideLabel, ShowInInspector]
        //[PropertySpace(1, 1), GUIColor("@Colors.Orange")]
        //private readonly string message = "������";

        [TitleGroup("Reference Mode")]
        [EnumToggleButtons, HideLabel]
        [SerializeField, Indent] private MovementReferenceMode _mode = MovementReferenceMode.World;

        [Space]

        [TitleGroup("Reference Mode")]
        [ShowIf("_mode", MovementReferenceMode.External)]
        [SerializeField, Indent] private Transform _externalReference = null;


        /// ----------------------------------------------------------------------------
        // Properity

        /// <summary>
        /// �ړ����͂̊�ƂȂ���W�n
        /// </summary>
        public MovementReferenceMode Mode {
            get => _mode;
            set => _mode = value;
        }

        /// <summary>
        /// "External"���[�h�̎��ɎQ�Ƃ�����W�n
        /// </summary>
        public Transform ExternalReference {
            get => _externalReference;
            set => _externalReference = value;
        }


        /// <summary>
        /// ����W�n�i�J�������j�ɕϊ����ꂽ���̓x�N�g��.
        /// </summary>
        public Vector3 InputMovementReference { get; private set; }

        /// <summary>
        /// ����W�n�̐��ʃx�N�g��.
        /// </summary>
        public Vector3 MovementReferenceForward { get; private set; }

        /// <summary>
        /// ����W�n�̉E�x�N�g��
        /// </summary>
        public Vector3 MovementReferenceRight { get; private set; }


        /// ----------------------------------------------------------------------------
        // Public Method 

        public void SetMode(MovementReferenceMode mode) {
            _mode = mode;

            ResetInputData();
            UpdateMovementReferenceData();
        }


        /// <summary>
        /// ���͒l�̍X�V
        /// </summary>
        public void UpdateData(Vector2 movementInput) {
            // ���W�n�̍X�V
            UpdateMovementReferenceData();

            // ���͒l�̍X�V
            Vector3 inputMovementReference =
                (MovementReferenceRight * movementInput.x) +
                (MovementReferenceForward * movementInput.y);
            InputMovementReference = Vector3.ClampMagnitude(inputMovementReference, 1f);
        }

        public void ResetInputData() {
            InputMovementReference = Vector3.zero;
        }


        /// ----------------------------------------------------------------------------
        // Private Method 

        /// <summary>
        /// ����W�n�̍X�V
        /// </summary>
        private void UpdateMovementReferenceData() {
            // Forward
            switch (Mode) {
                case MovementReferenceMode.World:   // ----- �O���[�o�����W�n�

                    MovementReferenceForward = Vector3.forward;
                    MovementReferenceRight = Vector3.right;
                    break;

                case MovementReferenceMode.Actor:   // ----- �L�������ʊ

                    MovementReferenceForward = transform.forward;
                    MovementReferenceRight = transform.right;
                    break;

                case MovementReferenceMode.External:    // ---- �C�ӂ̍��W�n�

                    if (ExternalReference != null) {
                        MovementReferenceForward = Vector3.Normalize(Vector3.ProjectOnPlane(ExternalReference.forward, transform.up));
                        MovementReferenceRight = Vector3.Normalize(Vector3.ProjectOnPlane(ExternalReference.right, transform.up));
                    } else
                        if (Application.isPlaying)
                        Debug_.LogWarning("the external reference is null! assign a Transform.");
                    break;
            }
        }


        /// ----------------------------------------------------------------------------
#if UNITY_EDITOR

        [Title("Debug")]
        [SerializeField, Indent] Vector3 _offset = Vector3.up * 0.01f;
        [SerializeField, Indent] float _radius = 1.5f;

        private void OnValidate() => UpdateMovementReferenceData();

        private void OnDrawGizmos() {

            var pos = transform.position + _offset;

            // Circle
            Gizmos_.DrawWireCircle(pos, _radius * 0.4f, Colors.Gray);
            Gizmos_.DrawWireCircle(pos, _radius * 0.8f, Colors.Gray);

            // Vectors
            Gizmos_.DrawRayArrow(pos, MovementReferenceForward * _radius, Colors.DeepSkyBlue);
            Gizmos_.DrawRayArrow(pos, MovementReferenceRight * _radius, Colors.Orangered);
            Gizmos_.DrawRay(pos, InputMovementReference * _radius, Colors.WhiteSmoke);
        }
#endif

    }


    /// <summary>
    /// MovementReference�ɑ΂���ėp���C�u����
    /// </summary>
    public static class MovementReferenceExtensions {

        /// <summary>
        /// 
        /// </summary>
        public static void SetCameraTransform(this MovementReference self) {
            if (Camera.main == null) {
                Debug_.LogWarning("active camera not found.");
                return;
            }

            self.Mode = MovementReferenceMode.External;
            self.ExternalReference = Camera.main.transform ?? throw new ArgumentNullException();
        }
    }

}
