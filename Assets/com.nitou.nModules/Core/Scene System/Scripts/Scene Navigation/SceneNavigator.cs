using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

// [�Q�l]
//  �R�K�l�u���O: �V�[�����ǂݍ��܂�Ă��邩�m�F����֐� https://baba-s.hatenablog.com/entry/2022/11/28/162515
//  �R�K�l�u���O: ���ݓǂݍ��܂�Ă��邷�ׂẴV�[�����擾����֐� https://baba-s.hatenablog.com/entry/2022/11/28/162103
//  qiita: �V�[���̏d���ǂݍ��݂�LINQ�Ŗh�� https://qiita.com/segur/items/b13045e6f3a9949e0503

namespace nitou.SceneSystem {

    /// <summary>
    /// EntryPoint�֘A�̏�����ǉ�����SceneManager�̃��b�v�N���X
    /// </summary>
    public static class SceneNavigator {

        /// <summary>
        /// ���������������Ă��邩�ǂ���
        /// </summary>
        public static bool IsInitialized { get; private set; }

        /// <summary>
        /// �e�X�g���s�i�����[�g�V�[���ȊO������s����Ă���j���ǂ���
        /// </summary>
        public static bool IsTestRun { get; private set; }


        /// ----------------------------------------------------------------------------
        #region Shared Data

        // �V�[���ԋ��L�f�[�^
        private static readonly Dictionary<Type, SceneSharedData> _sharedData = new();

        /// <summary>
        /// ���L�f�[�^��o�^����
        /// </summary>
        public static void SetData<TData>(TData data)
            where TData : SceneSharedData {

            var type = typeof(TData);
            if (_sharedData.ContainsKey(type)) {
                Debug_.LogWarning("�o�^����Ă���f�[�^���㏑�����܂��D");
            }
            _sharedData[type] = data;
        }

        /// <summary>
        /// ���L�f�[�^���폜����
        /// </summary>
        public static void CleaData<TData>() 
            where TData : SceneSharedData{

            var type = typeof(TData);
            if (!_sharedData.ContainsKey(type)) {
                Debug_.LogWarning("�o�^����Ă���f�[�^�͂���܂���D");
                return;
            }

            _sharedData.Remove(type);
        }

        /// <summary>
        /// ���L�f�[�^���擾����
        /// </summary>
        public static bool TryGetData<TData>(out TData data)
            where TData : SceneSharedData {

            var type = typeof(TData);
            if (!_sharedData.ContainsKey(type)) {
                Debug_.LogWarning("�w�肳�ꂽ�f�[�^�͖��o�^�ł��D");
                data = null;
                return false;
            }

            data = _sharedData[type] as TData;
            return true;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        // Public Methord

        /// <summary>
        /// �G���g���[�|�C���g���擾����
        /// </summary>
        public static bool TryGetEntryPoint(this Scene scene, out ISceneEntryPoint entryPoint) {
            return scene.TryGetComponentInScene(out entryPoint);
        }

        /// <summary>
        /// �G���g���[�|�C���g���擾����
        /// </summary>
        public static bool TryGetEntryPoint<TEntryPoint>(this Scene scene, out TEntryPoint entryPoint)
            where TEntryPoint : ISceneEntryPoint {
            return scene.TryGetComponentInScene(out entryPoint);
        }


        /// ----------------------------------------------------------------------------
        // Public Methord (�ėp)

        /// <summary>
        /// �V�[�����ǂݍ��܂�Ă��邩�m�F����
        /// </summary>
        public static bool IsLoaded(string sceneName) => 
            GetAllScenes().Any(x => x.name == sceneName && x.isLoaded);

        /// <summary>
        /// �A�N�e�B�u�ȃV�[�����擾����
        /// </summary>
        public static Scene GetActiveScene() {
            return SceneManager.GetActiveScene();
        }

        /// <summary>
        /// ���ݓǂݍ��܂�Ă���S�V�[�����擾����
        /// </summary>
        public static IEnumerable<Scene> GetAllScenes() {
            // �� SceneManager.GetAllScenes()�͋��`���̂��ߎg�p���Ȃ��D

            var sceneCount = SceneManager.sceneCount;
            for (var i = 0; i < sceneCount; i++) {
                yield return SceneManager.GetSceneAt(i);
            }
        }


        /// ----------------------------------------------------------------------------
        // Public Methord

        /// <summary>
        /// �V�[����ǂݍ��ށD
        /// </summary>
        public static async UniTask<Scene> LoadSceneAsync(string sceneName, bool setActive = false, bool disallowSameScene = true) {

            // ���ɓǂݍ��܂�Ă���ꍇ,
            if (disallowSameScene && IsLoaded(sceneName)) {
                Debug_.LogWarning($"Scene [{sceneName}] is alredy loaded.");
                return SceneManager.GetSceneByName(sceneName);
            }

            // �V�[���̓ǂݍ���
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            var scene = SceneManager.GetSceneByName(sceneName);


            // �A�N�e�B�u�ȃV�[���ɐݒ肷��
            if (setActive) {
                SceneManager.SetActiveScene(scene);
            }
            return scene;
        }

        /// <summary>
        /// �V�[�����擾����D���݂��Ȃ��ꍇ�͓ǂݍ���Ŏ擾����
        /// </summary>
        public static async UniTask<Scene> GetOrLoadSceneAsync(string sceneName, bool setActive = false) {

            // �V�[���ǂݍ���
            if (!IsLoaded(sceneName)) {
                await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }

            var scene = SceneManager.GetSceneByName(sceneName);

            // �A�N�e�B�u�ȃV�[���ɐݒ肷��
            if (setActive) {
                SceneManager.SetActiveScene(scene);
            }
            return scene;
        }

        /// <summary>
        /// �V�[�����������D
        /// </summary>
        public static UniTask UnLoadSceneAsync(string sceneName) {
            return SceneManager.UnloadSceneAsync(sceneName).ToUniTask();
        }

        /// <summary>
        /// �V�[�����������D
        /// </summary>
        public static UniTask UnLoadSceneAsync(Scene scene) {
            return SceneManager.UnloadSceneAsync(scene).ToUniTask();
        }


        /// ----------------------------------------------------------------------------
        // Private Methord

        internal static void RuntimeInitialize() {
            if (IsInitialized) return;

            //Debug_.Log("RuntimeInitialize");

            // �J�n�V�[���̏���
            foreach (var scene in GetAllScenes()) {

                // �ǂݍ��݃C�x���g
                if (scene.TryGetEntryPoint(out var entryPoint)) {
                    entryPoint.OnSceneLoadAsync();
                }
            }

            {
                // �A�N�e�B�u���C�x���g
                var activeScene = SceneManager.GetActiveScene();
                if (activeScene.TryGetEntryPoint(out var entryPoint)) {
                    entryPoint.OnSceneActivateAsync();
                }
            }

            // --- 

            // �V�[���ǂݍ��ݎ��̃C�x���g�o�^
            ObservableSceneEvent.SceneLoadedAsObservable()
                .Subscribe(x => {
                    Debug_.Log($"Scene Loaded : [{x.Item1.name}] (LoadType: {x.Item2})", Colors.AliceBlue);

                    // �C�x���g����
                    if (x.Item1.TryGetEntryPoint(out var entry)) {
                        entry.OnSceneLoadAsync();
                    }
                });

            // �V�[��������̃C�x���g�o�^
            ObservableSceneEvent.SceneUnloadedAsObservable()
                .Subscribe(x => {
                    Debug_.Log($"Scene Unloaded : [{x.name}] ", Colors.AliceBlue);

                    // �C�x���g����
                    if (x.TryGetEntryPoint(out var entry)) {
                        entry.OnSceneUnloadAsync();
                    }
                });

            // �A�N�e�B�u�V�[���؂�ւ����̃C�x���g�o�^
            ObservableSceneEvent.ActiveSceneChangedAsObservable()
                .Subscribe(x => {
                    var previousScene = x.Item1;
                    var nextScene = x.Item2;

                    // �C�x���g����
                    ISceneEntryPoint entry;
                    if (previousScene.IsValid() && previousScene.TryGetEntryPoint(out entry)) {
                        entry.OnSceneDeactivateAsync();
                    }
                    if (nextScene.TryGetEntryPoint(out entry)) {
                        entry.OnSceneActivateAsync();
                    }

                    Debug_.Log($"Scene Changed : [{x.Item1.name}] => [{x.Item2.name}]", Colors.AliceBlue);
                });

            // �t���O�X�V
            IsInitialized = true;
        }
    }

}