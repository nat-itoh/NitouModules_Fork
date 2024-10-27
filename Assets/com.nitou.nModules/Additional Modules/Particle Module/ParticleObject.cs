using System;
using UniRx;
using UnityEngine;

// [�Q�l]
//  Hatena: �p�[�e�B�N���ė��p�N���X https://www.stmn.tech/entry/2016/03/01/004816 
//  qiita: UniRx��ObjectPool���g����ParticleSystem���Ǘ����� https://qiita.com/KeichiMizutani/items/fc22a6037447d840adc2
//  kan�̃�����: UniRx�ŃI�u�W�F�N�g�v�[��(ObjectPool)���ȒP���� https://kan-kikuchi.hatenablog.com/entry/UniRx_ObjectPool

namespace nitou.ParticleModule {

    /// <summary>
    /// �p�[�e�B�N�����Đ����邽�߂̃��b�p�[�R���|�[�l���g
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    [DisallowMultipleComponent]
    public sealed class ParticleObject : MonoBehaviour {

        private ParticleSystem particle;


        /// ----------------------------------------------------------------------------
        // LifeCycle Events 

        private void Awake() {
            particle = GetComponent<ParticleSystem>();
        }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// �p�[�e�B�N�����Đ�����
        /// </summary>
        public IObservable<Unit> PlayParticle(Vector3 position) {
            transform.position = position;
            particle.Play();

            // ParticleSystem��startLifetime�ɐݒ肵���b�����o������I���ʒm
            return Observable.Timer(TimeSpan.FromSeconds(particle.main.startLifetimeMultiplier))
                .ForEachAsync(_ => particle.Stop());
        }

        /// <summary>
        /// �p�[�e�B�N�����Đ�����
        /// </summary>
        public IObservable<Unit> PlayParticle(Vector3 position, Quaternion rotation) {
            transform.position = position;
            transform.rotation = rotation;
            particle.Play();

            // ParticleSystem��startLifetime�ɐݒ肵���b�����o������I���ʒm
            return Observable.Timer(TimeSpan.FromSeconds(particle.main.startLifetimeMultiplier))
                .ForEachAsync(_ => particle.Stop());
        }

        /// <summary>
        /// �p�[�e�B�N�����Đ�����(���C�x���g���s)
        /// </summary>
        public IObservable<Unit> PlayParticleWithEvent(Vector3 position) {
            transform.position = position;
            particle.Play();

            var particleEvent = GetComponent<IParticleLifeCycleEvent>();
            particleEvent?.OnPlayed();  // ���J�n�C�x���g

            // ParticleSystem��startLifetime�ɐݒ肵���b�����o������I���ʒm
            return Observable.Timer(TimeSpan.FromSeconds(particle.main.startLifetimeMultiplier))
                .ForEachAsync(_ => { 
                    particle.Stop();
                    particleEvent?.OnStopped();    // ���I���C�x���g
                });
        }

        /// <summary>
        /// �p�[�e�B�N�����Đ�����(���C�x���g���s)
        /// </summary>
        public IObservable<Unit> PlayParticleWithEvent(Vector3 position, Quaternion rotation) {
            transform.position = position;
            transform.rotation = rotation;
            particle.Play();

            var particleEvent = GetComponent<IParticleLifeCycleEvent>();
            particleEvent?.OnPlayed();  // ���J�n�C�x���g

            // ParticleSystem��startLifetime�ɐݒ肵���b�����o������I���ʒm
            return Observable.Timer(TimeSpan.FromSeconds(particle.main.startLifetimeMultiplier))
                .ForEachAsync(_ => {
                    particle.Stop();
                    particleEvent?.OnStopped();    // ���I���C�x���g
                });
        }
    }
}