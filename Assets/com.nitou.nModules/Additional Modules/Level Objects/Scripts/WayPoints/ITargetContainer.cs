using System.Collections.Generic;

namespace nitou.LevelObjects{

    /// <summary>
    /// 任意型のコンテナ
    /// </summary>
    public interface ITargetContainer<T>{
        int Count { get; }
        IEnumerable<T> Targets { get; }
        T First { get; }

        void Add(T item);
        void Clear();
        bool Contains(T item);
        bool Remove(T item);
    }


    /// <summary>
    /// 
    /// </summary>
    public interface ITargetSelector<T> {
        public T Selected { get; }

        public bool Select(T item);
    }
}
