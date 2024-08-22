
namespace nitou.BachProcessor{
    
    public interface ISystemBase{
        /// <summary>
        /// �V�X�e���̎��s����
        /// </summary>
        int Order { get; }
    }

    public interface IEarlyUpdate : ISystemBase {
        void OnUpdate();
    }

    public  interface IPostUpdate : ISystemBase {
        void OnLateUpdate();
    }
}