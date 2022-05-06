using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatLogic : MonoBehaviour
{
    public GameObject Rope;

    public float force = 40;

    private Rigidbody2D boat;

    // Start is called before the first frame update
    void Start()
    {
        boat = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(boat.velocity.x) < 0.01)
        {
            boat.constraints = RigidbodyConstraints2D.FreezePositionY;
            boat.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            boat.constraints = RigidbodyConstraints2D.None;

            //船行走时按空格 船变快
            if (Input.GetKeyDown(KeyCode.Space))
                GetComponentInParent<ConstantForce2D>().force += new Vector2(10, 0);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Rope.GetComponent<HingeJoint2D>().enabled = false;
            GetComponentInParent<ConstantForce2D>().force = new Vector2(force, 0);
            
        }
            
    }
}
