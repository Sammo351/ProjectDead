using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Senses {
    //Priority- higher number = higher priority. Sight starts at 1, sound =0
    public static void TriggerSightAlert (Transform tr, int priority = 1) {
        List<IDetectSight> list = Ext.FindInterfaces<IDetectSight> ();
        list.ForEach ((x) => x.OnDetectSight (tr, priority));

    }
    public static void TriggerSoundAlert (Vector3 position) {
        List<IDetectSound> list = Ext.FindInterfaces<IDetectSound> ();
        list.ForEach ((x) => x.OnDetectSound (position));

    }
}