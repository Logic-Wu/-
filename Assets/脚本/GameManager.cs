using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //
    public bool canFixCamera = false;


    public GameObject Menu;
    public GameObject PauseButton;

    public GameObject HeiMu;

    //菜单音效
    public List<AudioClip> CaiDan;

    private GameObject player;

    private AudioSource audioSource;

    public GameObject crewlist;
    public GameObject OptionList;

    //摄像机移动位置
    public float MoveTime = 5.0f;
    private Vector3 Pos1;
    private Vector3 Pos2;
    public Vector3 originalPosition;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        HeiMu.SetActive(true);
        StartCoroutine(KaiMu());


        audioSource = GetComponent<AudioSource>();


        Pos2 = GameObject.FindGameObjectWithTag("BianJie2").transform.position;
        originalPosition = Camera.main.transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Time.timeScale == 0)
            {
                PauseButton.GetComponent<Button>().enabled = false;
            }
            else
            {
                PauseButton.GetComponent<Button>().enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseOnClick();
            }
        }
            
        
    }

    public void StartOnClick()
    {
        audioSource.PlayOneShot(CaiDan[0]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CrewListOnClick()
    {
        audioSource.PlayOneShot(CaiDan[0]);

        if (OptionList.activeSelf)
        {
            crewlist.SetActive(true);
            Debug.Log("isActive");
            OptionList.SetActive(false);
        }
        else
        {
            crewlist.SetActive(false);
            OptionList.SetActive(true);
        }
    }

    //Pause
    public void PauseOnClick()
    {
        audioSource.PlayOneShot(CaiDan[0]);
        Time.timeScale = 0;
        Menu.SetActive(true);
        player.GetComponent<PlayerLogic>().enabled = false;
    }

    //Continue
    public void ContinueOnClick()
    {
        audioSource.PlayOneShot(CaiDan[1]);
        Time.timeScale = 1;
        Menu.SetActive(false);
        player.GetComponent<PlayerLogic>().enabled = true;
    }

    //Restart
    public void RestartOnclick()
    {
        audioSource.PlayOneShot(CaiDan[0]);
        //重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    //Quit
    public void QuitOnClick()
    {
        audioSource.PlayOneShot(CaiDan[0]);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    IEnumerator KaiMu()
    {
        float step = -1.0f / 120;
        for (float time = 2.0f; time >= 0; time -= Time.fixedDeltaTime)
        {
            HeiMu.GetComponent<Image>().color = new Color(HeiMu.GetComponent<Image>().color.r,
                HeiMu.GetComponent<Image>().color.g, HeiMu.GetComponent<Image>().color.b, HeiMu.GetComponent<Image>().color.a + step);
            //Debug.Log(time);
            yield return null;
        }
        HeiMu.GetComponent<Image>().color = new Color(HeiMu.GetComponent<Image>().color.r,
                HeiMu.GetComponent<Image>().color.g, HeiMu.GetComponent<Image>().color.b, 0);

        if (SceneManager.GetActiveScene().buildIndex != 0)
            MoveCamera();
    }

    void MoveCamera()
    {
        player.transform.DetachChildren();
        Pos1 = Camera.main.transform.position;
        Debug.Log(Pos1);
        Debug.Log(Pos2);
        StartCoroutine(Move(Pos1, Pos2));
    }

    IEnumerator Move(Vector3 p1, Vector3 p2)
    {
        float speed = 10.0f / 60;
        Vector3 direction = (p2 - p1).normalized;
        Debug.Log(direction);
        Debug.Log(speed);
        Debug.Log(speed * direction);
        for (float time = MoveTime; time >= 0; time -= Time.fixedDeltaTime)
        {

            Camera.main.transform.position += direction * speed;
            Debug.Log(time);

            if (Camera.main.transform.position.x >= p2.x)
                break;

            yield return null;
        }
        for (float time = 3.0f; time >= 0; time -= Time.fixedDeltaTime)
        {

            Debug.Log(time);
            yield return null;
        }
        for (float time = MoveTime; time >= 0; time -= Time.fixedDeltaTime)
        {

            Camera.main.transform.position -= direction * speed;
            Debug.Log(time);

            if (Camera.main.transform.position.x <= p1.x)
                break;

            yield return null;
        }

        Camera.main.transform.SetParent(player.transform);
        Camera.main.transform.localPosition = originalPosition;

    }

}
