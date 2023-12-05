using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsPassiveItem : PassiveItem 
{
    protected override void ApplyModifier()
    {
        player.GetComponent<PlayerScript>().currentMoveSpeed *= 1 + passiveItemData.Multiplier / 100f;
    }
}
