using UnityEngine;

namespace nitou.EventChannel {
    using nitou.EventChannel.Shared;

    /// <summary>
    /// AudioClip�^�̃C�x���g���X�i�[ 
    /// </summary>
    [AddComponentMenu(
        ComponentMenu.Prefix.EventChannel + "AudioClip Event Listener"
    )]
    public class AudioClipEventListener : EventListener<AudioClip, AudioClipEventChannel> { }
}
