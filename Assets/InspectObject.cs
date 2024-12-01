using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InspectObject : MonoBehaviour, IDragHandler
{

    public Transform prizeTransform;
    public void OnDrag(PointerEventData eventData)
    {
        prizeTransform.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
