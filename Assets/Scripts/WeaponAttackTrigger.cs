using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackTrigger : MonoBehaviour
{
    public BoxCollider weaponCollider;
    public void SpearIsTriggerOn()
    {
        weaponCollider.isTrigger = true;
    }

    public void SpearIsTriggerOff()
    {
        weaponCollider.isTrigger = false;
    }
}
