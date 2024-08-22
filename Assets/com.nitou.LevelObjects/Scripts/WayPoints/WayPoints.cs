using System.Linq;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace nitou.LevelObjects{

    /// <summary>
    /// WayPoint���Ǘ�����R���|�[�l���g
    /// </summary>
    public class WayPoints : MonoBehaviour ,ITargetContainer<WayPoint>, ITargetSelector<WayPoint>{

        public enum PointType {
            Grobal,
            Local,
        }

        [SerializeField] List<WayPoint> _points = new();
 

        /// ----------------------------------------------------------------------------
        #region IContainer interface

        public int Count => _points.Count;
        public IEnumerable<WayPoint> Targets => _points;
        public WayPoint First => _points.FirstOrDefault();

        public bool Contains(WayPoint point) => _points.Contains(point);
        public void Add(WayPoint point) => _points.Add(point);
        public bool Remove(WayPoint point) => _points.Remove(point);
        public void Clear() => _points.Clear();
        #endregion

        /// ----------------------------------------------------------------------------
        #region ISelector interface

        /// <summary>
        /// �I������Ă���<see cref="WayPoint"/>
        /// </summary>
        public WayPoint Selected { get; private set; }


        /// <summary>
        /// �w�肵��<see cref="WayPoint"/>��I������
        /// </summary>
        public bool Select(WayPoint box) {
            if (!Contains(box)) return false;

            Selected = box;
            return true;
        }
        #endregion


        /// ----------------------------------------------------------------------------
#if UNITY_EDITOR
        private void OnDrawGizmosSelected() {
            //Gizmos_.DrawLines(_points.Select(x => x.position).ToList(), Colors.GreenYellow);
        }
#endif
    }
}