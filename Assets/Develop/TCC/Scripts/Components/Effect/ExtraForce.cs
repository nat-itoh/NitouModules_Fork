using UnityEngine;
using UnityEngine.Events;
using nitou.BachProcessor;
using nitou.DesignPattern.Pooling;

namespace nitou.LevelActors.Effect {
    using nitou.LevelActors.Core;
    using nitou.LevelActors.Interfaces.Core;
    using nitou.LevelActors.Interfaces.Components;
    
    /// <summary>
    ///     A component that applies an impact to the character from outside.
    ///     It decelerates due to air resistance and friction with the ground.
    /// </summary>
    [DisallowMultipleComponent]
    public class ExtraForce : ComponentBase,
        IEffect,
        IEarlyUpdateComponent {

        /// <summary>
        ///     Friction
        /// </summary>
        [SerializeField] float _friction = 1;

        /// <summary>
        ///     air resistance
        /// </summary>
        [SerializeField] float _drag = 0.1f;

        /// <summary>
        ///     Threshold to stop acceleration.
        /// </summary>
        [SerializeField] float _threshold = 0.5f;

        /// <summary>
        ///     Reflectance strength of the character. 1 for full reflection, 0 to stop upon collision.
        /// </summary>
        [Range(0, 1)] 
        [SerializeField] float _bounce = 0f;

        /// <summary>
        ///     Callbacks when hit other collider.
        /// </summary>
        public UnityEvent<Collider> OnHitOtherCollider;

        private IGroundContact _groundCheck;
        private ActorSettings _settings;
        private ITransform _transform;
        private Vector3 _velocity;

        // 定数
        private const int HIT_CAPACITY = 15;


        /// <summary>
        ///     Reflectance strength of the character. 1 for full reflection, 0 to stop upon collision.
        /// </summary>
        public float Bounce {
            get => _bounce;
            set => _bounce = value;
        }

        /// <summary>
        /// 現在の速度
        /// </summary>
        public Vector3 Velocity => _velocity;

        /// <summary>
        /// 更新タイミング
        /// </summary>
        int IEarlyUpdateComponent.Order => Order.Effect;


        /// ----------------------------------------------------------------------------
        // MonoBehviour Method

        private void Awake() {
            GatherComponents();
        }

        private void OnValidate() {
            _friction = _friction.Positive();
            _drag = _drag.Positive();
            _threshold = Mathf.Max(0.1f, _threshold);
        }

        void IEarlyUpdateComponent.OnUpdate(float deltaTime) {
            // If there is a vector affecting the character, perform deceleration and bounce processing.
            if (_velocity.magnitude > _threshold) {
                // If there is a collider at the destination, reflect the vector.
                if (HasColliderOnDestination(deltaTime, out var closestHit)) {
                    // Event to be called when colliding with other objects.
                    // Process before correcting Velocity.
                    OnHitOtherCollider.Invoke(closestHit.collider);

                    if (_bounce > 0) {
                        // When colliding with other ExtraForce, propagate the impact.
                        if (closestHit.collider.TryGetComponent(out ExtraForce other) &&
                            closestHit.collider.TryGetComponent(out ActorSettings otherSettings)) {
                            // Force = Mass * Acceleration
                            var ownForce = _settings.Mass * _velocity;
                            var otherForce = otherSettings.Mass * other._velocity;
                            var velocity = (ownForce + otherForce) / (_settings.Mass + otherSettings.Mass);

                            // Add acceleration to self and the target of the collision.
                            other.AddForce(velocity * other._bounce);
                            _velocity = Vector3.Reflect(velocity, closestHit.normal) * _bounce;
                        } else {
                            _velocity = Vector3.Reflect(_velocity, closestHit.normal) * _bounce;
                        }
                    }
                }

                // Decelerate the character's vector. The deceleration rate switches between ground and air.
                var value = _groundCheck.IsOnGround ? _friction : _drag;
                _velocity -= _velocity * (deltaTime * value);
            } else {
                // If the acceleration falls below the threshold, disable the vector.
                _velocity = Vector3.zero;
            }
        }



        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        ///     Add impact to the character.
        /// </summary>
        /// <param name="value">Power</param>
        public void AddForce(Vector3 value) {
            _velocity += value / _settings.Mass;
        }
        
        /// <summary>
        ///     Override velocity.
        /// </summary>
        /// <param name="value">new value.</param>
        public void SetVelocity(Vector3 value) {
            _velocity = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetVelocity() {
            _velocity = Vector3.zero;
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        ///     Gather all components attached own object.
        /// </summary>
        private void GatherComponents() {
            _settings = gameObject.GetComponentInParent<ActorSettings>();
            _transform = _settings.gameObject.GetComponent<ITransform>();
            _groundCheck = _settings.gameObject.GetComponentInChildren<IGroundContact>();
        }

        /// <summary>
        ///     Method to retrieve the shape of a capsule.
        /// </summary>
        /// <param name="headPoint">Coordinates of the top of the capsule.</param>
        /// <param name="bottomPoint">Coordinates of the bottom of the capsule.</param>
        private void GetBottomHeadPosition(out Vector3 headPoint, out Vector3 bottomPoint) {
            // Get the current position of the capsule.
            var point = _transform.Position;

            // Get the height of the capsule.
            var height = _settings.Height;

            // Get the radius of the capsule.
            var radius = _settings.Radius;

            // Calculate the coordinates of the bottom of the capsule.
            bottomPoint = point + new Vector3(0, radius, 0);

            // Calculate the coordinates of the top of the capsule.
            headPoint = point + new Vector3(0, height - radius, 0);
        }

        /// <summary>
        ///     Determines if there is an object at the destination of movement.
        /// </summary>
        /// <param name="deltaTime">Delta time since the previous frame.</param>
        /// <param name="closestHit">Information about the closest collider. It will be set to default if no objects are present.</param>
        /// <returns>True if an object is present.</returns>
        private bool HasColliderOnDestination(float deltaTime, out RaycastHit closestHit) {
            // Get the positions of the character's head and bottom.
            GetBottomHeadPosition(out var bottom, out var top);

            // Calculate the distance: Character's radius + 1 frame of movement.
            var distance = _settings.Radius * 0.5f + _velocity.magnitude * deltaTime;

            // Create an array for collision detection.
            var hits = ArrayPool<RaycastHit>.New(HIT_CAPACITY);

            // Perform collision detection with the character's shape.
            var hitCount = Physics.CapsuleCastNonAlloc(top, bottom,
                _settings.Radius, _velocity.normalized, hits, distance,
                _settings.EnvironmentLayer, QueryTriggerInteraction.Ignore);

            // Find the closest collider among the colliders within the range, excluding the collider to which self belongs.
            // isCapsuleHit indicates whether the hit was successful.
            var isCapsuleHit = _settings.ClosestHit(hits, hitCount, distance, out var hit);

            // Release the used array.
            ArrayPool<RaycastHit>.Free(hits);

            if (isCapsuleHit) {
                // Get the normal of the contacting surface.
                // Without this step, there is a possibility of unexpected flipping during contact with the ground.
                var normal = _velocity.normalized;
                var ray = new Ray(hit.point - normal * 0.1f, normal);

                // Perform a raycast to find the closest hit point on the collider.
                var result = hit.collider.Raycast(ray, out closestHit, 1);

                return result;
            }

            // If there is no collision, set closestHit to the default value and return False.
            closestHit = default;
            return false;
        }


        /// ----------------------------------------------------------------------------
#if UNITY_EDITOR
        private void OnDrawGizmosSelected() {
            if (!Application.isPlaying) return;

            // Calculate the center position of the character.
            var centerPosition = _transform.Position + new Vector3(0, _settings.Height * 0.5f, 0);

            // Calculate the target position for movement.
            var maxDistance = _velocity.magnitude * 0.28f;
            var targetPosition = centerPosition + _velocity.normalized * maxDistance;

            // Represent the movement vector.
            Gizmos_.DrawSphere(targetPosition, _settings.Radius, Colors.Blue.WithAlpha(0.4f));

            // Represent the line to the target position and the wireframe of the movement vector.
            Gizmos.color = Color.white;
            Gizmos.DrawLine(targetPosition, centerPosition);
            Gizmos.DrawWireSphere(targetPosition, _settings.Radius);
        }

#endif
    }
}