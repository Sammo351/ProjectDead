using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ext {
    public static float Map (this float t, float a, float b, float x, float y) {
        return (t - a) / (b - a) * (y - x) + x;
    }
}