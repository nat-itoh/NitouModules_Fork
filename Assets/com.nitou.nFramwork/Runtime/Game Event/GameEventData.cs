using UnityEngine;

namespace nitou.GameSystem {

    /// <summary>
    /// �Q�[���C�x���g�p�̃f�[�^�N���X
    /// </summary>
    public abstract class GameEventData {

        public static EmptyEventData Empty() {
            return new EmptyEventData();
        }
    }


    public class EmptyEventData : GameEventData { }
}
