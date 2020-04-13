using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmedVehicle : MonoBehaviour, IShootable
{
    public Light light;
    public float LightPulseSpeed = 2f;

    private bool _alarmActivated = false;
    private float _internalTimer = 0f;

    public void OnShot(GameObject projectile, DamagePacket packet)
    {
        if (_alarmActivated)
            return;
        GetComponent<AudioSource>().Play();
        Spawn.Surround(transform.position, 10);

        _alarmActivated = true;
        LightPulseSpeed *= 2f;
        light.range *= 5f;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _internalTimer += Time.deltaTime * LightPulseSpeed;

            light.intensity = Mathf.PingPong(_internalTimer, 2.5f);
    }
}
