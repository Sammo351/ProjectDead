using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions {

    //Vector3
    public static Vector2 XY (this Vector3 v) {
        return new Vector2 (v.x, v.y);
    }
    public static Vector2 XZ (this Vector3 v) {
        return new Vector2 (v.x, v.z);
    }
    public static Vector2 YZ (this Vector3 v) {
        return new Vector2 (v.y, v.z);
    }
    public static float Value (this Vector3 v, int i) {
        i = Mathf.Clamp (i, 0, 2);
        switch (i) {
            case 0:
                return v.x;
            case 1:
                return v.y;
            case 2:
                return v.z;
        }
        return v.x;
    }
    public static Vector3 Clamp (this Vector3 v) {
        float x = v.x > 0 ? 1 : v.x < 0 ? -1 : 0;
        float y = v.y > 0 ? 1 : v.y < 0 ? -1 : 0;
        float z = v.z > 0 ? 1 : v.z < 0 ? -1 : 0;
        return new Vector3 (x, y, z);
    }
    public static float[] Values (this Vector3 v) {
        return new float[] { v.x, v.y, v.z };
    }
    public static Vector3 SetX (this Vector3 v, float newX) {
        return new Vector3 (newX, v.y, v.z);
    }
    public static Vector3 SetY (this Vector3 v, float newY) {
        return new Vector3 (v.x, newY, v.z);
    }
    public static Vector3 SetZ (this Vector3 v, float newZ) {
        return new Vector3 (v.x, v.y, newZ);
    }
    public static Vector3 AdjustX (this Vector3 v, float adj) {
        return new Vector3 (v.x + adj, v.y, v.z);
    }
    public static Vector3 AdjustY (this Vector3 v, float adj) {
        return new Vector3 (v.x, v.y + adj, v.z);
    }
    public static Vector3 AdjustZ (this Vector3 v, float adj) {
        return new Vector3 (v.x, v.y, v.z + adj);
    }
    public static Vector3 Adjust (this Vector3 v, float adj) {
        float x = v.x + adj;
        float y = v.y + adj;
        float z = v.z + adj;
        return new Vector3 (x, y, z);
    }

    public static Vector3 Add (this Vector3 v, Vector3 v2) {
        float x = v.x + v2.x;
        float y = v.y + v2.y;
        float z = v.z + v2.z;
        return new Vector3 (x, y, z);
    }
    public static Vector3 Subtract (this Vector3 v, Vector3 v2) {
        float x = v.x - v2.x;
        float y = v.y - v2.y;
        float z = v.z - v2.z;
        return new Vector3 (x, y, z);
    }
    public static Vector3 Multiply (this Vector3 v, Vector3 v2) {
        float x = v.x * v2.x;
        float y = v.y * v2.y;
        float z = v.z * v2.z;
        return new Vector3 (x, y, z);
    }
}