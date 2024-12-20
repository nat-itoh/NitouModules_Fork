using UnityEngine;

namespace nitou.LevelActors.Sensor{

    /// <summary>
    /// 手動で
    /// </summary>
    public interface IManualSensor : ISensor {
               

        /// <summary>
        /// スキャンを実行する
        /// </summary>
        public void Scan();

        /// <summary>
        /// 
        /// </summary>
        public bool IsInSight(Collider collider);
    }
}
