using UnityEngine;

namespace nitou.EventChannel {
    using nitou.EventChannel.Shared;

    /// <summary>
    /// <see cref="void"/>�^�̃C�x���g�`�����l��
    /// </summary>
    [CreateAssetMenu(
        fileName = "Event_Void",
        menuName = AssetMenu.Prefix.EventChannel + "Void Event"
    )]
    public class VoidEventChannel : EventChannel { }

}