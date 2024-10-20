#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEditor.Timeline;

// [�Q�l]
//  qiita: Unity2019.2����g����悤�ɂȂ���ClipEditor�ŃN���b�v�̌����ڂ��g������ https://qiita.com/jukey17/items/e4db4028aeb650819bc5
//  LIGHT11: Timeline��Clip�̌����ڂ��g������ https://light11.hatenadiary.com/entry/2021/08/31/192405

namespace nitou.Timeline.Editor {

    [CustomTimelineEditor(typeof(ColorClip))]
    public class ColorClipEditor : ClipEditor {

        // ��Editor��TimelineAsset���̑S�g���b�N�ŋ��ʂ̂��߁Cdictionay�ŊǗ�����
        Dictionary<ColorClip, Texture2D> _textures = new();


        /// ----------------------------------------------------------------------------
        // ClipEditor Method

        public override void DrawBackground(TimelineClip clip, ClipBackgroundRegion region) {
            var tex = GetGradientTexture(clip);
            if (tex != null) GUI.DrawTexture(region.position, tex);
        }

        public override ClipDrawOptions GetClipOptions(TimelineClip clip) {
            var options = base.GetClipOptions(clip);
            options.highlightColor = Color.clear;
            options.displayClipName = false;
            return options;
        }

        public override void OnClipChanged(TimelineClip clip) {
            GetGradientTexture(clip, true);
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// 
        /// </summary>
        private Texture2D GetGradientTexture(TimelineClip clip, bool update = false) {
            Texture2D tex = Texture2D.whiteTexture;

            var customClip = clip.asset as ColorClip;
            if (customClip == null) return tex;

            var gradient = customClip.gradient;
            if (gradient == null) return tex;

            if (update) {
                _textures.Remove(customClip);
            } else {
                _textures.TryGetValue(customClip, out tex);
                if (tex != null) return tex;
            }

            // �e�N�X�`������
            var b = (float)(clip.blendInDuration / clip.duration);
            tex = new Texture2D(128, 1);
            for (int i = 0; i < tex.width; ++i) {
                var t = (float)i / tex.width;
                var color = customClip.gradient.Evaluate(t);
                if (b > 0f) color.a = Mathf.Min(t / b, 1f);     // ���u�����h�����̓A���t�@�ŃI�[�o�[���b�v
                tex.SetPixel(i, 0, color);
            }
            tex.Apply();
            _textures.Add(customClip, tex);

            return tex;
        }
    }
}

#endif