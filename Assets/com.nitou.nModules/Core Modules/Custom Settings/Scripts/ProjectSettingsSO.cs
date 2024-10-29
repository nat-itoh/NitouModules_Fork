using UnityEngine;
using Sirenix.OdinInspector;

// [�Q�l]
//  qiita: Unity�œƎ��̐ݒ��UI��񋟂ł���SettingsProvider�̏Љ�Ɛݒ�t�@�C���̕ۑ��ɂ��� https://qiita.com/sune2/items/a88cdee6e9a86652137c

namespace nitou.Settings {

    /// <summary>
    /// Runtime�ŎQ�Ƃ���v���W�F�N�g�ŗL�̐ݒ�f�[�^
    /// </summary>
    public class ProjectSettingsSO : ScriptableObject {

        #region Singleton
        private static ProjectSettingsSO _instance;
        public static ProjectSettingsSO Instance {
            get {
                if (_instance == null) {
                    _instance = Resources.Load<ProjectSettingsSO>(nameof(ProjectSettingsSO));
                }
                return _instance;
            }
        }
        #endregion


        /// ----------------------------------------------------------------------------

        [Title(" ")]
        [Indent] public bool executeAppLauncher;
        [Indent] public string text;

        [Title("UI")]

        [Indent] public bool s;

        [SerializeField] Vector2 _referenceResolution = new Vector2(1920, 1080);
        
        [SerializeField] int _screenCanvasSortingOrder = 10;
        [SerializeField] int _overlayCanvasSortingOrder = 100;



        /// ----------------------------------------------------------------------------

        /// <summary>
        /// ��𑜓x
        /// </summary>
        public Vector2 ReferenceResolution => _referenceResolution;

        /// <summary>
        /// �ʏ�L�����o�X�̕`�揇
        /// </summary>
        public int ScreenCanvasSortingOrder => _screenCanvasSortingOrder;

        /// <summary>
        /// �I�[�o�[���C�L�����o�X�̕`�揇
        /// </summary>
        public int OverlayCanvasSortingOrder => _overlayCanvasSortingOrder;
    }

}