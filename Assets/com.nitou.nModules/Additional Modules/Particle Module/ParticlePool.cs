using UniRx;
using UniRx.Toolkit;
using UnityEngine;

// [�Q�l]
//  Hatena: �p�[�e�B�N���ė��p�N���X https://www.stmn.tech/entry/2016/03/01/004816 
//  qiita: UniRx��ObjectPool���g����ParticleSystem���Ǘ����� https://qiita.com/KeichiMizutani/items/fc22a6037447d840adc2
//  kan�̃�����: UniRx�ŃI�u�W�F�N�g�v�[��(ObjectPool)���ȒP���� https://kan-kikuchi.hatenablog.com/entry/UniRx_ObjectPool

namespace nitou.ParticleModule {

    /// <summary>
    /// �p�[�e�B�N���̃I�u�W�F�N�g�v�[��
    /// </summary>
    public sealed class ParticlePool : ObjectPool<ParticleObject> {

        private ParticleObject _prefab;                 // �v���n�u
        private readonly Transform _parentTransform;    // ���������I�u�W�F�N�g�̐e

        /// <summary>
        /// �p�[�e�B�N����
        /// </summary>
        public string ParticleName => _particleName;
        private readonly string _particleName;


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// �R���X�g���N�^
        public ParticlePool(Transform transform, ParticleObject origin, string particleName = "") {
            this._parentTransform = transform;
            this._prefab = origin;
            this._particleName = particleName;
        }


        /// ----------------------------------------------------------------------------
        // Override Method 

        protected override ParticleObject CreateInstance() {
            if (_prefab == null) SetDefalutPrefab();
            return Object.Instantiate(_prefab, _parentTransform, true);
        }

        protected override void OnBeforeRent(ParticleObject instance) {
            Debug.Log($"{instance.name}���v�[��������o����܂���");
            base.OnBeforeRent(instance);
        }

        protected override void OnBeforeReturn(ParticleObject instance) {
            Debug.Log($"{instance.name}���v�[���ɖ߂���܂���");
            base.OnBeforeReturn(instance);
        }

        protected override void OnClear(ParticleObject instance) {
            base.OnClear(instance);
        }


        /// ----------------------------------------------------------------------------
        // Private Method 

        /// <summary>
        /// �f�t�H���g�̃p�[�e�B�N����o�^
        /// </summary>
        private void SetDefalutPrefab() {
            var obj = new GameObject($"Defalut Particle");
            var particle = obj.AddComponent<ParticleSystem>();
            _prefab = obj.AddComponent<ParticleObject>();
        }

    }

}












//// �v�[������I�u�W�F�N�g���擾����O�Ɏ��s�����
//protected override void OnBeforeRent(ParticleObject instance) {
//    Debug.Log($"{instance.name}���v�[��������o����܂���");
//    base.OnBeforeRent(instance);
//}

//// �I�u�W�F�N�g���v�[���ɖ߂�O�Ɏ��s�����
//protected override void OnBeforeReturn(ParticleObject instance) {
//    Debug.Log($"{instance.name}���v�[���ɖ߂���܂���");
//    base.OnBeforeReturn(instance);
//}