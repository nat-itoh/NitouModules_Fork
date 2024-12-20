using UnityEngine;

namespace nitou.AnimationModule {

    /// <summary>
    /// A component which takes the root motion from an <see cref="Animator"/> and applies it to a
    /// <see cref="CharacterController"/>.
    /// </summary>
    /// https://kybernetik.com.au/animancer/api/Animancer/RedirectRootMotionToCharacterController
    /// 
    [AddComponentMenu("Animancer/Redirect Root Motion To Character Controller")]
    public class RedirectRootMotionToCharacterController : RedirectRootMotion<CharacterController> {

        protected override void OnAnimatorMove() {
            if (!ApplyRootMotion) return;

            Target.Move(Animator.deltaPosition);
            Target.transform.rotation *= Animator.deltaRotation;
        }

    }
}
