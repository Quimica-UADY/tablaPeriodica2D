using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandlerScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;
    public Transform slots;
    

    public GameObject item{
        get{
            return transform.gameObject;
        }
    }
    
    #region IBeginDragHandler implementation;

    public void OnBeginDrag (PointerEventData eventData) {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    #endregion

    #region IDragHandler implementation;

    public void OnDrag (PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    #endregion

    #region IEndDragHandler implementation;

    public void OnEndDrag (PointerEventData eventData) {
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if(transform.parent == startParent){
            transform.position = startPosition;
        }
        //EXITOSO
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
            }
    }

    #endregion
}
