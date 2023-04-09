using System;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public event Action<Vector3> OnCollisionEvent;
    public Rigidbody Rigidbudy { get; private set; }
    [SerializeField] private FixedJoint fixedJoint;
    private bool canBeConnected;

    private void Awake() {
        Rigidbudy = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (fixedJoint == null && canBeConnected) {
            fixedJoint = gameObject.AddComponent<FixedJoint>();

            var collisionRb = collision.rigidbody;
            if (collisionRb is null ) {
                collisionRb = collision.gameObject.AddComponent<Rigidbody>();
                collisionRb.isKinematic = true;
            }

            fixedJoint.connectedBody = collisionRb;
            canBeConnected = false;
            OnCollisionEvent?.Invoke(transform.position);
        }
    }

    public void SetSpeed(Vector3 velocity) {
        Rigidbudy.velocity = velocity;
    }



    public void StopFix()
    {
        canBeConnected = true;
        if (fixedJoint) {
            Destroy(fixedJoint);
        }
    }
}
