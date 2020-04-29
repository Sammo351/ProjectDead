using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class Ext
{
    public static float Map(this float t, float a, float b, float x, float y)
    {
        return (t - a) / (b - a) * (y - x) + x;
    }

    public static List<T> FindInterfaces<T>()
    {
        List<T> interfaces = new List<T>();
        return GameObject.FindObjectsOfType<MonoBehaviour>().OfType<T>().ToList();

    }
    public static Enemies GetAllowedEnemies()
    {

        Enemies ret = (Enemies)Enum.ToObject(typeof(Enemies), (int)World.enemies);
        return ret;

    }
    public static bool IsEnemyAllowedToSpawn(Enemies enemy)
    {
        return (World.enemies & enemy) != 0;
    }
    public static bool LineOfSight(GameObject g1, GameObject g2)
    {
        Vector3 dir = (g2.transform.position - g1.transform.position).normalized;
        float distance = Vector3.Distance(g1.transform.position, g2.transform.position);
        RaycastHit[] hits = Physics.RaycastAll(g1.transform.position, dir, distance);
        bool hitSomething = false;
        Debug.Log("Count: " + hits.Length);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            GameObject g = hit.collider.gameObject;
            if (g != g1 && g != g2)
            {
                hitSomething = true;
                break;
            }
        }
        return !hitSomething;

    }
}