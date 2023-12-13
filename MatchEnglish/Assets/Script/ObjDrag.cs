using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ObjDrag : MonoBehaviour
{

    Vector2 SavePosisi;
    public bool IsDiatasObj;

    Transform saveObj;

    public SpriteRenderer spriteRenderer;

    public int ID;

    public Text Texts;

    public UnityEvent OnDragBenar;
    private void Start()
    {
        SavePosisi = transform.position;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
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

                spriteRenderer.sortingOrder -= 3;
                saveObj.GetComponent<SpriteRenderer>().sortingOrder -=4;

                saveObj.GetComponent<SpriteRenderer>().enabled = false;
                saveObj.GetComponent<Rigidbody2D>().simulated = false;
                saveObj.GetComponent<BoxCollider2D>().enabled = false;

                gameObject.GetComponent<BoxCollider2D>().enabled = false;

                OnDragBenar.Invoke();
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
