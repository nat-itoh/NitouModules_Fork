
namespace nitou.Detecor{

    /// <summary>
    /// �Z���T�Ō��o�\�ȃI�u�W�F�N�g
    /// </summary>
    public interface ISensorDetectable {

        /// <summary>
        /// ���o�͈͂ɓ��������̏���
        /// </summary>
        internal void OnEnter();

        /// <summary>
        /// ���o�͈͂���o�����̏���
        /// </summary>
        internal void OnExit();
    }
}
