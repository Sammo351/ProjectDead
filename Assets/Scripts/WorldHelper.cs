using UnityEngine;
using System.Collections.Generic;
using dUtility;
using System.Linq;
using System.Collections;
using Mobs;

public class WorldHelper : MonoBehaviour
{
    public GameObject Explosion;

    static WorldHelper Instance;

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    public LayerMask ExplosionLayer;

    public static void AddExplosion(Vector3 point, float Radius, float Force, int damage, Entity attacker = null)
    {
        GameObject g = GameObject.Instantiate(Instance.Explosion, point, Quaternion.identity);
        RaycastHit[] hits = Physics.SphereCastAll(point, Radius, Vector2.one, Radius, Instance.ExplosionLayer);
        //FindObjectOfType<CameraShake>().ShakeCamera(1f, Time.deltaTime);

        foreach (RaycastHit hit in hits)
        {
            float perc = Radius - hit.distance;
            perc = Mathf.Sqrt(perc);

            Vector3 direction = hit.point - point;

            RaycastHit newHit;

            // if (Physics.Raycast(point, direction, out newHit, Radius, Instance.ExplosionLayer))
            // {
            //     if (newHit.collider.gameObject == hit.collider.gameObject)
            //     {
                    if (hit.collider.gameObject.GetComponent<Entity>())
                    {
                        //DamageSystem.Damage(hit.collider.gameObject.GetComponent<Entity>(), damage, attacker);
                    }

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForceAtPosition(direction * Force * perc, point, ForceMode.VelocityChange);
                    }
                }
           // }

        //}


        Instance.StartCoroutine(Instance.ApplyExplosiveForces(point, Radius, Force));


        Destroy(g, 2);

    }

    public IEnumerator ApplyExplosiveForces(Vector3 point, float Radius, float Force)
    {
        yield return new WaitForFixedUpdate();

        RaycastHit[] hits = Physics.SphereCastAll(point, Radius, Vector2.one, Radius, Instance.ExplosionLayer);
        foreach (RaycastHit hit in hits)
        {
            float perc = Radius - hit.distance;
            perc = Mathf.Sqrt(perc);

            Vector3 direction = hit.point - point;

            RaycastHit newHit;

            // if (Physics.Raycast(point, direction, out newHit, Radius, Instance.ExplosionLayer))
            // {
            //     if (newHit.collider == hit.collider)
            //     {

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForceAtPosition(direction * Force * perc, point, ForceMode.VelocityChange);
                    }
            //     }
            // }
        }

    }

    // IEnumerator WaitForRagdolls(Rigidbody rigidbody, Vector3 direction, float Force, float perc, Vector3 point)
    // {
    //     yield return new WaitForEndOfFrame();

    // }



    public static EntityTargetable FindNearestTarget(Transform transform, params EntityTargetable[] excluded)
    {
        List<EntityTargetable> targets;
        targets = GameObject.FindObjectsOfType<EntityTargetable>().ToList();
        List<EntityTargetable> filteredList = new List<EntityTargetable>();
        Debug.Log("Found Targets: " + targets.Count);

        foreach (EntityTargetable check in targets)
        {
            if (!excluded.Contains(check))
            {
                filteredList.Add(check);
            }
        }

        Debug.Log("Filtered Targets: " + filteredList.Count);
        EntityTargetable target = filteredList[0];
        Debug.Log("Found Target: " + target);
        return target;
    }
}

public static class WorldExtensions
{
    // public static EntityPlayer FindNearestTarget(this Transform transform)
    // {
    //     return World.FindNearestTarget(transform);
    // }
}