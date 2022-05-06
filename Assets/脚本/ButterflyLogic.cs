using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyLogic : MonoBehaviour
{
    public GameObject Door;

    public float Speed = 0.1f;

    public GameObject OptionCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Door.GetComponent<Renderer>().enabled)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            

            Vector3 p1 = gameObject.transform.position;
            Vector3 p2 = Door.transform.position;
            
            StartCoroutine(Move(p1,p2));
        }

        if (gameObject.GetComponent<Renderer>().enabled == true)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>().enabled = false;
    }

    IEnumerator Move(Vector3 p1, Vector3 p2)
    {
        Vector3 dir = (p2 - p1).normalized;
        float speed = Speed / 60;

        for(float MoveTime = 10.0f; MoveTime >= 0; MoveTime -= Time.deltaTime)
        {
            gameObject.transform.position += dir * speed;
            if (gameObject.transform.position.x >= p2.x)
            {
                ShowOptions();
                break;
            }
                
            yield return null;
        }
        Destroy(gameObject);

    }

    void ShowOptions()
    {
        Debug.Log("Show");
        GameObject.FindGameObjectWithTag("Player").transform.DetachChildren();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>().enabled = false;
        OptionCanvas.SetActive(true);
    }
}
