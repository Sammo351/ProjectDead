using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace dUtility
{
    public static class Vector2Extensions
    {


        public static float Value(this Vector2 v, int i)
        {
            i = Mathf.Clamp(i, 0, 1);
            return i == 0 ? v.x : v.y;
        }
        public static Vector3 ToVector3(this Vector2 v, float z = 0)
        {
            return new Vector3(v.x, v.y, z);
        }
        public static Vector2 Clamp(this Vector2 v)
        {
            float x = v.x > 0 ? 1 : v.x < 0 ? -1 : 0;
            float y = v.y > 0 ? 1 : v.y < 0 ? -1 : 0;
            return new Vector2(x, y);
        }
        public static float[] Values(this Vector2 v)
        {
            return new float[] { v.x, v.y };
        }
        public static Vector2 SetX(this Vector2 v, float newX)
        {
            return new Vector2(newX, v.y);
        }
        public static Vector2 SetY(this Vector2 v, float newY)
        {
            return new Vector2(v.x, newY);
        }
        public static Vector2 AdjustX(this Vector2 v, float adj)
        {
            return new Vector2(v.x + adj, v.y);
        }
        public static Vector2 AdjustY(this Vector2 v, float adj)
        {
            return new Vector2(v.x, v.y + adj);
        }
        public static Vector2 Adjust(this Vector2 v, float adj)
        {
            float x = v.x + adj;
            float y = v.y + adj;
            return new Vector2(x, y);
        }

        public static Vector2 Add(this Vector2 v, Vector2 v2)
        {
            float x = v.x + v2.x;
            float y = v.y + v2.y;
            return new Vector2(x, y);
        }
        public static Vector2 Subtract(this Vector2 v, Vector2 v2)
        {
            float x = v.x - v2.x;
            float y = v.y - v2.y;
            return new Vector2(x, y);
        }
        public static Vector2 Multiply(this Vector2 v, Vector2 v2)
        {
            float x = v.x * v2.x;
            float y = v.y * v2.y;
            return new Vector2(x, y);
        }
    }
}