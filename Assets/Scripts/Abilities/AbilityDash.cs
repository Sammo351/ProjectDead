using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDash : Ability
{
    public float force = 10f;
    RigidbodyConstraints beforeConstraints;
    void Start()
    {

    }

    public override void Update()
    {
        base.Update();
    }
    public override string GetAbilityName()
    {
        return "Dash";
    }
    public override void OnTriggered()
    {
        Debug.Log("Triggered");
        gameObject.layer = LayerMask.NameToLayer("Player-Ignored");
        StartCoroutine("CountDownDash");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        beforeConstraints = rb.constraints;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    public override float CoolDown()
    {
        return 4;
    }
    void Finish()
    {
        GetComponent<Rigidbody>().constraints = beforeConstraints;
        gameObject.layer = LayerMask.NameToLayer("Player");
        OnEnd();
    }
    IEnumerator CountDownDash()
    {
        yield return new WaitForSeconds(0.5f);
        Finish();
    }
}
