#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;

// [�Q�l]
//  _: Unity2019.1���瓱�����ꂽEditorTool�̏Љ� https://blog.yucchiy.com/2020/09/editor-tools/

namespace nitou.Detecor.EditorScripts{

    /// <summary>
    /// Editor extension for SequentialCollisionDetector.
    /// To avoid problems during EditMultipleObjects, the editor extension without content avoids simultaneous editing of components.
    /// /</summary>
    [CustomEditor(typeof(SequentialCollisionDetector))]
    internal class SequentialCollisionDetectorEditor : OdinEditor{

        // ����Ώ�
        private static SequentialCollisionDetector _instance = null;


        /// ----------------------------------------------------------------------------
        // LifeCycle Events

        protected override void OnEnable() {
            base.OnEnable();
            _instance = target as SequentialCollisionDetector;
            Tools.hidden = true;
        }

        protected override void OnDisable() {
            base.OnDisable();
            Tools.hidden = false;
        }

        private void OnSceneGUI() {

            // Current time rate
            Handles.Label(Vector3.zero, $"[{_instance.Rate} / 1]");

            foreach (var data in _instance.DataList) {
                var box = data.volume;

                using (new Handles.DrawingScope(_instance.transform.localToWorldMatrix)) {

                    int controlId = GUIUtility.GetControlID(FocusType.Passive);
                    var capSize = HandleUtility.GetHandleSize(box.GetWorldPosition(_instance.transform)) * 0.2f;

                    // �I��p�n���h���L���b�v�̕`��
                    if (Event.current.type.IsRepaintOrLayout()) {
                        var color = (data == _instance.SelectedBox) ? Colors.Orange : Colors.White;
                        using (new Handles.DrawingScope(color)) {
                            Handles.SphereHandleCap(controlId, box.position, box.rotation, capSize, Event.current.type);
                        }
                    }

                    // �I���{�b�N�X�̍X�V
                    else if (Event.current.type == EventType.MouseDown) {
                        // �N���b�N���������ɂ�����̂�ControlId����v������I�𒆂Ƃ���
                        // if (controlId == EditorGUIUtility.hotControl) {
                        if (controlId == HandleUtility.nearestControl) {
                            _instance.Select(data);
                        }
                    }


                    // �I���{�b�N�X�̑���
                    if (data != _instance.SelectedBox) continue;

                    switch (Tools.current) {
                        case Tool.Move: BoxPositionHandle(box); break;
                        case Tool.Rotate: BoxRotationHandle(box); break;
                        case Tool.Scale: BoxScaleHandle(box); break;
                        default: break;
                    }

                    using (new Handles.DrawingScope(Colors.GreenYellow)) {
                        Handles.DrawDottedLine(Vector3.zero, box.position, 4);
                    }

                }
            }
        }


        /// ----------------------------------------------------------------------------
        // Private Method (Handle)

        /// <summary>
        /// �ړ�����p�̃n���h��
        /// </summary>
        private void BoxPositionHandle(Shapes.Box box) {
            EditorGUI.BeginChangeCheck();
            var newPosition = Handles.PositionHandle(box.position, box.rotation);
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(_instance, "Change Box Position");
                box.position = newPosition;
            }
        }

        /// <summary>
        /// ��]����p�̃n���h��
        /// </summary>
        private void BoxRotationHandle(Shapes.Box box) {
            EditorGUI.BeginChangeCheck();
            var newRotation = Handles.RotationHandle(box.rotation, box.position);
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(_instance, "Change Box Rotation");
                box.eulerAngle = newRotation.eulerAngles;
            }
        }

        /// <summary>
        /// �X�P�[������p�̃n���h��
        /// </summary>
        private void BoxScaleHandle(Shapes.Box box) {

            EditorGUI.BeginChangeCheck();
            var newScale = Handles.ScaleHandle(box.size, box.position, box.rotation);
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(_instance, "Change Box Scale");
                box.size = newScale;
            }
        }


        /// ----------------------------------------------------------------------------
        // Private Method (Gizmo)

        [DrawGizmo(GizmoType.Active)]
        private static void DrawGizmoOnSelect(SequentialCollisionDetector target, GizmoType type) {
            if (target == null) return;

            target.DataList.ForEach(data => {
                var box = data.volume;
                var center = box.GetWorldPosition(target.transform);
                var rotation = box.GetWorldRotaion(target.transform);

                //Gizmos_.DrawLine(target.transform.position, center, Color.gray);

                // �I���{�b�N�X�̏ꍇ
                if (data == target.SelectedBox) {
                    //Gizmos_.DrawCube(center, rotation, box.size, Colors.Yellow);
                    Gizmos_.DrawWireCube(center, rotation, box.size, Colors.Green);
                }
                //
                else {
                    Gizmos_.DrawWireCube(center, rotation, box.size, Colors.GreenYellow);
                }

                // Display translucent yellow if in range and enabled.
                if (data.IsInTimeRange(target.Rate)) {
                    var activeColor = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.2f);
                    Gizmos_.DrawCube(center, rotation, box.size, activeColor);
                }
            });
        }

        /*
        [DrawGizmo(GizmoType.NonSelected)]
        private static void DrawGizmo(SequentialCollisionDetector target, GizmoType type) {
            if (target == null) return;

            target.DataList.ForEach(data => {
                var box = data.box;
                var center = box.GetWorldPosition(target.transform);
                var rotation = box.GetWorldRotaion(target.transform);
                Gizmos_.DrawWireCube(center, rotation, box.size, Colors.GreenYellow);
            });
        }
        */

    }
}
#endif