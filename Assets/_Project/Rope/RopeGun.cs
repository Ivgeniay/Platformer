using PlayerInput;
using UnityEngine;

public class RopeGun : MonoBehaviour
{
    [SerializeField] private Hook hook;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float speed;
    [SerializeField] private float ropeSpring = 100;
    [SerializeField] private float ropeDamper = 5;
    [SerializeField] private float maxFixedDistance = 5;

    private SpringJoint springJoint;



    private void OnEnable() {
        InputSys.Instance.OnJumpEvent += OnJumpHandler;
        hook.OnCollisionEvent += OnHookCollisionHandler;
    }

    private void OnDisable(){
        InputSys.Instance.OnJumpEvent -= OnJumpHandler;
        hook.OnCollisionEvent -= OnHookCollisionHandler;
    }

    private void OnJumpHandler() {
        DesctroySpring();
    }

    private void OnHookCollisionHandler(Vector3 fixedPlace)
    {
        CreateSpring(fixedPlace);
    }

    void Update()
    {
        var move = InputSys.Instance.GetMoving();

        if (Input.GetMouseButtonDown(2)) {
            Shot();
        }

        if (move == Vector2.down) {
            DesctroySpring();
        }
    }

    private void Shot() {
        hook.StopFix();
        //if (springJoint)
        //    Destroy(springJoint);

        DesctroySpring();

        hook.transform.position = spawnPoint.position;
        hook.transform.rotation = spawnPoint.rotation;

        hook.SetSpeed(spawnPoint.forward * speed);
    }

    private void CreateSpring(Vector3 fixedPlace)
    {
        if (springJoint == null) {
            springJoint = gameObject.AddComponent<SpringJoint>();
            springJoint.connectedBody = hook.Rigidbudy;
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.spring = ropeSpring;
            springJoint.damper = ropeDamper;

            var distance = Vector3.Distance(fixedPlace, transform.position);
            springJoint.maxDistance = distance > maxFixedDistance ? maxFixedDistance :  distance;
        }
    }

     private void DesctroySpring() {
        if (springJoint)
            Destroy(springJoint);
    }
}
