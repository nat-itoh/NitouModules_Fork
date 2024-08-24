using UnityEngine;

namespace nitou.LevelObjects{
    using nitou.Inspector;

    //[ExecuteAlways]
    public class LineToTarget : LineRendererAddOn {

        [Title("Target Components")]
        [SerializeField, Indent]
        private Transform _targetTrans;

        protected override bool UseWorldSpace => true;

        /// <summary>
        /// ���_�𐶐�����
        /// </summary>
        protected override Vector3[] CreateVertices() {
            if (_targetTrans == null) {
                return new Vector3[0]; // ��̒��_�z���Ԃ�
            }

            // �n�_�ƏI�_��2�_��ݒ�
            return new Vector3[] { transform.position, _targetTrans.position };
        }

        /// <summary>
        /// �`����X�V���邩���m�F����
        /// </summary>
        protected override bool CheckDirtyFlag() => _targetTrans != null;
    }
}
