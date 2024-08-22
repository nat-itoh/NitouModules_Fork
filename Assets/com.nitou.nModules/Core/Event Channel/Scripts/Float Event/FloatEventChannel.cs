using UnityEngine;

namespace nitou.EventChannel {
    using nitou.EventChannel.Shared;

    /// <summary>
    /// <see cref="float"/>�^�̃C�x���g�`�����l��
    /// </summary>
    [CreateAssetMenu(
        fileName = "Event_Float",
        menuName = AssetMenu.Prefix.EventChannel + "Float Event"
    )]
    public class FloatEventChannel : EventChannel<float> { }

}