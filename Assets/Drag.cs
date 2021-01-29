using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Vector2 _origin;

    private Vector2 _distance;
    
    private Vector2 _lastPosition;

    public Rotor rotor;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _origin = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        _distance = eventData.position - _lastPosition;

        _lastPosition = eventData.position;
        
        //rotor.Rotate(_distance);
    }
}