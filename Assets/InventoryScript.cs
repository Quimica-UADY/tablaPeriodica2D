using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    // [SerializeField] Text inventoryText;
    // Start is called before the first frame update
    void Start()
    {
        HasChanged ();
    }

    #region IHasChanged implementation
    public void HasChanged () {
        // System.Text.StringBuilder builder = new System.Text.StringBuilder();
        // builder.Append(" - ");
        foreach (Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<SlotHandler>().item;
            if (item)
            {
                foreach (Transform slotItem in slots){
                    float distance = Vector3.Distance(slotItem.transform.position, item.transform.position);
                    if (distance < 27)
                    {
                        item.transform.position = slotItem.transform.position;
                    }   
                }
            }
            // GameObject item = slotTransform.GetComponent<SlotHandler>().item;
            // if (item)
            // {
            //     string nameItem = item.name;
            //     GameObject labl = GameObject.Find(nameItem + "-Slot");

            //     if (labl.transform.position == item.transform.position)
            //     {
            //         Debug.Log("Si");
            //     }
                
            // }
        }
        // inventoryText.text = builder.ToString ();
    }
    #endregion
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler {
        void HasChanged();
    }
}
