
namespace nitou{

    /// <summary>
    /// <see cref="TData"/>�^�̃f�[�^��I���ł���
    /// </summary>
    public interface IDataSelector<TData>{
        
        /// <summary>
        /// �I�𒆂̃C���X�^���X
        /// </summary>
        TData Selected { get; }

        bool IsValid { get; }

        /// <summary>
        /// �w��C���X�^���X��I������
        /// </summary>
        bool Select(TData data);
    }
}
