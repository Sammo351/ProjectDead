using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetectSound
{
    void OnDetectSound(Vector3 position);
    void OnDetectSound(Vector3 position, float priority);
}