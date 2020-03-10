﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent (typeof (NavMeshAgent))]
public class AI : MonoBehaviour, IDetectSound, IDetectSight {
    // these are 2 different targets, if using transform, updates will be frequent to track movement
    // if vector3 no update will be triggered.
    NavMeshAgent agent;
    Transform targetTransform = null;
    Vector3? target = null;
    AIState state = AIState.Idle;
    public float fullVisionRadius = 6f; // Can see you (even from behind) if you enter this range
    public float searchTime = 7f; //After reaching destination, zombie will look about for this amount of time
    public float angleOfSightCone = 170f, sightRange = 18f, soundRange = 30f;
    float targetPriority = -1;
    float time = 0, targetSearchCooldown = 0.2f, targetArrivedRadius = 1.8f;
    void Start () {
        agent = GetComponent<NavMeshAgent> ();
    }

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        if (GetTargetLocation () != null) {
            if (IsTargetTransform ()) {
                agent.SetDestination (targetTransform.position);
            } else { agent.SetDestination (target.Value); }
        }
        if (time >= targetSearchCooldown) {
            time -= targetSearchCooldown;
            LookForTargets ();
        }
        if (ReachedTarget ()) {
            if (GetTargetLocation () != null) //valid position
            {
                if (IsTargetTransform ()) //if reached an ENTITY 
                {

                } else //if reached target POSITION
                {
                    SetTargetEmpty ();
                    //OnTargetLost ();

                }

            }
        }
    }
    public float AngleOfSightCone (Vector3 target) {

        float distance = Vector3.Distance (target, transform.position);
        distance = Mathf.Clamp (distance, 0, fullVisionRadius);
        float trueVision = Mathf.Clamp (distance.Map (2.5f, fullVisionRadius, 360, angleOfSightCone), 0, 360);
        return trueVision;
    }
    public void OnDetectSound (Vector3 position) {

        if (IsTargetInSoundRange (position)) {
            SetTarget (position, 0);
        }
    }
    public void OnDetectSight (Transform t, int priority) {

        if (IsTargetInSightRange (t.position)) {
            SetTarget (t, priority);

        }
    }
    Vector3? GetTargetLocation () {
        return targetTransform != null ? targetTransform.position : target;
    }
    public void SetTarget (Transform t, int priority) {
        if (priority >= targetPriority) {
            targetPriority = priority;
            target = null;
            targetTransform = t;
            GetComponent<Zombie> ().Target = t;
            state = AIState.Engaged;

        }
    }
    public void SetTarget (Vector3 t, int priority) {
        if (priority >= targetPriority) {
            target = t;
            targetTransform = null;
            GetComponent<Zombie> ().Target = null;
            state = AIState.Engaged;
        }
    }
    void SetTargetEmpty () {
        targetPriority = -1;
        target = null;
        targetTransform = null;
        GetComponent<Zombie> ().Target = null;
        state = AIState.Idle;

    }
    public bool IsTargetTransform () { return targetTransform != null; }

    void LookAtTarget (Vector3 position) {
        transform.forward = (position - transform.position).normalized;
    }
    bool IsTargetInSightRange (Vector3 _target) {
        Vector3 targetDir = _target - transform.position;
        float angle = Vector3.Angle (targetDir, transform.forward);
        float distance = Vector3.Distance (_target, transform.position);

        return angle <= angleOfSightCone * 0.5f && distance <= sightRange;
    }
    bool IsTargetInSoundRange (Vector3 _target) {
        float distance = Vector3.Distance (_target, transform.position);
        return distance < soundRange;
    }
    bool IsTargetInAwarenessRange (Vector3 _target) {
        float awarenessAngle = AngleOfSightCone (_target);
        Vector3 targetDir = _target - transform.position;
        float distance = Vector3.Distance (_target, transform.position);
        float angle = Vector3.Angle (_target, transform.forward);
        return angle < awarenessAngle * 0.5f && distance <= fullVisionRadius && (this is IDetectSight || this is IDetectSound);
    }
    void LookForTargets () {
        float closestRadius = sightRange * 2;
        Transform current = null;
        Collider[] colliders = Physics.OverlapSphere (transform.position, sightRange);
        for (int i = 0; i < colliders.Length; i++) {
            GameObject g = colliders[i].gameObject;
            if (g.GetComponent<IZombieTarget> () != null) {
                if ((IsTargetInSightRange (g.transform.position) || IsTargetInAwarenessRange (g.transform.position)) && LineOfSight (g)) {
                    float distance = Vector3.Distance (g.transform.position, transform.position);
                    if (distance < closestRadius) {
                        closestRadius = distance;
                        current = g.transform;
                    }
                }
            }
        }
        if (current != null) {
            SetTarget (current, 1);
        } else if (IsTargetTransform ()) {
            SetTarget (GetTargetLocation ().Value, 1);
        }
    }

    public bool ReachedTarget () {
        Vector3? targ = GetTargetLocation ();
        if (targ == null) {
            return true;
        }
        return Vector3.Distance (targ.Value, transform.position) <= targetArrivedRadius;
    }
    public virtual void OnTargetLost () {
        Debug.Log ("searching");
        Search (searchTime);
    }
    void OnDrawGizmosSelected () {
        angleOfSightCone = Mathf.Clamp (angleOfSightCone, 0, 360f);
        Vector3 origin = transform.position.SetY (0.02f);
        Handles.color = new Color (0.4f, 0, 0, 0.2f);
        Vector3 startVector = Quaternion.Euler (0, -angleOfSightCone * 0.5f, 0) * transform.forward;
        Vector3 endVector = Quaternion.Euler (0, angleOfSightCone * 0.5f, 0) * transform.forward;
        if (this is IDetectSight) {
            Handles.DrawSolidArc (origin, Vector3.up, startVector, angleOfSightCone, sightRange);
            Handles.color = new Color (1, 1, 1, 0.5f);
            /* Handles.DrawWireArc (origin, Vector3.up, startVector, angleOfSightCone, sightRange);
            Handles.DrawLine (origin, startVector * sightRange);
            Handles.DrawLine (origin, endVector * sightRange); */
        }
        Handles.color = new Color (0.1f, 0.1f, 0.1f, 0.4f);
        if (this is IDetectSound) {
            Handles.DrawSolidDisc (origin - Vector3.down * 0.01f, Vector3.up, soundRange);

        }
        float closestRadius = sightRange * 2;
        Transform current = null;
        Collider[] colliders = Physics.OverlapSphere (transform.position, sightRange);
        for (int i = 0; i < colliders.Length; i++) {
            GameObject g = colliders[i].gameObject;
            if (g.GetComponent<IZombieTarget> () != null) {
                if ((IsTargetInSightRange (g.transform.position) || IsTargetInAwarenessRange (g.transform.position)) && LineOfSight (g)) {
                    float distance = Vector3.Distance (g.transform.position, transform.position);
                    if (distance < closestRadius) {
                        closestRadius = distance;
                        current = g.transform;
                    }
                }
            }
        }

        Vector3 tar = current != null?current.transform.position : Vector3.one.SetY (0) * 100;
        Handles.color = Color.white;
        Handles.DrawLine (transform.position, tar);
        float vision = AngleOfSightCone (tar);
        Handles.color = new Color (0, 255, 255, 0.1f);
        startVector = Quaternion.Euler (0, -vision * 0.5f, 0) * transform.forward;
        endVector = Quaternion.Euler (0, vision * 0.5f, 0) * transform.forward;
        if (this is IDetectSight) {
            Handles.DrawSolidArc (origin, Vector3.up, startVector, vision, fullVisionRadius);

        }

    }
    bool LineOfSight (GameObject g) {
        RaycastHit hit;
        bool ret = Physics.Linecast (transform.position, g.transform.position, out hit);
        return ret && hit.collider.gameObject == g;
    }
    void OnValidate () {
        if (gameObject.GetComponent<NavMeshAgent> () == null) {
            gameObject.AddComponent<NavMeshAgent> ();
        }
    }
    public float Speed {
        get { return GetComponent<NavMeshAgent> ().speed; }
        set { GetComponent<NavMeshAgent> ().speed = value; }
    }
    void Search (float count) {
        if (count <= 0) {
            return;
        }
        Debug.Log ("spinning: " + count);
        transform.Rotate (Vector3.up, 10f, Space.World);
        Search (count -= Time.deltaTime);

    }
}
public enum AIState { Idle, Patrolling, Searching, Engaged }