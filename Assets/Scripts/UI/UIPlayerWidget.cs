using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerWidget : MonoBehaviour
{
    public PlayerController playerController;
    public Weapon playerWeapon;
    public Slider HealthSlider;
    public Slider StaminaSlider;
    public Slider UltimateSlider;
    public Text AmmoCounter;
    public Text WeaponName;

    Inventory inventory;
    private void Start()
    {
        inventory = playerController.Inventory;
        playerWeapon = inventory.GetActiveWeapon();

    }


    void Update()
    {
        if (playerController == null) return;

        HealthSlider.value = (float)playerController.Health / (float)playerController.maxHealth;
        StaminaSlider.value = playerController.currentStamina / playerController.maxStamina;
        playerWeapon = inventory.GetActiveWeapon();
        if (playerWeapon.reloading)
            AmmoCounter.text = "Reloading! " + " (" + playerWeapon.Ammo + ")";
        else
            AmmoCounter.text = playerWeapon.currentClip + "/" + playerWeapon.clipSize + " (" + playerWeapon.Ammo + ")";

        WeaponName.text = playerWeapon != null ? playerWeapon.weaponName : "Melee";
    }
}
