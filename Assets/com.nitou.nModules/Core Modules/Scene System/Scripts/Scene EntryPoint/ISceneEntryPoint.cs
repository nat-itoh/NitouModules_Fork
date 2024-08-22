using Cysharp.Threading.Tasks;

namespace nitou.SceneSystem{

    /// <summary>
    /// �e�V�[���ɔz�u����N�_�I�u�W�F�N�g
    /// </summary>
    public interface ISceneEntryPoint {

        /// <summary>
        /// �V�[���ǂݍ��ݎ��̏���
        /// </summary>
        UniTask OnSceneLoadAsync();

        /// <summary>
        /// �A�N�e�B�u�V�[���ɐݒ肳�ꂽ���̏���
        /// </summary>
        UniTask OnSceneActivateAsync();

        /// <summary>
        /// �A�N�e�B�u�V�[������������ꂽ���̏���
        /// </summary>
        UniTask OnSceneDeactivateAsync();

        /// <summary>
        /// �V�[��������̏���
        /// </summary>
        UniTask OnSceneUnloadAsync();
    }
}
