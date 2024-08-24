using UnityEngine;
using UnityEngine.AI;

// [�Q�l]
//  _: NavMeshAgent�ŁA���ꂩ��ʂ�o�H��`�悷�� https://indie-du.com/entry/2016/05/21/080000#google_vignette

namespace nitou.LevelObjects{

    /// <summary>
    /// NavMeshPath����������ȈՃR���|�[�l���g
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class NavMeshPathVisualizer : MonoBehaviour{

        // �`��p�R���|�[�l���g
        private LineRenderer _lineRenderer;


        /// ----------------------------------------------------------------------------
        // MonoBehaviour

        private void Awake() {
            _lineRenderer = gameObject.GetComponent<LineRenderer>();

            // ��\����Ԃɐݒ�
            _lineRenderer.enabled = false;
            this.enabled = false;
        }

        private void OnEnable() {
            _lineRenderer.enabled = true;
        }

        private void OnDisable() {
            _lineRenderer.enabled = false;
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// ���C�������_���[�Ƀp�X��ݒ肷��
        /// </summary>
        public void SetPath(NavMeshPath path, bool showLine = true) {
            Error.ArgumentNullException(path);

            // �\�����
            this.enabled = showLine;

            // �o�H�̍X�V
            _lineRenderer.SetPositions(path.corners);
            //_lineRenderer.SetColor(GetPathColor(path.status));
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// �p�X�̏�Ԃɉ������`��F���擾����
        /// </summary>
        private Color GetPathColor(NavMeshPathStatus pathStatus) => pathStatus switch {
            NavMeshPathStatus.PathComplete => Colors.Green,
            NavMeshPathStatus.PathPartial=> Colors.Yellow,
            NavMeshPathStatus.PathInvalid => Colors.Red,
            _=> throw new System.NotImplementedException()
        };

    }
}
