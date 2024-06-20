using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Systems
{
    public class MouseEnterExitEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event EventHandler OnMouseEnter;
        public event EventHandler OnMouseExit;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Enter");
            OnMouseEnter?.Invoke(this,EventArgs.Empty); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Exit");
            OnMouseExit?.Invoke(this,EventArgs.Empty); 
        }
    }
}
