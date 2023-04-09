using Player;
using PlayerInput;
using Rope;
using Unity.VisualScripting;
using UnityEngine;

public class RopeGun : MonoBehaviour
{
    [SerializeField] private Hook hook;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform ropeStartPoint;
    [SerializeField] private float speed;
    [SerializeField] private float ropeSpring = 100;
    [SerializeField] private float ropeDamper = 5;
    [SerializeField] private float maxFixedDistance = 5;
    [SerializeField] private float maxEnableHookDistance = 15;
    [SerializeField] private RopeRenderer ropeRenderer;
    [SerializeField] private PlayerMove playerMove;

    private SpringJoint springJoint;
    private float distance;
    private RopeState currentRopeState;


    private void OnEnable() {
        InputSys.Instance.OnJumpEvent += OnJumpHandler;
        hook.OnCollisionEvent += OnHookCollisionHandler;
    }

    private void OnDisable(){
        InputSys.Instance.OnJumpEvent -= OnJumpHandler;
        hook.OnCollisionEvent -= OnHookCollisionHandler;
    }

    private void OnJumpHandler() {
        if (currentRopeState == RopeState.Active) 
            playerMove.RopeJump();
        
        DesctroySpring();
    }

    private void OnHookCollisionHandler(Vector3 fixedPlace) {
        CreateSpring(fixedPlace);
    }

    void Update()
    {
        var move = InputSys.Instance.GetMoving();

        if(currentRopeState == RopeState.Fly)
        {
            distance = distance = Vector3.Distance(hook.transform.position, ropeStartPoint.position);
            if (distance > maxEnableHookDistance) {
                currentRopeState = RopeState.Disable;
                hook.gameObject.SetActive(false);
                ropeRenderer.Hide();
            }
        }

        if (Input.GetMouseButtonDown(2)) {
            Shot();
        }

        if (move == Vector2.down) {
            DesctroySpring();
        }

        if (currentRopeState == RopeState.Fly || currentRopeState == RopeState.Active) {
            ropeRenderer.Draw(ropeStartPoint.position, hook.transform.position, distance);
        }
    }

    private void Shot() {
        distance = 0;

        hook.gameObject.SetActive(true);
        hook.StopFix();

        DesctroySpring();

        hook.transform.position = spawnPoint.position;
        hook.transform.rotation = spawnPoint.rotation;

        hook.SetSpeed(spawnPoint.forward * speed);

        currentRopeState = RopeState.Fly;
    }

    private void CreateSpring(Vector3 fixedPlace)
    {
        if (springJoint == null) {
            springJoint = gameObject.AddComponent<SpringJoint>();
            springJoint.connectedBody = hook.Rigidbudy;
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.spring = ropeSpring;
            springJoint.damper = ropeDamper;
            springJoint.anchor = ropeStartPoint.localPosition;

            distance = Vector3.Distance(fixedPlace, ropeStartPoint.position);
            springJoint.maxDistance = distance > maxFixedDistance ? maxFixedDistance :  distance;
            currentRopeState = RopeState.Active;
        }
    }

     private void DesctroySpring() {
        if (springJoint)
        {
            Destroy(springJoint);
            ropeRenderer.Hide();
            hook.gameObject.SetActive(false);
            currentRopeState = RopeState.Disable;
        }
    }
}

public enum RopeState
{
    Disable,
    Fly,
    Active
}
