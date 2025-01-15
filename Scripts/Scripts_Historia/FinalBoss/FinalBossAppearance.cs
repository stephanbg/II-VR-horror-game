using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Asegúrate de tener esta directiva using

public class FinalBossAppearance : MonoBehaviour
{
    public GameObject finalBoss;
    public Item.ItemType itemType = Item.ItemType.Heart;

    private bool hasHadItem = false;

    private void Update()
    {
        if (InventoryManager.Instance.HasItem(itemType))
        {
            hasHadItem = true;
        }
        if (hasHadItem && !InventoryManager.Instance.HasItem(itemType))
        {
            if (finalBoss != null)
            {
                finalBoss.SetActive(true);
            }
            hasHadItem = false;
        }
    }
}
