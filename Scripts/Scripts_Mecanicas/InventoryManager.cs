using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;
using TMPro;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject Inventory;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;
    private bool isInventoryOpen = false;

    private void Awake() {
      Instance = this; 
    }

    private void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("No gamepad connected.");
            return; // No gamepad connected.
        }

        // Verificar si el botón Y está presionado
        if (gamepad.buttonNorth.wasPressedThisFrame && (CreditScreen.creditsActive == false && GameOverScreen.gameoverActive == false))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        Inventory.SetActive(isInventoryOpen);
        if (isInventoryOpen)
        {
            ListItems();
        }
    }

    public void Add(Item item) {
        Items.Add(item);
    }

    public void Remove(Item item) {
        Items.Remove(item);
    }

    public bool HasItem(Item.ItemType itemType) {
        return Items.Any(item => item.itemType == itemType);
    }

    public void ListItems() {
        CleanContent();
        foreach (var item in Items) {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (EnableRemove.isOn) removeButton.gameObject.SetActive(true);
        }
        SetInventoryItems();
    }


    public void CleanContent() {
        foreach (Transform item in ItemContent) {
            Destroy(item.gameObject);
        }
    }


    public void EnableItemsRemove() {
        if (EnableRemove.isOn) {
            foreach (Transform item in ItemContent) {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        } else {
            foreach (Transform item in ItemContent) {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems() {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++) {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
}
