using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP
{
    public static void GiveToPlayer(PlayerController pc, int value)
    {
        Inventory inv = pc.Inventory;
        inv.XP += value;
    }
    /* Give XP to all players */
    public static void Give(int value)
    {
        foreach (PlayerController pc in GameObject.FindObjectsOfType<PlayerController>())
        {
            GiveToPlayer(pc, value);
        }
    }
}