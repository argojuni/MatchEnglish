using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDrag : MonoBehaviour
{

    Vector2 SavePosisi;
    public bool IsDiatasObj;

    Transform saveObj;

    public int ID;

    private void Start()
    {
        SavePosisi = transform.position;
    }

    private void OnMouseDown()
    {
        
    }
    private void OnMouseUp()
    {
        if (IsDiatasObj)
        {
            int ID_TempatDrop = saveObj.GetComponent<tempat_drop>().ID;

            if(ID == ID_TempatDrop)
            {
                transform.SetParent(saveObj);
                transform.localPosition = Vector3.zero;
                transform.localScale = new Vector3(0.35f, 0.2f);
                //transform.localScale = saveObj.localScale;

                saveObj.GetComponent<SpriteRenderer>().enabled = false;
                saveObj.GetComponent<Rigidbody2D>().simulated = false;
                saveObj.GetComponent<BoxCollider2D>().enabled = false;

                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                transform.position = SavePosisi;
            }
        }
        else
        {
            transform.position = SavePosisi;
        }
    }
    private void OnMouseDrag()
    {
        Vector2 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Pos;
    }

    private void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.CompareTag("Drop"))
        {
            IsDiatasObj = true;
            saveObj = trig.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.CompareTag("Drop"))
        {
            IsDiatasObj = false;
        }
    }
}
