using UnityEngine;

namespace nitou.GameSystem {
    using nitou.EventChannel.Shared;

    /// <summary>
    /// InGame�p�̃C�x���g�`�����l��
    /// </summary>
    [CreateAssetMenu(
        fileName = "Event_Game",
        menuName = AssetMenu.Prefix.EventChannel + "Game Event",
        order = AssetMenu.Order.Early
    )]
    public class GameEventChannel : EventChannel<GameEventData> { }
}
