using System;
using Cysharp.Threading.Tasks;
using UniRx;

// [�Q�l]
//  qiita: 2022�N���݂ɂ�����UniRx�̎g���݂� https://qiita.com/toRisouP/items/af7d32846ab99f493d92

namespace nitou.GameSystem {
    using nitou.DesignPattern;

    /// <summary>
    /// �v���Z�X�̊��N���X
    /// </summary>
    public abstract class ProcessBase : IProcess {

        // State
        private ImtStateMachine<ProcessBase, StateEvent> _stateMachine;

        // Others
        private readonly UniTaskCompletionSource<ProcessResult> _finishedSource = new();
        private IDisposable _disposable;
        private ProcessResult _resultData = null;

        /// <summary>
        /// �I�����̒ʒm
        /// </summary>
        public UniTask<ProcessResult> ProcessFinished => _finishedSource.Task;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ProcessBase() {

            // �J�ڃe�[�u��
            _stateMachine = new ImtStateMachine<ProcessBase, StateEvent>(this);
            {
                // �|�[�Y
                _stateMachine.AddTransition<RunningState, PauseState>(StateEvent.Pause);
                _stateMachine.AddTransition<PauseState, RunningState>(StateEvent.UnPause);
                // �I��
                _stateMachine.AddTransition<RunningState, EndState>(StateEvent.Complete);
                _stateMachine.AddTransition<PauseState, EndState>(StateEvent.Complete);
                _stateMachine.AddTransition<RunningState, EndState>(StateEvent.Cancel);
                _stateMachine.AddTransition<PauseState, EndState>(StateEvent.Cancel);
            }
        }

        /// <summary>
        /// �I������
        /// </summary>
        public virtual void Dispose() {
            _disposable?.Dispose();
            _disposable = null;
        }


        /// ----------------------------------------------------------------------------
        // Public Method (�O������)

        public void Run() {
            _stateMachine.SetStartState<RunningState>();
            _stateMachine.Update();

            // �X�V����
            _disposable = Observable.EveryUpdate().Subscribe(_ => _stateMachine.Update());
        }
        public void Pause() => _stateMachine.SendEvent(StateEvent.Pause);
        public void UnPause() => _stateMachine.SendEvent(StateEvent.UnPause);
        public void Cancel(CancelResult cancelResult) {
            _stateMachine.SendEvent(StateEvent.Cancel);

            // ���ʃf�[�^�̊i�[
            _resultData = cancelResult ?? new CancelResult();
        }


        /// ----------------------------------------------------------------------------
        // Protected Method 

        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnPause() { Debug_.Log("Pause", Colors.Orange); }
        protected virtual void OnUnPause() { Debug_.Log("Un Pause", Colors.Orange); }
        protected virtual void OnEnd() { }

        /// <summary>
        /// �v���Z�X�����C�x���g�̔��΁i���h���N���X�p�j
        /// </summary>
        protected void TriggerComplete(CompleteResult result) {
            _stateMachine.SendEvent(StateEvent.Complete);

            // ���ʃf�[�^�̊i�[
            _resultData = result;
        }


        /// ----------------------------------------------------------------------------
        #region Inner State

        // �J�ڃC�x���g        
        private enum StateEvent {
            // �|�[�Y
            Pause,
            UnPause,
            // �I��
            Complete,
            Cancel,
        }

        private abstract class StateBase : ImtStateMachine<ProcessBase, StateEvent>.State { }

        /// <summary>
        /// ���s�X�e�[�g
        /// </summary>
        private sealed class RunningState : StateBase {
            private bool isFirstEnter = true;

            protected override void Enter() {
                if (isFirstEnter) {
                    Context.OnStart();
                    isFirstEnter = false;
                }
            }
            protected override void Update() => Context.OnUpdate();
        }

        /// <summary>
        /// �|�[�Y�X�e�[�g
        /// </summary>
        private sealed class PauseState : StateBase {
            protected override void Enter() {
                Context.OnPause();
            }
            protected override void Exit() {
                Context.OnUnPause();
            }
        }

        /// <summary>
        /// �I���X�e�[�g
        /// </summary>
        private sealed class EndState : StateBase {
            protected override void Enter() {
                Context.OnEnd();
                // �I���ʒm
                Debug_.Log($" Result : {Context._resultData.GetType()}", Colors.Orange);
                Context._finishedSource.TrySetResult(Context._resultData);
            }
        }

        #endregion
    }

}
