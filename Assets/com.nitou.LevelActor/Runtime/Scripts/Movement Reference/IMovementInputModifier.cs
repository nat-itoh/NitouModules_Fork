using UnityEngine;

namespace nitou.LevelActors
{
    public interface IMovementInputModifier{

        /// <summary>
        /// 修正後の入力ベクトル
        /// </summary>
        public Vector3 ModifieredInputVector { get; }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateInputData(Vector2 movementInput);

        /// <summary>
        /// 
        /// </summary>
        public void ResetInputData();
    }
}
