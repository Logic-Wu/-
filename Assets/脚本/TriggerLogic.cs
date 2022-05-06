using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerLogic : MonoBehaviour
{
    //被控制的物件
    public GameObject Door;

    public GameObject NPC;

    //黑幕
    public GameObject HeiMu;

    public GameObject OptionCanvas;

    //
    private Collider2D m_collider;
    

    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.tag == "Total" && collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }

        if (collision.gameObject.tag == "滑块" && gameObject.tag == "DoorTri")
        {
            foreach(SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
            {
                child.flipX = true;
            }
            OpenDoor();
        }

        if (collision.gameObject.tag == "NPC" && gameObject.tag == "DoorTri")
        {
            foreach (SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
            {
                child.flipX = true;
            }
            OpenDoor();
        }

        if (collision.gameObject.tag == "NPC" && gameObject.tag == "NPC")
        {
            NPC.GetComponent<NPCLogic>().Direction *= -1;
        }

        //关卡传送门
        if (collision.gameObject.tag == "Player" && gameObject.tag == "TelePort")
        {
            StartCoroutine(NextScene(collision));
        }
        
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.tag == "Total" && collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.tag == "Total" && collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    private void OpenDoor()
    {
        Door.GetComponent<Animator>().SetBool("Open", true);
        Door.GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator NextScene(Collider2D player)
    {
        player.GetComponent<PlayerLogic>().MoveSpeed = 0;
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            float step = 1.0f / 60;
            for (float time = 1.0f; time >= 0; time -= Time.fixedDeltaTime)
            {
                HeiMu.GetComponent<Image>().color = new Color(HeiMu.GetComponent<Image>().color.r,
                    HeiMu.GetComponent<Image>().color.g, HeiMu.GetComponent<Image>().color.b, HeiMu.GetComponent<Image>().color.a + step);
                Debug.Log(time);
                yield return null;
            }

            //加载下个场景
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {

            StartCoroutine(Close());
            HeiMu.GetComponentInChildren<Text>().text = "未完待续";


        }

    }

    IEnumerator Close()
    {
        float step = 1.0f / (3*60);
        for (float time = 3.0f; time >= 0; time -= Time.fixedDeltaTime)
        {
            HeiMu.GetComponent<Image>().color = new Color(HeiMu.GetComponent<Image>().color.r,
                HeiMu.GetComponent<Image>().color.g, HeiMu.GetComponent<Image>().color.b, HeiMu.GetComponent<Image>().color.a + step);
            HeiMu.GetComponentInChildren<Text>().color = new Color(HeiMu.GetComponentInChildren<Text>().color.r,
                HeiMu.GetComponentInChildren<Text>().color.g, HeiMu.GetComponentInChildren<Text>().color.b, HeiMu.GetComponentInChildren<Text>().color.a + step);
            Debug.Log(time);
            yield return null;
        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
