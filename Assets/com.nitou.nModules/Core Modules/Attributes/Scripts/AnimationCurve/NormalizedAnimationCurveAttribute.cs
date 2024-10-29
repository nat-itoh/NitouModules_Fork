using System;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// [�Q�l]
//  LIGHT11: AnimationCurve�𐳋K������A�g���r���[�g����� https://light11.hatenadiary.com/entry/2021/08/11/194500

namespace nitou.Inspector {

    /// <summary>
    /// <see cref="AnimationCurve"/>�͈̔͂�0 ~ 1�ɐ������鑮��
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class NormalizedAnimationCurveAttribute : PropertyAttribute {

        public bool NormalizeValue { get; }
        public bool NormalizeTime { get; }

        public NormalizedAnimationCurveAttribute(bool normalizedValue = true, bool normalizedTime = true) {
            NormalizeValue = normalizedValue;
            NormalizeTime = normalizedValue;
        }

    }
}


#if UNITY_EDITOR
namespace nitou.Inspector.EditorScripts {

    [CustomPropertyDrawer(typeof(NormalizedAnimationCurveAttribute))]
    public class NormalizedAnimationCurveAttributeDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            var attr = (NormalizedAnimationCurveAttribute)attribute;

            if (property.propertyType != SerializedPropertyType.AnimationCurve) {
                // AnimationCurve�ȊO�̃t�B�[���h�ɃA�g���r���[�g���t�����Ă����ꍇ�̃G���[�\��
                position = EditorGUI.PrefixLabel(position, label);
                var preIndent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                EditorGUI.LabelField(position, "Use NormalizedAnimationCurveAttribute with AnimationCurve.");
                EditorGUI.indentLevel = preIndent;
                return;
            }

            using (var scope = new EditorGUI.ChangeCheckScope()) {
                EditorGUI.PropertyField(position, property, label, true);

                var curve = property.animationCurveValue;
                if (scope.changed) {
                    if (attr.NormalizeValue) {
                        property.animationCurveValue = property.animationCurveValue.NormalizeValue();
                    }

                    if (attr.NormalizeTime) {
                        property.animationCurveValue = property.animationCurveValue.NormalizeTime();
                    }
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUI.GetPropertyHeight(property);
        }

    }
}
#endif
