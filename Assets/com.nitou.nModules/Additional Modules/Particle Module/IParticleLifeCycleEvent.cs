
namespace nitou.ParticleModule{

    /// <summary>
    /// ParticleSystem�̍Đ����̃C�x���g
    /// </summary>
    public interface IParticleLifeCycleEvent {

        /// <summary>
        /// �Đ��J�n���̃C�x���g
        /// </summary>
        public void OnPlayed();

        /// <summary>
        /// �Đ��I�����̃C�x���g
        /// </summary>
        public void OnStopped();
    }
}