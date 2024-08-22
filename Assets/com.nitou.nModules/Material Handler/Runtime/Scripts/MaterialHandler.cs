using System;
using UnityEngine;

// [�Q�l]
//  qiita: 3D�I�u�W�F�N�g�̎c������ https://qiita.com/madoramu_f/items/fada99645cd03fd7f515
//  UnityIndies: �}�e���A���A�������ĂȂ��Ƃ����Ƀ��������[�N https://www.create-forever.games/unity-material-memory-leak/
//  Hatena: Renderer.material�Ŏ擾�����}�e���A���͎����Ŕj�����Ȃ��ƃ��[�N����b https://light11.hatenadiary.com/entry/2019/11/03/223241

namespace nitou.MaterialControl {

    /// <summary>
    /// �}�e���A���̃v���p�e�B����p���b�p�[�N���X
    /// </summary>
    public abstract class MaterialHandler : IDisposable , INormalizedValueTicker{

        protected readonly Shader _shader = null;
        protected Material _material = null;

        private NormalizedValue _rate;

        /// <summary>
        /// �}�e���A���ϐ����ꊇ���삷�邽�߂̃v���p�e�B
        /// </summary>
        public NormalizedValue Rate {
            get => _rate;
            set {
                _rate = value;
                OnRateChanged(_rate);
            }
        }

        /// <summary>
        /// ���C���J���[
        /// </summary>
        public Color Color {
            get => _material.color;
            set => _material.color = value;
        }


        /// ----------------------------------------------------------------------------
        // Public Method 

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public MaterialHandler(Shader shader) {
            if (shader == null) throw new ArgumentNullException(nameof(shader));

            // �}�e���A������
            _shader = shader;
            _material = new Material(_shader);
        }

        /// <summary>
        /// �I������
        /// </summary>
        public void Dispose() {
            if (_material == null) return;
            GameObject.Destroy(_material);
            _material = null;
        }


        /// ----------------------------------------------------------------------------
        // Public Method (��{����)

        /// <summary>
        /// �����_���[�Ƀ}�e���A����K�p����
        /// </summary>
        public void OnApplayMaterial(Renderer renderer) {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            renderer.sharedMaterial = _material;
        }

        /// <summary>
        /// �e�N�X�`����ݒ肷��
        /// </summary>
        public void SetMainTex(Texture texture) {
            _material.mainTexture = texture;
        }

        /// <summary>
        /// �J���[��ݒ肷��
        /// </summary>
        public void SetMainColor(Color color) {
            _material.color = color;
        }


        /// ----------------------------------------------------------------------------
        // Protected Method 

        /// <summary>
        /// �ꊇ�v���p�e�B���ω������Ƃ��̏���
        /// </summary>
        protected virtual void OnRateChanged(float rate) { }
    }


    public static partial class RendererExtensions {

        /// <summary>
        /// �����_���[�Ƀ}�e���A����K�p����g�����\�b�h
        /// </summary>
        public static void SetSharedMaterial(this Renderer self, MaterialHandler handler) {
            handler.OnApplayMaterial(self);
        }
    }
}