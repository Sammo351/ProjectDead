using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Interactable : MonoBehaviour, IInteractable {
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
    public virtual void OnInteraction () {
        Debug.Log ("Successful interaction");
    }
    public virtual float InteractRange () {
        return 1.9f;
    }
    public virtual GameObject GameObject () {
        return gameObject;
    }
    void OnDrawGizmos () { Handles.DrawWireDisc (transform.position, Vector3.up, InteractRange ()); }

}