#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace nitou {

    [CustomPropertyDrawer(typeof(OdinIgnoreAttribute))]
    public class OdinIgnoreDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent content) {

            // �����̂���Color�^�̂ݗp�ӂ��Ă���
            if(property.propertyType == SerializedPropertyType.Color) {
                property.colorValue = EditorGUI.ColorField(position, property.displayName, property.colorValue);
            }

        }
    }
}
#endif