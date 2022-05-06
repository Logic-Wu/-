using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BianjieLogic : MonoBehaviour
{
    bool cameraFix = true;

    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(originalPosition);
    }

    // Update is called once per frame
    void Update()
    {
        originalPosition = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().originalPosition;
        if (Camera.main.transform.parent)
        {
            cameraFix = true;
        }
        else
            cameraFix = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (cameraFix)
            {
                //解除与camera的父子关系
                collision.transform.DetachChildren();
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!cameraFix)
            {
                Camera.main.transform.SetParent(collision.transform);
                Camera.main.transform.localPosition = originalPosition;
            }
            Camera.main.transform.localPosition = originalPosition;

        }
    }
}
