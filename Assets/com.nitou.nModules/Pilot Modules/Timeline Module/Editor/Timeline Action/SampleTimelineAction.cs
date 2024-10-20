#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEditor.Timeline.Actions;
using UnityEditor.ShortcutManagement;

// [�Q�l]
//  qiita: Unity Timeline 1.4�Œǉ����ꂽTimelineAction�̎g���� https://qiita.com/jukey17/items/3eefd32e527080d5bcb6
//  Doqument: Class TimelineAction https://docs.unity3d.com/Packages/com.unity.timeline@1.8/api/UnityEditor.Timeline.Actions.TimelineAction.html

namespace nitou.Timeline {

    /// <summary>
    /// TimelineAction���������߂̃e�X�g�N���X
    /// </summary>
    [MenuEntry("Custom Action/Sample Timeline Action")]
    public class SampleTimelineAction : TimelineAction {

        public override ActionValidity Validate(ActionContext context) {
            return ActionValidity.Valid;
        }

        public override bool Execute(ActionContext context) {
            Debug.Log("Test timeline action");
            return true;
        }


        [TimelineShortcut("SampleTimelineAction", KeyCode.Q)]
        public static void HandleShortCut(ShortcutArguments  args) {
            Invoker.InvokeWithSelected<SampleTimelineAction>();
        }
    }
}
#endif



