using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {
    void OnInteraction ();
    float InteractRange ();
    GameObject GameObject ();
}