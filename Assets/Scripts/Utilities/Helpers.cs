using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class Ext {
    public static float Map (this float t, float a, float b, float x, float y) {
        return (t - a) / (b - a) * (y - x) + x;
    }

    public static List<T> FindInterfaces<T> () {
        List<T> interfaces = new List<T> ();
        return GameObject.FindObjectsOfType<MonoBehaviour> ().OfType<T> ().ToList ();

    }
    public static Enemies GetAllowedEnemies () {

        Enemies ret = (Enemies) Enum.ToObject (typeof (Enemies), (int) World.enemies);
        return ret;

    }
    public static bool IsEnemyAllowedToSpawn (Enemies enemy) {
        return (World.enemies & enemy) != 0;
    }
}