using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorLogic : MonoBehaviour
{
    //
    //public GameObject targetObject;

    //
    public Transform m_position;

    private GameObject lastOne;
    private GameObject PreOne;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "滑块")
        {
            Instantiate(collision.gameObject, m_position.position, m_position.rotation);

            collision.gameObject.GetComponent<Rigidbody2D>().mass /= 100;
            PreOne = lastOne;
            lastOne = collision.gameObject;

            if (PreOne != null)
                Destroy(PreOne);
        }
    }
}
