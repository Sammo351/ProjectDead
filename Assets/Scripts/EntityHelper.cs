using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EntityHelper : MonoBehaviour
{
    static EntityHelper Instance;
    public GameObject PlayerRigidbody;

    public void Awake()
    {
        Instance = FindObjectOfType<EntityHelper>();
    }

    public static void ConvertToRagdoll(GameObject player)
    {

        GameObject ragdoll = GameObject.Instantiate(Instance.PlayerRigidbody);
        ragdoll.transform.position = player.transform.position;
        
        Transform entityRenderer = player.GetComponentInChildren<Animator>().transform.GetChild(0);
        // ragdoll.GetComponent<PlayerRenderer>().PlayerColor = entityRenderer.PlayerColor;
        Texture playerTex = player.GetComponentInChildren<SkinnedMeshRenderer>().material.GetTexture("_MainTex");
        ragdoll.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTexture("_MainTex", playerTex);
        ragdoll.name = ragdoll.name.Replace("(Clone)", "");

        foreach (Transform t in entityRenderer.GetComponentsInChildren<Transform>())
        {
            Rigidbody n = GetRagdollBone(t, ragdoll.GetComponentsInChildren<Rigidbody>());

            if (n != null)
            {
                n.transform.position = t.position;
                n.transform.rotation = t.rotation;
                n.GetComponent<Rigidbody>().velocity = (player.GetComponent<Rigidbody>().velocity * player.GetComponent<Rigidbody>().mass) / n.GetComponent<Rigidbody>().mass;
            }
        }player.gameObject.SetActive(false);
    }

    public static Rigidbody GetRagdollBone(Transform reference, Rigidbody[] transforms)
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            if (transforms[i].name == reference.name)
            {
                return transforms[i];
            }
        }
        return null;
    }

    public static string GetChildPath(Transform t)
    {
        string path = t.name;
        Transform last = t;
        while (last.parent.parent != null)
        {
            path = last.parent.name + "/" + path;
            last = last.parent;
        }

        return path;
    }
}
