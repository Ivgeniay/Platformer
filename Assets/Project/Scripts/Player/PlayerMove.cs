using PlayerInput;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float moveForce;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float friction;
        [SerializeField] private float maxAngle;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private CapsuleCollider collider_;
        [SerializeField] private Transform modelTransform;
        [SerializeField][Range(0, 1)] float scaleTime = 1;


        private bool isSquat = false;
        private Rigidbody rigidBody;
        private Vector2 inputMoving;

        private bool isGrounded {
            get => Physics.CheckCapsule(
                    collider_.bounds.center,
                    new Vector3(collider_.bounds.center.x, collider_.bounds.min.y, collider_.bounds.center.z),
                    collider_.bounds.size.x / 2 * 0.9f,
                    groundLayerMask);
        }

        private bool isFitAngel;

        #region Mono
        private void Awake() {
            rigidBody = GetComponent<Rigidbody>();
        }

        private void OnEnable() {
            Subscribe();
        }
        private void OnDisable() {
            Unsubscribe();
        }

        void Update() {
            Time.timeScale= scaleTime;
            inputMoving = InputSys.Instance.GetMoving();
            if (inputMoving.y < 0 || !isGrounded) isSquat = true;

            if (isSquat) modelTransform.localScale = Vector3.Lerp(
                                                            modelTransform.localScale,
                                                            new Vector3(1, 0.5f, 1),
                                                            Time.deltaTime * 15);
            else modelTransform.localScale = Vector3.Lerp(
                                                            modelTransform.localScale,
                                                            Vector3.one,
                                                            Time.deltaTime * 20);
            if (isSquat == true) isSquat = CanStandUp();
        }

        void FixedUpdate() {
            float speedMultiplier = 1f;
            if (!isGrounded) speedMultiplier /= 1.33f;
            if (rigidBody.velocity.x > maxSpeed && inputMoving.x > 0) speedMultiplier = 0; 
            if (rigidBody.velocity.x < -maxSpeed && inputMoving.x < 0) speedMultiplier = 0; 
            
            SetPlayerForce(rigidBody, speedMultiplier);
            if (isGrounded) SetDrag(rigidBody, friction);
        }
        private void OnCollisionStay(Collision collision) {
            foreach (var contact in collision.contacts) {
                float angle = Vector3.Angle(contact.normal, Vector3.up);
                isFitAngel = angle < maxAngle ? true : false;
            }
        }
        #endregion
        private void Subscribe() {
            InputSys.Instance.OnJumpEvent += OnJumpEventHandler;
        }
        private void Unsubscribe() {
            InputSys.Instance.OnJumpEvent -= OnJumpEventHandler;
        }
        private bool CanStandUp() {
            var left = new Vector3(collider_.bounds.max.x, collider_.bounds.max.y, collider_.bounds.center.z);
            var right = new Vector3(collider_.bounds.min.x, collider_.bounds.max.y, collider_.bounds.center.z);
            var distance = 1.68f;

            var rigthSide = Physics.Raycast(right, Vector3.up, distance, groundLayerMask);
            var leftSide = Physics.Raycast(left, Vector3.up, distance, groundLayerMask);

            if (rigthSide || leftSide) return true;
            return false;
        }
        private void OnJumpEventHandler() {
            if (isGrounded)
                Jump(rigidBody, jumpForce);
        }
        private void Jump(Rigidbody rigidb, float jumpForce) =>
            rigidb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
        private void SetPlayerForce(Rigidbody rigidbody, float speedMultiplier) =>
            rigidbody.AddForce(
                inputMoving.x * moveForce * speedMultiplier, 0, 0,
                ForceMode.Acceleration);
        private void SetDrag(Rigidbody rigidb, float friction) =>
            rigidb.AddForce(-rigidb.velocity.x * friction, 0, 0, ForceMode.Acceleration);

    }

}
