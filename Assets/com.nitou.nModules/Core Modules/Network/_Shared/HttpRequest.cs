
namespace nitou.Networking {

    /// <summary>
    /// ���N�G�X�g�̊��N���X
    /// </summary>
    public abstract class HttpRequest {
        
        public abstract string Path { get; }


        /// ----------------------------------------------------------------------------
        #region Result

        /// <summary>
        /// �ʐM���̂�����ɍs��ꂽ���̌���
        /// </summary>
        public abstract record Result() {
            public bool IsSuccess() => this is Success;
            public bool IsCanceled() => this is Canceld;
            public bool IsFiled() => this is Failed;
        }

        public record Success() : Result;
        public record Canceld() : Result;
        public record Failed() : Result;
        #endregion
    }
}
