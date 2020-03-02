using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

    void OnDamaged (Entity attacker, int damageAmount, DamageType damageType);
}