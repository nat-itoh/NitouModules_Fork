#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// [�Q�l]
//  qiita: AnimationClip.SampleAnimation �ɂ� Animator �R���|�[�l���g���K�v https://qiita.com/neusstudio/items/e98401817cc3b8c21c94

namespace nitou.AnimationModule {
    using nitou.Detecor;

    /// <summary>
    /// �A�j���[�V�����Ɗ֘A���鏈�����V�[���ҏW���邽�߂̃R���|�[�l���g
    /// </summary>
    [ExecuteAlways]
    public partial class AnimationPreview : MonoBehaviour {

        // General
        private float _playbackSpeed = 1f;
        public static bool autoStopOnDisable = false;

        // Animation
        [SerializeField] private GameObject _target;
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _animationClip;
        private float _animationSpeed = 1f;
        private float _animationDelay = 0f;

        // Particle


        // play
        [SerializeField] private SequentialCollisionDetector _colliders;

        // playback
        private float _playbackStart = 0f;
        private float _playbackEnd = 3f;
        private float _playbackValue = 0f;
        private double _startTime;


        /// ----------------------------------------------------------------------------
        // Properity (Playback Control)

        /// <summary>
        /// �V�~�����[�V�������s�����ǂ���
        /// </summary>
        public bool IsSimulating { get; private set; } = false;

        /// <summary>
        /// �|�[�Y��Ԃ��ǂ���
        /// </summary>
        public bool IsPaused { get; private set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool IsScrubbing { get; set; } = false;

        /// <summary>
        /// Animator�̍X�V�������s���邩�ǂ���
        /// </summary>
        public bool CanUpdateAnimator => _animator != null && _animationClip != null;

        /// <summary>
        /// Collider�̍X�V�������s���邩�ǂ���
        /// </summary>
        public bool CanUpdateCollider => _colliders != null;



        /// ----------------------------------------------------------------------------
        // Properity (Playback Control)

        public float PlaybackSpeed {
            get => _playbackSpeed;
            set => _playbackSpeed = Mathf.Max(0f, value);
        }

        // Save autoRandomSeed
        public static bool AUTO_SAVE_SEED_ON_SIMULATE = true;


        /// ----------------------------------------------------------------------------
        // Properity

        /// <summary>
        /// �A�j���[�V����������ΏۃI�u�W�F�N�g
        /// </summary>
        public Animator Animator => _animator;

        /// <summary>
        /// �A�j���[�V�����N���b�v
        /// </summary>
        public AnimationClip Clip => _animationClip;

        /// <summary>
        /// �A�j���[�V�����̍Đ����x
        /// </summary>
        public float AnimationSpeed {
            get => _animationSpeed;
            set => _animationSpeed = Mathf.Max(0, value);
        }

        /// <summary>
        /// �A�j���[�V�����̊J�n�^�C�~���O
        /// </summary>
        public float AnimationDelay {
            get => _animationDelay;
            set => _animationDelay = Mathf.Max(0, value);
        }


        /// ----------------------------------------------------------------------------
        // Properity (Playback Control)

        /// <summary>
        /// �J�n����
        /// </summary>
        public float PlaybackStart {
            get => _playbackStart;
            set {
                _playbackStart = Mathf.Clamp(value, 0, _playbackEnd);
                ClampPlaybackValue();
            }
        }

        /// <summary>
        /// �I������
        /// </summary>
        public float PlaybackEnd {
            get => _playbackEnd;
            set {
                _playbackEnd = Mathf.Clamp(value, _playbackStart, float.MaxValue);
                ClampPlaybackValue();
            }
        }

        /// <summary>
        /// ���݂̃V�~�����[�V�����Đ�����
        /// </summary>
        public float PlaybackValue {
            get => _playbackValue;
            set {
                _playbackValue = value;
                ClampPlaybackValue();
            }
        }

        /// <summary>
        /// ���݂̃V�~�����[�V�Đ���
        /// </summary>
        public float PlaybackPercent => _playbackValue / _playbackEnd;


        /// <summary>
        /// ���݂̃A�j���[�V�����Đ�����
        /// </summary>
        public float AnimationPlayback => (_animationClip != null)
            ? (_playbackValue - AnimationDelay)
            : 0;

        /// <summary>
        /// ���݂̃A�j���[�V�����Đ��� (��NormalizedTime)
        /// </summary>
        public float AnimationPlaybackPercent => (_animationClip != null)
            ? Mathf.Clamp01((_playbackValue - _animationDelay) / _animationClip.length)
            : 0;



        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        private void OnEnable() {
            EditorApplication.update += SimulateUpdate;
        }

        private void OnDisable() {
            EditorApplication.update -= SimulateUpdate;
        }

        public void Reset() {
            IsSimulating = false;
            IsPaused = true;
        }

        private void OnValidate() {
            if (_target == null) return;

            // Animator�̃Z�b�g�A�b�v
            if (_target.TryGetComponentInChildren<Animator>(out _animator)) {
                if (CanUpdateAnimator) {
                    _animationClip.SampleAnimation(_animator.gameObject, 0);
                }
            } else {
                Debug_.Log($"{_target.name}�ɂ�Animator���A�^�b�`����Ă��܂���D");
                _target = null;
            }
        }

        /// ----------------------------------------------------------------------------
        // Private Method

        public void PlayButton() {
            IsPaused = false;
            StartSimulation(true, false);
        }

        public void PauseButton() {
            IsPaused = true;
            StartSimulation(true, false);
        }

        public void StopButton() {
            IsPaused = true;
            StartSimulation(false, true);
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// �V�~�����[�V�������J�n����
        /// </summary>
        private void StartSimulation(bool _simulate, bool _resetToBeginning) {

            if (_resetToBeginning) {
                _playbackValue = _playbackStart;
            }
            // 
            else {
                _playbackValue = _playbackValue % _playbackEnd;
            }

            _startTime = EditorApplication.timeSinceStartup - (_playbackValue / _playbackSpeed);
            IsSimulating = _simulate;

            // �A�j���[�V�����X�V
            if (CanUpdateAnimator) {
                _animationClip.SampleAnimation(_animator.gameObject, 0f);
            }

            // �p�[�e�B�N���X�V
            if (true) {

            }

            // �R���C�_�[�X�V
            if (CanUpdateCollider) {
                _colliders.Rate = 0f;
            }

        }

        /// <summary>
        /// �V�~�����[�V���������߂���J�n����
        /// </summary>
        public void RestartSimulation() => StartSimulation(true, true);

        /// <summary>
        /// �X�V����
        /// </summary>
        private void SimulateUpdate() {
            if (!IsSimulating) return;

            // �Đ���
            if (!IsPaused && !IsScrubbing) {
                // �o�ߎ���
                double dt = (EditorApplication.timeSinceStartup - _startTime) * _playbackSpeed; // ��_playbackSpeed��dt�݂̂Ɋ֗^
                _playbackValue = (float)(dt) % _playbackEnd;

                if (dt >= _playbackEnd) {
                    RestartSimulation();
                    return;
                }
            }

            // �A�j���[�V�����X�V
            if (CanUpdateAnimator) {
                _animationClip.SampleAnimation(_animator.gameObject, AnimationPlayback);
            }

            // �R���C�_�[�X�V
            if (CanUpdateCollider) {
                _colliders.Rate = AnimationPlaybackPercent;
            }

        }




        /// ----------------------------------------------------------------------------
        // Private Method

        private void ClampPlaybackValue() {
            _playbackValue = Mathf.Clamp(_playbackValue, _playbackStart, _playbackEnd);
        }

        private void Save() {
            if (AUTO_SAVE_SEED_ON_SIMULATE) {
                //foreach (var p in _particlePreviews) {
                //    p.SaveSeed();
                //}
            }
        }

        private void Load() {
            if (AUTO_SAVE_SEED_ON_SIMULATE) {
                //foreach (var p in _particlePreviews) {
                //    p.LoadSeed();
                //}
            }
        }

    }

}
#endif