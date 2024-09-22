using System;
using Cysharp.Threading.Tasks;

namespace nitou.GameSystem {

    /// <summary>
    /// 
    /// </summary>
    public interface IProcess : IDisposable{

        /// <summary>
        /// �I�����̒ʒm
        /// </summary>
        public UniTask<ProcessResult> ProcessFinished { get; }

        /// <summary>
        /// �J�n����
        /// </summary>
        public void Run();
        
        /// <summary>
        /// �|�[�Y����
        /// </summary>
        public void Pause();

        /// <summary>
        /// �|�[�Y��������
        /// </summary>
        public void UnPause();

        /// <summary>
        /// �L�����Z������
        /// </summary>
        public void Cancel(CancelResult cancelResult = null);
    }
}
