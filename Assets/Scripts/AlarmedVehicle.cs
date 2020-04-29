using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmedVehicle : MonoBehaviour, IShootable
{
    public Light light;
    public float LightPulseSpeed = 4f;
    public float lightRangeMultiplier = 5f;

    private bool _alarmActivated = false;
    private float _internalTimer = 0f, _durationTimer = 0;
    public float alarmDuration = 4f;
    public float zombieTargetPriority = 5;

    public void OnShot(GameObject projectile, DamagePacket packet)
    {
        if (_alarmActivated) { _durationTimer = 0; return; }

        GetComponent<AudioSource>().Play();
        Spawn.SurroundWithPriority(transform.position, 10, zombieTargetPriority);
        Senses.TriggerSoundAlert(transform.position, zombieTargetPriority);

        _alarmActivated = true;
        //LightPulseSpeed *= 2f;
        light.range *= lightRangeMultiplier;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (_alarmActivated)
        {
            _internalTimer += Time.deltaTime * LightPulseSpeed;

            light.intensity = Mathf.PingPong(_internalTimer, 2.5f);
            _durationTimer += Time.deltaTime;
            Senses.TriggerSoundAlert(transform.position, zombieTargetPriority);
            if (_durationTimer >= alarmDuration)
            {
                _durationTimer = 0;
                _alarmActivated = false;
                GetComponent<AudioSource>().Stop();
                light.range /= lightRangeMultiplier;

            }
        }
    }
}
