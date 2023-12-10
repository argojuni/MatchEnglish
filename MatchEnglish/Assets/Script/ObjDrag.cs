using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDrag : MonoBehaviour
{
    public Vector2 SavePosisi;

    private void Start()
    {
        SavePosisi = transform.position;
    }

    private void OnMouseDown()
    {
        
    }
    private void OnMouseUp()
    {
        transform.position = SavePosisi;
    }
    private void OnMouseDrag()
    {
        Vector2 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Pos;
    }
}
