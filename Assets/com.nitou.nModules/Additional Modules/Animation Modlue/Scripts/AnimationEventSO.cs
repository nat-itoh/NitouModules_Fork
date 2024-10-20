using UnityEngine;
using Sirenix.OdinInspector;

// [�Q�l]
//  Hatena: Unity��Animator�ŃA�j���[�V�����J�ڂ���Ƃ��̎����Ȃ�̉� https://yutakaseda3216.hatenablog.com/entry/2016/08/19/112114

namespace nitou.AnimationModule {

    /// <summary>
    /// Animator�̃X�e�[�g��Animation�ƕR�Â��Ď��s�����C�x���g�f�[�^
    /// </summary>
    public abstract class AnimationEventSO : SerializedScriptableObject {

        /// ----------------------------------------------------------------------------
        #region Inspecter Group�i���C���X�y�N�^�\���p�j

        // �O���[�v
        protected const string CLIP_INFO = "�N���b�v���";
        protected const string EVENT_INFO = "�A�j���[�V���� �C�x���g";
        #endregion


        /// ----------------------------------------------------------------------------
        #region Field & Properity

        // �ėp

        /// <summary>
        /// ������
        /// </summary>
        [TextArea(2, 3)]
        [SerializeField] private string _description = "";

        /// --- 
        // �N���b�v���

        /// <summary>
        /// �ΏۃN���b�v
        /// </summary>
        [TitleGroup(CLIP_INFO), Indent]
        [SerializeField] private AnimationClip Clip;

        /// <summary>
        /// �N���b�v�̒���[sec]
        /// </summary>
        [TitleGroup(CLIP_INFO), Indent]
        [ShowInInspector, ReadOnly]
        public float Length => (Clip != null) ? Clip.length : 0;

        /// <summary>
        /// �N���b�v�̒���[sec]
        /// </summary>
        [TitleGroup(CLIP_INFO), Indent]
        [ShowInInspector, ReadOnly]
        public bool IsLoop => (Clip != null) ? Clip.isLooping : false;

        /// --- 
        // ���������p

        /// <summary>
        /// �S�ẴC�x���g�������������ǂ���
        /// </summary>
        public bool IsCompleted { get; protected set; }

        /// <summary>
        /// ���݁C�ҋ@���̃C�x���g���
        /// �i�����ۂ̃C�x���g�f�[�^�̃��X�g�͔h���N���X�Œ�`�j
        /// </summary>
        protected int _currentIndex;

        /// <summary>
        /// �C�x���g���s�^�C�~���O�]�����̋��e�덷�i���w��l�ɐ��K�����Ԃ�p���Ă���̂͏C������ׂ��j
        /// </summary>
        protected static readonly float BREADTH_TIME = 0.01f;

        #endregion


        /// ----------------------------------------------------------------------------
        // Internal Method

        /// <summary>
        /// ����������
        /// </summary>
        internal virtual void Initialize() {
            SortData();

            _currentIndex = 0;
            IsCompleted = false;
        }

        /// <summary>
        /// �f�[�^�����s�^�C�~���O���Ƀ\�[�g����
        /// </summary>
        internal abstract void SortData();


        /// ----------------------------------------------------------------------------
        // Internal Method

        /// <summary>
        /// �C�x���g�����s����^�C�~���O���]������
        /// </summary>
        internal abstract bool CheckNormalizeTime(float currentNormalizedTime);

        /// <summary>
        /// �ҋ@���̃C�x���g�����s����
        /// </summary>
        internal abstract void ExecuteCurrentEvent(Animator animator);
    }



}