using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool isInvincible = false;

    public void TakeDamage() {
        if (isInvincible) return;

        isInvincible = true;
        HealthManager.health--;

        if (HealthManager.health > 0)
        {
            StartCoroutine(GetHurt());
        }
    }


    IEnumerator GetHurt() {
        yield return new WaitForSeconds(5);
        isInvincible = false;
    }
}
