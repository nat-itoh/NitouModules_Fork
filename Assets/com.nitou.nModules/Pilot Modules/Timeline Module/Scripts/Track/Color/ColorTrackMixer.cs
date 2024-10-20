using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace nitou.Timeline {

    public class ColorTrackMixer : PlayableBehaviour {

        private Renderer _renderer = null;
        private Material _originalMat = null;
        private Material _newMat = null;


        /// ----------------------------------------------------------------------------
        // Override Method (���s���̏���)


        public override void OnBehaviourPause(Playable playable, FrameData info) {
            if (_newMat != null) Object.DestroyImmediate(_newMat);
            if (_renderer != null) _renderer.material = _originalMat;
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData) {

            var renderer = playerData as Renderer;
            if (renderer == null) return;

            if (_newMat == null) {
                // �������̋L�^
                _renderer = renderer;
                _originalMat = renderer.sharedMaterial;

                // �}�e���A������
                _newMat = new Material(renderer.sharedMaterial);
                renderer.material = _newMat;
            }

            // �u�����h�J���[�̐���
            var color = Color.clear;
            for (int i = 0; i < playable.GetInputCount(); i++) {
                
                var sp = (ScriptPlayable<ColorClipBehaviour>)playable.GetInput(i);
                
                var behaviour = sp.GetBehaviour();
                var weight = playable.GetInputWeight(i);
                color += behaviour.OutputColor * weight;
            }

            _newMat.color = color;

        }

    }
}
