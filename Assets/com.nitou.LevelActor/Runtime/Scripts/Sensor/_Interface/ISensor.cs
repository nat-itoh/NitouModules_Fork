using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace nitou.LevelActors.Sensor{

    /// <summary>
    /// �Z���T�̃C���^�[�t�F�[�X
    /// </summary>
    public interface ISensor {

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<GameObject> Objects { get; }

    }


    public interface ISensor<T> {

        /// <summary>
        /// �ΏۃI�u�W�F�N�g�̃��X�g�D
        /// </summary>
        public IReadOnlyReactiveCollection<T> Targets { get; }
    }


    
    /// <summary>
    /// Sensor�֘A�̊g�����\�b�h�W�D
    /// </summary>
    public static class SensorExtensions {

    }
       
}
