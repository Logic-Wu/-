using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//传送带脚本
public class ConveyorLogic : MonoBehaviour
{
    //传送带方向
    public Vector2 Direction = Vector2.left;
    //
    public float ConveySpeed = 1.0f;

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
        //传送带初始位置
        Vector2 pos = Rig.position;
        //步长，传送带移动方向与物体相反
        Vector2 step = -Direction * ConveySpeed * Time.fixedDeltaTime;

        //只移动传送带
        Rig.position += step;
        //移动传送带以及传送带上的物体
        Rig.MovePosition(pos);
    }
}
