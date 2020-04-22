using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    bool inUse = false;
    float _cooldown = 100;

    void Start()
    {

    }

    public virtual void Update()
    {
        if (!inUse)
        {
            _cooldown += Time.deltaTime;
        }
    }

    public virtual bool ReadyToUse()
    {
        return !inUse && _cooldown >= CoolDown();
    }
    public virtual float CoolDown()
    {
        return 10;
    }
    public virtual void OnTriggered()
    {

    }
    public virtual void OnEnd()
    {
        inUse = false;
    }
    public virtual string GetAbilityName()
    {
        return "Blank";
    }



    public float GetCoolDownPercentage()
    {
        float actual = Mathf.Clamp(_cooldown, 0, CoolDown());
        return (actual / CoolDown()) * 100f;
    }
    /* Call this to use ability. Will only trigger if allowed */
    public void Trigger()
    {
        if (ReadyToUse())
        {
            inUse = true;
            OnTriggered();
            _cooldown = 0;
        }
    }

}
