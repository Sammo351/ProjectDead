﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {
    public float MaxAngleBeforeTurning = 45f;
    /*   public Transform WeaponHolder; */
    private int _playerIndex = 0;
    private Color _playerColor = Color.white;

    private Animator _animator;
    private Rigidbody _rigidbody;

    public float moveSpeed = 10f;
    public float runSpeed = 15f;

    public bool UseAimAssist = true;
    public float AimAssistDistance = 10f;
    public float AimAssistAngle = 15f;

    internal Vector3 aimVector;

    public bool isSprinting = false;
    public float maxStamina = 10;
    public float currentStamina = 10;

    private Vector2 _moveVectorInput;
    private Vector2 _aimVectorInput;
    PlayerInput _playerInput;

    public PlayerControls playerControls;

    void Awake () {
        playerControls = new PlayerControls ();
    }

    public void Start () {
        _playerInput = GetComponent<PlayerInput> ();
        currentStamina = maxStamina;

        _animator = GetComponentInChildren<Animator> ();
        _rigidbody = GetComponent<Rigidbody> ();

        //Sets players index and increases the index for next player
        PlayerIndex = GLOBAL.AddPlayerController (this);
        //gets the relevent color from players index
        PlayerColor = GLOBAL._PlayerColors[PlayerIndex];
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.F)) {
            AttemptInteraction ();
        }
    }
    void OnEnable () {
        playerControls.Enable ();
    }

    void OnDisable () {
        playerControls.Disable ();
    }

    private void OnShoot (InputAction.CallbackContext obj) {
        throw new NotImplementedException ();
    }

    public void FixedUpdate () {
        HandleAiming ();
        HandleMovement ();
    }

    public void OnMovement (InputValue value) {
        this._moveVectorInput = value.Get<Vector2> ();

    }

    public void OnAimVector (InputValue value) {
        this._aimVectorInput = value.Get<Vector2> ();
    }

    void OnControlsChanged (PlayerInput input) {
        this._playerInput = input;
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
        Vector2 inputVec = _moveVectorInput;

        float moveX = inputVec.x;
        float moveY = inputVec.y;
        //Might need to use inputVec.y.DeadZone(0.1) to eliminate controller wobbliness

        Vector3 moveVec = new Vector3 (moveX, 0f, moveY);
        moveVec = Camera.main.transform.TransformDirection (inputVec);
        moveVec.y = 0;
        moveVec.Normalize ();

        Vector3 localizedInput = transform.InverseTransformDirection (moveVec);

        if (moveVec.sqrMagnitude > 0) {
            //isSprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0;
            if (isSprinting)
                currentStamina -= Time.fixedDeltaTime;
        } else {
            isSprinting = false;
            if (currentStamina < maxStamina) {
                currentStamina += Time.fixedDeltaTime;
                currentStamina = Mathf.Clamp (currentStamina, 0, maxStamina);
            }
        }

        _rigidbody.MovePosition (transform.position + (moveVec * GetMoveSpeed () * Time.fixedDeltaTime));
    }

    public float GetMoveSpeed () {
        return isSprinting ? runSpeed : moveSpeed;
    }
    public void HandleAiming () {
        if (_playerInput == null) {
            return;
        }
        if (_playerInput.currentControlScheme == "Keyboard") {
            Ray ray = Camera.main.ScreenPointToRay (_aimVectorInput);
            Plane plane = new Plane (Vector3.up, Vector3.zero);

            float distance;
            if (plane.Raycast (ray, out distance)) {
                Vector3 target = ray.GetPoint (distance);
                Vector3 direction = target - transform.position;
                transform.forward = direction;
                aimVector = direction;
            }
        } else {
            Vector3 aim = new Vector3 (_aimVectorInput.x, 0, _aimVectorInput.y);
            if (aim.sqrMagnitude > 0) {
                Vector3 direction = Camera.main.transform.TransformDirection (_aimVectorInput);
                direction.y = 0;
                transform.forward = direction;
                aimVector = direction;
            }

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

    void AttemptInteraction () {
        Debug.Log ("interacting");
        foreach (Interactable ic in GameObject.FindObjectsOfType<Interactable> ()) {

            float distance = Vector3.Distance (transform.position, ic.transform.position);
            if (distance <= ic.InteractRange ()) {
                Debug.Log ("With " + ic);
                ic.OnInteraction ();
            }
        }
    }
    void OnDrawGizmos () {
        //Gizmos.DrawLine(WeaponHolder.position, WeaponHolder.position + transform.forward * 5);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine (transform.position, transform.position + transform.forward * 5);
    }

    //DEBUG STUFF
    void OnGUI () {
        //GUILayout.Label ("Player Input: " + _moveVectorInput);
        //GUILayout.Label ("Player Stamina: " + currentStamina);
        //GUILayout.Label ("Input: " + _playerInput.currentControlScheme);
    }

}