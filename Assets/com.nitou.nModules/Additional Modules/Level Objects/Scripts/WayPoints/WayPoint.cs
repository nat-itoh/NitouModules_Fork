using UnityEngine;
using Sirenix.OdinInspector;

namespace nitou.LevelObjects {
    using nitou.Inspector;

    /// <summary>
    /// �V�[����̒ʉߓ_��\���N���X
    /// </summary>
    [System.Serializable]
    public class WayPoint {

        /// <summary>
        /// �^�O
        /// </summary>
        public enum TagTypes {
            None,
            Start,
            Tresure,
            Goal,
        }


        /// ----------------------------------------------------------------------------
        // Field

        [HorizontalGroup]
        [HideLabel]
        public Vector3 position = Vector3.zero;

        [HorizontalGroup()]
        [HideLabel]
        public TagTypes tag = TagTypes.None;


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// �J���[���擾���� 
        /// </summary>
        public Color GetColor() => tag switch {
            TagTypes.Start => Colors.Green,
            TagTypes.Goal => Colors.Red,
            _ => Colors.White,
        };

        public override string ToString() {
            return $"pt - {tag}, {position}";
        }
    }
}
