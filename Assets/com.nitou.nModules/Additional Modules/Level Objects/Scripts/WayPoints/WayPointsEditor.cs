#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.AnimatedValues;

// [�Q�l]
//  Zenn: Vector3��Enum��SceneView��ŕҏW�ł���悤�ɂ��� https://zenn.dev/kd_gamegikenblg/articles/30b2b1139b213c
//  qiita: Unity Editor�̊g�� Vector3�̍��W���V�[������ύX�ł���悤�ɂ��� https://qiita.com/RYA234/items/13d98a49e291ee2028d7

namespace nitou.LevelObjects.EditorScripts {
    using nitou.EditorShared;

    [CustomEditor(typeof(WayPoints))]
    public class WayPointsEditor : Editor {

        // ����Ώ�
        private static WayPoints _instance = null;

        // �`��p
        private AnimBool _animBool;
        private AnimBool _animBool2;


        /// ----------------------------------------------------------------------------
        // Editor Method

        private void OnEnable() {
            _instance = target as WayPoints;

            _animBool = new AnimBool(false);
            _animBool.valueChanged.AddListener(Repaint);

            _animBool2 = new AnimBool(false);
            _animBool2.valueChanged.AddListener(Repaint);
        }

        private void OnDisable() {
            _instance = null;
        }


        public override void OnInspectorGUI() {
            DrawDefaultInspector();


            // -----
            using (var group = new EditorUtil.GUI.FoldoutGroupScope("Title", _animBool)) {
                if (group.Visible) {

                    EditorGUILayout.LabelField("Test a");
                    EditorGUILayout.LabelField("Test b");
                    EditorGUILayout.LabelField("Test v");
            
                }
            }

            // -----
            using (var group = new EditorUtil.GUI.FoldoutGroupScope("Title 2", _animBool2)) {
                if (group.Visible) {

                    EditorGUILayout.LabelField("Test a");
                    EditorGUILayout.LabelField("Test b");
                    EditorGUILayout.LabelField("Test v");

                }
            }
        }

        private void OnSceneGUI() {
            if (_instance == null) return;

            // [�Q�l]
            //  LIGHT11: HandleCap���N���b�N���ꂽ���Ƃ��擾���� https://light11.hatenadiary.com/entry/2018/03/20/003345
            //  StackOverflow: �G�f�B�^�[�n���h����I�����ăv���p�e�B�C���X�y�N�^�[�E�B���h�E��\��������@ https://stackoverflow.com/questions/51238340/how-to-make-editor-handles-selectable-to-display-properties-inspector-window

            _instance.Targets.ForEach((point, index) => {

                bool isSelectedBox = (point == _instance.Selected);

                int controlId = GUIUtility.GetControlID(FocusType.Passive);
                var capSize = HandleUtility.GetHandleSize(_instance.transform.TransformPoint(point.position)) * 0.2f;

                // ���X�R�[�v����Handle���\�b�h�̓��[�J�����W�Ŏw��ł���
                using (new Handles.DrawingScope(_instance.transform.localToWorldMatrix)) {

                    // �I��p�n���h���L���b�v�̕`��
                    if (Event.current.type.IsRepaintOrLayout()) {
                        var color = isSelectedBox ? Colors.Orange : Colors.White;
                        using (new Handles.DrawingScope(color)) {
                            Handles.SphereHandleCap(controlId, point.position, Quaternion.identity, capSize, Event.current.type);
                        }
                    }

                    // �I���A�C�e���̍X�V
                    else if (Event.current.type == EventType.MouseDown) {
                        // �N���b�N���������ɂ�����̂�ControlId����v������I�𒆂Ƃ���
                        //if (controlId == EditorGUIUtility.hotControl) {
                        if (controlId == HandleUtility.nearestControl) {
                            _instance.Select(point);

                            // Editor Tool�̑I��
                            ToolManager.SetActiveTool(typeof(MoveTool));
                        }
                    }

                    // ���_����I���A�C�e���܂ł̔j��
                    if (isSelectedBox) {
                        using (new Handles.DrawingScope(Colors.GreenYellow)) {
                            Handles.DrawDottedLine(Vector3.zero, point.position, 4);
                        }
                    }

                    // ���x��
                    Styles.label.normal.textColor = point.GetColor();
                    Handles.Label(point.position, $"\n\n{index}: {point.tag}", Styles.label);
                }
            });

            /*

            // �j��
            using (new Handles.DrawingScope(_instance.transform.localToWorldMatrix)) {
                for(int i=0; i< _instance.Count; i++) {
                    var point = _instance.ta

                }
                Handles.DrawDottedLines(_instance.Targets.Select(p => p.position).ToArray(), 4);
            }

            // Others
            for(int i=0; i< _instance.Points.Count; i++) {
                var wayPoint = _instance.Points[i];

                // Lavel
                var style = new GUIStyle();
                style.normal.textColor = wayPoint.GetColor();
                style.fontSize = 14;
                Handles.Label(wayPoint.position, $"\n\n{i}: {wayPoint.tag}", style);

                // 
                Handles.BeginGUI();

                //var screenPos = HandleUtility.WorldToGUIPointWithDepth(wayPoint.position);
                var screenPos = new Vector2();
                EditorGUI.BeginChangeCheck();
                var rect = new Rect(screenPos.x, screenPos.y + 10, 100, 20);
                var editedTag = (WayPoint.TagTypes)EditorGUI.EnumPopup(rect, wayPoint.tag);
                
                // �ύX���ꂽ�甽�f����
                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(_instance, "Edit Destination");
                    wayPoint.tag = editedTag;
                    //EditorUtility.SetDirty(_instance);
                }

                Handles.EndGUI();
            }

            */

        }




        /// ----------------------------------------------------------------------------
        // Private Method

        // [�Q�l]
        //  kan�̃�����: Unity�G�f�B�^�̍���ɓƎ��̋@�\��ǉ��o����EditorTool https://kan-kikuchi.hatenablog.com/entry/EditorTool
        //  UnityDocument: EditorTool https://docs.unity3d.com/ja/2022.1/ScriptReference/EditorTools.EditorTool.html

        [EditorTool("WayPoints/Move", typeof(WayPoints))]
        public class MoveTool : EditorTool {

            /// <summary>
            /// EditorTools���A�N�e�B�u�̂Ƃ��ɕ\�������n���h��
            /// </summary>
            public override void OnToolGUI(EditorWindow window) {
                if (!(window is SceneView sceneView)) return;

                var instance = target as WayPoints;

                var point = instance.Selected;
                if (point == null) return;

                using (new Handles.DrawingScope(instance.transform.localToWorldMatrix)) {
                    EditorGUI.BeginChangeCheck();

                    var newPosition = Handles.PositionHandle(point.position, Quaternion.identity);
                    if (EditorGUI.EndChangeCheck()) {
                        Undo.RecordObject(instance, "Change WayPoint Position");
                        point.position = newPosition;
                    }
                }
            }
        }

        private static class Styles {
            public static GUIStyle label;

            static Styles() {
                label = new GUIStyle(GUI.skin.label) {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 14,
                };
            }
        }
    }
}
#endif