using UnityEngine;

namespace nitou.Detecor {

    /// <summary>
    /// �L���b�V������^�[�Q�b�g�̎�ށD
    /// ���d���蓙������邽�߂̃L���b�V���ɗp����D
    /// </summary>
    public enum CachingTarget {
        RootObject = 0,                     // Cache the root object
        Collider = 1,                       // Cache colliders
        Rigidbody = 2,                      // Cache objects with Rigidbody
        CharacterController = 3,            // cache object with character controller.
        RigidbodyOrCharacterController = 4  // Rigidbody or Character controller.
    }


    /// <summary>
    /// 
    /// </summary>
    public static class DetectionUtil {

        /// <summary>
        /// ���o���ꂽ�R���C�_�[����C���̃R���C�_�[��ۗL����<see cref="GameObject"/>���擾����
        /// </summary>
        public static GameObject GetHitObject(this Collider hitCollider, CachingTarget targetType) {

            switch (targetType) {

                case CachingTarget.RootObject: {
                        return hitCollider.transform.root.gameObject;
                    }

                case CachingTarget.Rigidbody: {
                        // ��Rigidbody��null�̏ꍇ�̓R���C�_�[��Ԃ�
                        var rigidbody = hitCollider.attachedRigidbody;
                        return (rigidbody != null) ? rigidbody.gameObject : hitCollider.gameObject;
                    }

                case CachingTarget.CharacterController: {
                        // ��Controller��null�̏ꍇ�̓R���C�_�[��Ԃ�
                        var controller = hitCollider.GetComponentInParent<CharacterController>();
                        return (controller != null) ? controller.gameObject : hitCollider.gameObject;
                    }

                case CachingTarget.RigidbodyOrCharacterController: {
                        // Rigidbody
                        var rigidbody = hitCollider.attachedRigidbody;
                        if (rigidbody != null) return rigidbody.gameObject;

                        // Charactor Controller
                        var controller = hitCollider.GetComponentInParent<CharacterController>();
                        if (controller != null) return controller.gameObject;

                        // ���ǂ����null�Ȃ�R���C�_�[��Ԃ�
                        return hitCollider.gameObject;
                    }

                default:
                    return hitCollider.gameObject;
            }

        }



    }
}
