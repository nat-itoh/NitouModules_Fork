using Sirenix.OdinInspector;

namespace nitou.LevelActors{

    /// <summary>
    /// �A�N�^�[�ړ������̊�Ƃ�����W�n
    /// </summary>
    public enum MovementReferenceMode {

        /// <summary>
        /// �O���[�o�����W
        /// </summary>
        [LabelText(SdfIconType.PinMapFill)]
        World,

        /// <summary>
        /// �O�����W�n
        /// </summary>
        [LabelText(SdfIconType.PinMap)]
        External,

        /// <summary>
        /// �A�N�^�[���W�n
        /// </summary>
        [LabelText(SdfIconType.PersonFill)]
        Actor,
    }
}
