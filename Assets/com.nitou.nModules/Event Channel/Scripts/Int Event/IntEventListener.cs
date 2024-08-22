using UnityEngine;

namespace nitou.EventChannel {
    using nitou.EventChannel.Shared;

    /// <summary>
    /// <see cref="int"/>�^�̃C�x���g���X�i�[
    /// </summary>
    [AddComponentMenu(
        ComponentMenu.Prefix.EventChannel + "Int Event Listener"
    )]
    public class IntEventListener : EventListener<int, IntEventChannel> {}

}