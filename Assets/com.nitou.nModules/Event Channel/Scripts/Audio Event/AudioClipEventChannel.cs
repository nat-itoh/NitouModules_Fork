using UnityEngine;

namespace nitou.EventChannel{
    using nitou.EventChannel.Shared;

    /// <summary>
    /// AudioClip�^�̃C�x���g�`�����l��
    /// </summary>
    [CreateAssetMenu(
        fileName = "Event_AudioClip",
        menuName = AssetMenu.Prefix.EventChannel + "AudioClip Event"
    )]
    public class AudioClipEventChannel : EventChannel<AudioClip>{}
}
