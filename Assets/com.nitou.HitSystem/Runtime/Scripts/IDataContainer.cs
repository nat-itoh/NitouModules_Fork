
namespace nitou{

    /// <summary>
    /// 
    /// </summary>
    public interface IDataContainer<TData> {

        int Count { get; }

        TData First { get; }

        /// <summary>
        /// �v�f��ǉ�����
        /// </summary>
        void Add(TData item);
        
        /// <summary>
        /// �v�f���폜����
        /// </summary>
        bool Remove(TData item);
        
        /// <summary>
        /// 
        /// </summary>
        void Clear();

        /// <summary>
        /// �v�f���܂�ł��邩�m�F����
        /// </summary>
        bool Contains(TData item);
    }

}
