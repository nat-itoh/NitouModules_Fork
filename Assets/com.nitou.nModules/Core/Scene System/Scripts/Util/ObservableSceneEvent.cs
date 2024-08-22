using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UniRx;

// [�Q�l]
//  qiita: �V�[���̓ǂݍ��݃C�x���g��IObservable�ɂ��� https://qiita.com/su10/items/93977e0b95449ec1b944
//  qiita: UniRx��Observable.FromEvent���g�� https://qiita.com/ShirakawaMaru/items/4071aad0937ecbdc7fe9

namespace nitou.SceneSystem {

    /// <summary>
    /// <see cref="SceneManager"/>�̃C�x���g��Obserbable�ɕϊ����郉�C�u����
    /// </summary>
    public static class ObservableSceneEvent {

        /// <summary>
        /// "activeSceneChanged"�C�x���g��Observable�ɕϊ�����
        /// </summary>
        public static IObservable<Tuple<Scene, Scene>> ActiveSceneChangedAsObservable() {
            return Observable.FromEvent<UnityAction<Scene, Scene>, Tuple<Scene, Scene>>(
                h => (x, y) => h(Tuple.Create(x, y)),
                h => SceneManager.activeSceneChanged += h,
                h => SceneManager.activeSceneChanged -= h
                );
        }

        /// <summary>
        /// "sceneLoaded"�C�x���g��Observable�ɕϊ�����
        /// </summary>
        public static IObservable<Tuple<Scene, LoadSceneMode>> SceneLoadedAsObservable() {
            return Observable.FromEvent<UnityAction<Scene, LoadSceneMode>, Tuple<Scene, LoadSceneMode>>(
                h => (x, y) => h(Tuple.Create(x, y)),
                h => SceneManager.sceneLoaded += h,
                h => SceneManager.sceneLoaded -= h
            );
        }

        /// <summary>
        /// "sceneUnloaded"�C�x���g��Observable�ɕϊ�����
        /// </summary>
        public static IObservable<Scene> SceneUnloadedAsObservable() {
            return Observable.FromEvent<UnityAction<Scene>, Scene>(
                h => h.Invoke,
                h => SceneManager.sceneUnloaded += h,
                h => SceneManager.sceneUnloaded -= h
            );
        }
    }
}
