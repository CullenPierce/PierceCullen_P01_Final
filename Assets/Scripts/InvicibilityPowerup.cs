using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvicibilityPowerup : PowerupBase
{
    public Material material;
    

    protected override void PowerUp(Player player)
    {
        player._damageable = false;
        material.SetColor("_Color", Color.cyan);
    }

    protected override void PowerDown(Player player)
    {
        player._damageable = true;
        material.SetColor("_Color", Color.green);
    }
}
