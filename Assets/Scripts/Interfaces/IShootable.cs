using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable {
    void OnShot (GameObject projectile, DamagePacket packet);
}