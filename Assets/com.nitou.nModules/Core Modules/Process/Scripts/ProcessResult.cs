
namespace nitou.GameSystem {

    /// <summary>
    /// ���ʃf�[�^�̊��N���X
    /// </summary>
    public abstract class ProcessResult { }

    /// <summary>
    /// �������̌��ʃf�[�^
    /// </summary>
    public class CompleteResult : ProcessResult { }

    /// <summary>
    /// �L�����Z�����̌��ʃf�[�^
    /// </summary>
    public class CancelResult : ProcessResult { }
}
