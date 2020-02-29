using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {
    public float MaxAngleBeforeTurning = 45f;
    public Transform WeaponHolder;
    private int _playerIndex = 0;
    private Color _playerColor = Color.white;

    private Animator _animator;
    private Rigidbody _rigidbody;

    public float moveSpeed = 10f;

    public bool UseAimAssist = true;
    public float AimAssistDistance = 10f;
    public float AimAssistAngle = 15f;

    internal Vector3 aimVector;

    public void Start () {
        _animator = GetComponentInChildren<Animator> ();
        _rigidbody = GetComponent<Rigidbody> ();

        //Sets players index and increases the index for next player
        PlayerIndex = GLOBAL.AddPlayerController (this);
        //gets the relevent color from players index
        PlayerColor = GLOBAL._PlayerColors[PlayerIndex];
    }

    public void Update () {
        HandleAiming ();
        //HandleMovement();
    }
    public int PlayerIndex {
        get { return _playerIndex; }
        set { _playerIndex = value; }
    }
    public Color PlayerColor {
        get { return _playerColor; }
        set { _playerColor = value; }
    }
    public void HandleMovement () {
        Vector2 inputVec = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
        float moveX = inputVec.x;
        float moveY = inputVec.y; //.DeadZone(0.1f);

        Vector3 moveVec = new Vector3 (moveX, 0f, moveY);

        //Should change moveX/Y to delta position, so if walking into a wall, it doesn't animate walking into it. 

        //moveVec = Vector3.ClampMagnitude(moveVec, 1f);
        moveVec = Camera.main.transform.TransformDirection (inputVec);
        moveVec.y = 0;
        moveVec.Normalize ();

        Vector3 localizedInput = transform.InverseTransformDirection (moveVec);

        _animator.SetFloat ("Horizontal", localizedInput.x, 0.05f, Time.deltaTime);
        _animator.SetFloat ("Vertical", localizedInput.z, 0.05f, Time.deltaTime);

        _rigidbody.MovePosition (transform.position + (moveVec * moveSpeed * Time.deltaTime));

    }

    public void HandleAiming () {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        Plane plane = new Plane (Vector3.up, Vector3.zero);

        float distance;
        if (plane.Raycast (ray, out distance)) {
            Vector3 target = ray.GetPoint (distance);
            Vector3 direction = target - transform.position;

            transform.forward = direction;

            aimVector = direction;
        }

        //if (UseAimAssist)
        //{
        //    EntityTargetable[] targets = FindObjectsOfType<EntityTargetable>();
        //    float lastCost = Mathf.Infinity;
        //    foreach (EntityTargetable entity in targets)
        //    {
        //        Vector3 dir = entity.transform.position - transform.position;
        //        dir.y = 0f;
        //        float angle = Vector3.Angle(aimVec, dir);
        //        if (angle < AimAssistAngle)
        //        {
        //            if (dir.magnitude < AimAssistDistance)
        //            {
        //                float cost = dir.magnitude * angle;
        //                if(cost < lastCost)
        //                {
        //                    lastCost = cost;
        //                    aimVec = Vector3.Lerp(aimVec, dir.normalized, 1f);
        //                }
        //            }
        //        }
        //    }
        //}

    }

    //public void SetPlayerInput(PlayerInput playerInput)
    //{
    //    input = playerInput;
    //}

    void OnDrawGizmos () {
        Gizmos.DrawLine (transform.position, transform.position + transform.forward * 5);
    }

}