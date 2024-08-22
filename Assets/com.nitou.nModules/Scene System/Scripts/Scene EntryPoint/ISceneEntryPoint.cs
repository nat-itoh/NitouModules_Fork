using Cysharp.Threading.Tasks;

namespace nitou.SceneSystem{

    /// <summary>
    /// �e�V�[���ɔz�u����N�_�I�u�W�F�N�g
    /// </summary>
    public interface ISceneEntryPoint {

        /// <summary>
        /// �V�[���ǂݍ��ݎ��̏���
        /// </summary>
        UniTask OnSceneLoad();

        /// <summary>
        /// �A�N�e�B�u�V�[���ɐݒ肳�ꂽ���̏���
        /// </summary>
        UniTask OnSceneActivate();

        /// <summary>
        /// �A�N�e�B�u�V�[������������ꂽ���̏���
        /// </summary>
        UniTask OnSceneDeactivate();

        /// <summary>
        /// �V�[��������̏���
        /// </summary>
        UniTask OnSceneUnload();
    }
}
