using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointLogic : MonoBehaviour
{
    //预制前景匹配项
    public GameObject MatchObject;

    //Demo
    //翘板
    public GameObject QiaoBan;
    //NPC拐点
    public GameObject NPCTrigger;

    //XianJie
    //指示灯
    public GameObject Light;
    //风车
    public GameObject FengChe;

    //Park2
    public GameObject Conveyer;


    //匹配距离
    private float distance;
    //匹配项的render
    private Renderer m_renderer;

    //是否变大
    //private bool Bigger = false;
    private Vector3 OriginalPosition;


    private GameObject Player;
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        m_renderer = MatchObject.GetComponent<Renderer>();

        //开始时匹配项不可见
        m_renderer.enabled = false;

        

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        OriginalPosition = manager.originalPosition;
        Vector2 ScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 m_ScreenPos = Camera.main.WorldToScreenPoint(MatchObject.transform.position);

        distance = Vector2.Distance(ScreenPos, m_ScreenPos);

        //Debug.Log(distance);

        //当与匹配项足够近时，可以进行拼接
        if (distance <= 7.5f)
        {
            //if (!Bigger)
            //{
            //    Debug.Log("kaishi");
            //    StartCoroutine(BianDa());
            //    Bigger = true;
            //}
                
            
            manager.canFixCamera = true;

            if (Input.GetKeyDown(KeyCode.R))
            {
                Player.GetComponent<PlayerLogic>().enabled = false;

                gameObject.GetComponent<Renderer>().enabled = true;

                StartCoroutine(P_BianXiao());
                ////拼接支架
                //if (gameObject.tag == "holder")
                //    holderJoint();
                ////拼接传送带
                //if (gameObject.tag == "conveyor")
                //    ConveyorJoint();
                ////guaiwu pingtai
                //if (gameObject.tag == "platform")
                //    NPCplatform();
                ////发电站和灯塔
                //if (gameObject.tag == "发电站")
                //    EleAndLight();
                ////风车
                //if (gameObject.tag == "风车")
                //    WindAndTower();
                ////船帆
                //if (gameObject.tag == "船帆")
                //    Sail();
                ////一般碰撞物，如平台
                //if (gameObject.tag == "碰撞")
                //    SimpleCollider();
                //gameObject.SetActive(false);
                //m_renderer.enabled = true;
                
            }
        }
        //else
        //{
        //    if (Bigger)
        //    {
        //        Debug.Log("kaishibianxiao");
        //        StartCoroutine(BianXiao());
        //        Bigger = false;
        //    }
        //    m_renderer.enabled = false;
        //}
            
        
    }

    private void FixedUpdate()
    {
        
    }


    IEnumerator BianDa()
    {
        Debug.Log("bianda");
        Vector3 step = new Vector3(0.05f, 0.05f, 0f);
        for (float time = 0.2f; time>=0;time -= Time.fixedDeltaTime)
        {
            gameObject.transform.localScale += step;
            Debug.Log(time);
            yield return null;
        }
    }

    IEnumerator BianXiao()
    {
        Debug.Log("bianxiao");
        Vector3 step = new Vector3(0.05f, 0.05f, 0f);
        for (float time = 0.2f; time >= 0; time -= Time.fixedDeltaTime)
        {
            gameObject.transform.localScale -= step;
            Debug.Log(time);
            yield return null;
        }
    }


    IEnumerator P_BianXiao()
    {
        Vector3 step = new Vector3(0.01f, 0.01f, 0f);

        Debug.Log("bianda");
        
        for (float time = 0.2f; time >= 0; time -= Time.fixedDeltaTime)
        {
            gameObject.transform.localScale += step;
            Debug.Log(time);
            yield return null;
        }

        Debug.Log("pinjie bianxiao");
        for (float time = 0.2f; time >= 0; time -= Time.fixedDeltaTime)
        {
            gameObject.transform.localScale -= step;
            Debug.Log(time);
            yield return null;
        }


        
        //拼接支架
        if (gameObject.tag == "holder")
            holderJoint();
        //拼接传送带
        if (gameObject.tag == "conveyor")
            ConveyorJoint();
        //guaiwu pingtai
        if (gameObject.tag == "platform")
            NPCplatform();
        //发电站和灯塔
        if (gameObject.tag == "发电站")
            EleAndLight();
        //风车
        if (gameObject.tag == "风车")
            WindAndTower();
        //船帆
        if (gameObject.tag == "船帆")
            Sail();
        //一般碰撞物，如平台
        if (gameObject.tag == "碰撞")
            SimpleCollider();

        GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>().Play();


        //教学关卡
        if (gameObject.tag != "title")
        {
            //
            float shakeTime = 1.0f;
            float ShakeAmout = 4.0f;
            float shakeSpeed = 5.0f;



            float RunTime = 0f;

            while (RunTime < shakeTime)
            {
                Debug.Log(RunTime);

                //在y轴生成随机振动
                Vector3 randomPoint = OriginalPosition +
                    new Vector3(Random.insideUnitSphere.x * ShakeAmout, Random.insideUnitSphere.y * ShakeAmout, 0);

                Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, randomPoint, Time.deltaTime * shakeSpeed);

                yield return null;

                RunTime += Time.deltaTime;
            }

            Camera.main.transform.localPosition = OriginalPosition;
            //
        }
        else
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("ChaiFen"))
                Destroy(item);
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Total"))
                Destroy(item);
        }
            

        gameObject.SetActive(false);
        m_renderer.enabled = true;

        Player.GetComponent<PlayerLogic>().enabled = true;
    }

    //Title Joint for main menu
    void TitleJoint()
    {

    }

    //拼接支架
    void holderJoint()
    {
        //将翘板的刚体类型置为动态
        QiaoBan.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //激活铰链
        MatchObject.GetComponent<Joint2D>().enabled = true;
    }

    //拼接传送带
    void ConveyorJoint()
    {
        //激活其碰撞
        MatchObject.GetComponent<Collider2D>().enabled = true;
        //
    }

    //拼接怪物平台
    void NPCplatform()
    {
        NPCTrigger.SetActive(false);
    }

    //拼接发电站与灯塔
    void EleAndLight()
    {
        Light.GetComponent<Renderer>().enabled = true;
        Light.GetComponent<Animator>().SetBool("ele", true);
        MatchObject.GetComponent<AudioSource>().Play();
        if (!GameObject.FindGameObjectWithTag("风车"))
        {
            GameObject.FindGameObjectWithTag("船").GetComponent<ConstantForce2D>().enabled = true;
            GameObject.FindGameObjectWithTag("船").GetComponent<ConstantForce2D>().force = new Vector2(1, 0);
            FengChe.GetComponent<Animator>().SetBool("EleOn", true);
            FengChe.GetComponent<AudioSource>().Play();
        }
    }

    //拼接风车与灯塔
    void WindAndTower()
    {
        if (!GameObject.FindGameObjectWithTag("发电站"))
        {
            GameObject.FindGameObjectWithTag("船").GetComponent<ConstantForce2D>().enabled = true;
            GameObject.FindGameObjectWithTag("船").GetComponent<ConstantForce2D>().force = new Vector2(1, 0);
            MatchObject.GetComponent<Animator>().SetBool("EleOn", true);
            MatchObject.GetComponent<AudioSource>().Play();
        }
    }

    //拼接船帆
    void Sail()
    {
        GameObject.FindGameObjectWithTag("船").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    void SimpleCollider()
    {
        MatchObject.GetComponent<Collider2D>().enabled = true;
        //如果练的有传送带（即park2里面的
        if (Conveyer != null)
        {
            Conveyer.GetComponent<Collider2D>().enabled = true;
        }
    }
}
