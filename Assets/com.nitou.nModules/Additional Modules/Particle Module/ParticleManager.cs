using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

// [�Q�l]
//  Hatena: �p�[�e�B�N���ė��p�N���X https://www.stmn.tech/entry/2016/03/01/004816 
//  qiita: UniRx��ObjectPool���g����ParticleSystem���Ǘ����� https://qiita.com/KeichiMizutani/items/fc22a6037447d840adc2
//  kan�̃�����: UniRx�ŃI�u�W�F�N�g�v�[��(ObjectPool)���ȒP���� https://kan-kikuchi.hatenablog.com/entry/UniRx_ObjectPool


namespace nitou.ParticleModule {
    using nitou.DesignPattern;

    /// <summary>
    /// �p�[�e�B�N�����Ǘ�����O���[�o���}�l�[�W���D
    /// �������ȃv���W�F�N�g�܂��̓v���g�^�C�v�Ƃ��Ă̎g�p��z��
    /// </summary>
    public class ParticleManager : SingletonMonoBehaviour<ParticleManager> {

        // �e�p�[�e�B�N���v�[���̃��X�g
        private List<ParticlePool> _poolList = new ();
        private List<GameObject> _containers = new ();

        // ���\�[�X���
        private const string RESOUCE_PATH = "Particles/World/";     


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        protected override void Awake() {
            if (!base.CheckInstance()) {
                Destroy(this.gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnDestroy() {
            // �j�����ꂽ�Ƃ��iDispose���ꂽ�Ƃ��j��ObjectPool���������
            ClearList();
            _poolList = null;
            _containers = null;
        }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// �w�肵�����O�̃p�[�e�B�N���Đ�
        /// �����߂čĐ�����p�[�e�B�N���̓v�[���p�I�u�W�F�N�g�𐶐�
        /// </summary>
        public void PlayParticle(string particleName, Vector3 position, Quaternion rotation) {
            //���X�g����w�肵�����O�̃v�[���p�I�u�W�F�N�g���擾
            ParticlePool pool = _poolList.Where(p => p.ParticleName == particleName).FirstOrDefault();

            // �v�[�����������̏ꍇ�C
            if (pool == null) {
                // �i�[�p�̐e�I�u�W�F�N�g�𐶐� (���f�o�b�O�����p)
                var parentObj = new GameObject($"Pool [{particleName}]");
                parentObj.SetParent(this.transform);
                _containers.Add(parentObj);

                // �������̃I�u�W�F�N�g���擾
                var prefab = LoadOrCreateOrigin(particleName).GetOrAddComponent<ParticleObject>();

                // �v�[���̐���
                pool = new ParticlePool(parentObj.transform, prefab, particleName);
                _poolList.Add(pool);
            }

            // ObjectPool����1�擾
            var effect = pool.Rent();

            // �G�t�F�N�g���Đ����A�Đ��I��������pool�ɕԋp����
            effect.PlayParticle(position, rotation)
                .Subscribe(__ => { pool.Return(effect); });
        }

        /// <summary>
        /// �w�肵�����O�̃p�[�e�B�N���Đ� (�I�[�o�[���[�h)
        /// </summary>
        public void PlayParticle(string particleName, Vector3 position) =>
            PlayParticle(particleName, position, Quaternion.identity);

        /// <summary>
        /// �w�肵�����O�̃p�[�e�B�N���Đ�
        /// </summary>
        public void PlayParticleWithEvent(string particleName, Vector3 position, Quaternion rotation) {
            //���X�g����w�肵�����O�̃v�[���p�I�u�W�F�N�g���擾
            ParticlePool pool = _poolList.Where(p => p.ParticleName == particleName).FirstOrDefault();

            // �v�[�����������̏ꍇ�C
            if (pool == null) {
                // �i�[�p�̐e�I�u�W�F�N�g�𐶐� (���f�o�b�O�����p)
                var parentObj = new GameObject($"Pool [{particleName}]");
                parentObj.SetParent(this.transform);
                _containers.Add(parentObj);

                // �������̃I�u�W�F�N�g���擾
                var prefab = LoadOrCreateOrigin(particleName).GetOrAddComponent<ParticleObject>();

                // �v�[���̐���
                pool = new ParticlePool(parentObj.transform, prefab, particleName);
                _poolList.Add(pool);
            }

            // ObjectPool����1�擾
            var effect = pool.Rent();

            // �G�t�F�N�g���Đ����A�Đ��I��������pool�ɕԋp����
            effect.PlayParticleWithEvent(position, rotation)
                .Subscribe(__ => { pool.Return(effect); });
        }

        /// <summary>
        /// �w�肵�����O�̃p�[�e�B�N���Đ� (�I�[�o�[���[�h)
        /// </summary>
        public void PlayParticleWithEvent(string particleName, Vector3 position) =>
            PlayParticleWithEvent(particleName, position, Quaternion.identity);

        /// <summary>
        /// �I�u�W�F�N�g�v�[���̃��X�g���
        /// </summary>
        public void ClearList() {

            _poolList.ForEach(p => p.Dispose());
            _poolList.Clear();

            _containers.ForEach(o => o.Destroy());
            _containers.Clear();
        }


        /// ----------------------------------------------------------------------------
        // Private Method 

        /// <summary>
        /// �������̃I�u�W�F�N�g�����[�h
        /// �����[�h���s���̓f�t�H���g�̃p�[�e�B�N���𐶐�
        /// </summary>
        private GameObject LoadOrCreateOrigin(string particleName) {
            // ���\�[�X�̓ǂݍ���
            var origin = Resources.Load(RESOUCE_PATH + particleName) as GameObject;

            if (origin == null) {   // ----- ���s�����ꍇ�C
                // ���_�~�[�����Ă���
                origin = new GameObject($"Defalut Particle");
                Debug.Log($"[{particleName}]�Ƃ����p�[�e�B�N���̓ǂݍ��݂Ɏ��s���܂���");

            } else {                // ----- ���������ꍇ�C
                origin.name = particleName;
            }
            return origin;
        }

    }
}