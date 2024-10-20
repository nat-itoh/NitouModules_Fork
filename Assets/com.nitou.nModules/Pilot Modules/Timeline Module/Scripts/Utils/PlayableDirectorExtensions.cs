using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// [�Q�l]
//  qiita: PlayableDirector�̍Đ���await����g�����\�b�h https://qiita.com/rarudonet/items/f7fc7453ec1c6126af38
//  qiita: PlayableDirector�̃C�x���g��Observable������ https://qiita.com/Teach/items/a4d669aabcad19011b07
//  �䂢�u��: Timeline��Script���爵���Ƃ���Tips https://www.yui-tech-blog.com/entry/timeline-script-tips/


namespace nitou.Timeline{

    public static class PlayableDirectorExtensions{

        /// <summary>
        /// �I����ҋ@�ł���Đ����\�b�h
        /// </summary>
        public static UniTask PlayAsync(this PlayableDirector self) {
            self.Play();

            // �|�[�Yor�I���܂őҋ@
            return UniTask.WaitWhile(() => self.state == PlayState.Playing);
        }




        public static IObservable<PlayableDirector> PlayedAsObservable(this PlayableDirector self) {
            return Observable.FromEvent<PlayableDirector>(
                h => self.played += h,
                h => self.played -= h);
        }

        public static IObservable<PlayableDirector> PausedAsObservable(this PlayableDirector self) {
            return Observable.FromEvent<PlayableDirector>(
                h => self.paused += h,
                h => self.paused -= h);
        }

        public static IObservable<PlayableDirector> StoppedAsObservable(this PlayableDirector self) {
            return Observable.FromEvent<PlayableDirector>(
                h => self.stopped += h,
                h => self.stopped -= h);
        }

        // [����]
        // Stopped��"WrapMode"��None�ł����,�u�Đ��I�����v�܂��́uGameObject����Destroy�������v�ɌĂ΂��D
        // �������C�R���|�[�l���g��enabled==false�ɂȂ����Ƃ��ɂ͌Ă΂�Ȃ��i���f�����j�D







    }
}
