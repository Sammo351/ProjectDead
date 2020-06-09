using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    // Start is called before the first frame updat
    LineRenderer line;
    Light lightSource;
    GameObject lightObj;
    //public Gradient color;
    Color color;
    public Material material;
    public LayerMask layers;
    void Start()
    {
        if (line == null)
        {
            line = gameObject.AddComponent<LineRenderer>();
        }
        if (lightSource == null)
        {
            lightObj = new GameObject("Laser Focus");
            lightSource = lightObj.AddComponent<Light>();
            lightSource.enabled = false;
            //lightObj.transform.parent = transform.parent;
        }
        color = GetComponentInParent<PlayerController>().PlayerColor;
        lightSource.color = color;
        lightSource.shadows = LightShadows.Soft;
        lightSource.intensity = 0.5f;
        line.startWidth = 0.07f;
        line.endWidth = 0.07f;
        line.startColor = color;
        line.endColor = color;
        line.material = new Material(material);
        line.material.SetColor("_EmissionColor", color);
        line.receiveShadows = false;
        line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

    }

    // Update is called once per frame
    void Update()
    {
        color = GetComponentInParent<PlayerController>().PlayerColor;
        Vector3[] pos = new Vector3[2];
        line.positionCount = 2;
        pos[0] = transform.position;


        layers = Physics.AllLayers & ~(1 << LayerMask.NameToLayer("Player")) & ~(1 << LayerMask.NameToLayer("Player-Ignored"));

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 50f, layers))
        {
            pos[1] = hit.point;

            lightObj.transform.position = hit.point + (hit.normal * 0.25f);
            lightSource.enabled = true;

        }
        else
        {
            pos[1] = transform.position + transform.forward * 50;
            lightSource.enabled = false;
        }
        line.SetPositions(pos);

    }
}
