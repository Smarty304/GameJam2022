using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject[] slot;
    public Sprite normalBox;
    public Sprite selectedBox; 
    
    public void AddItemToSlot(GameObject item, int slotNumber)
    {
      //  slot[slotNumber].GetComponentInChildren<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        slot[slotNumber].GetComponentsInChildren<Image>()[1].sprite = item.GetComponent<SpriteRenderer>().sprite;
        slot[slotNumber].GetComponentsInChildren<Image>()[1].color = Color.white;
    }

    public void SelectSlot(int slotNumber)
    {
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i].GetComponent<Image>().sprite = normalBox;
        }

        slot[slotNumber].GetComponent<Image>().sprite = selectedBox;
    }
    
}
