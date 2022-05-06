using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLogic : MonoBehaviour
{
    public float moveSpeed = 1.0f;

    public Vector2 Direction = Vector2.right;

    private Rigidbody2D Rig;

    // Start is called before the first frame update
    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 step = Direction * moveSpeed * Time.fixedDeltaTime;

        Rig.position += step;
    }
}
