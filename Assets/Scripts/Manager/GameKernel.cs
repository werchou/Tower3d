using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameKernel : MonoBehaviour {

    public MoneySystem SetMoneySystem;

    public  static MoneySystem MoneySys = new MoneySystem();

    public  int InitLife;

    public float GameTime;

    public Text MoneyUI;

    public Text HealthyUI;

    public Text TimeUI;

    public Text TipsUI;

    private static InputSystem InputManager = new InputSystem();

    private static int currentLife;

    private GameObject menu;

    private GameObject scoreUI;

    void Awake()
    {
        menu = TipsUI.transform.FindChild("Menu").gameObject;
        scoreUI = TipsUI.transform.FindChild("Score").gameObject;
        Time.timeScale = 0;
    }


    // Use this for initialization
    void Start () {

        //Time.timeScale = 1;
        //初始化金钱系统
        MoneySys = SetMoneySystem;
        MoneySys.Init();
        HealthyUI.text = InitLife.ToString();
        currentLife = InitLife;

        
    }
	
	// Update is called once per frame
	void Update () {
        UpdateUI();

        InputManager.Update();

        CheckGameState();
    }

    void UpdateUI()
    {
        //更新金钱显示
        MoneyUI.text = MoneySys.getCurrent.ToString();
        //更新生命值
        HealthyUI.text = currentLife.ToString();
        //更新时间显示
        TimeTextUpdate();
    }

    void TimeTextUpdate()
    {
        GameTime -= Time.deltaTime;
        int minute = (int)(GameTime / 60);
        int second = (int)(GameTime % 60);

        string showtime = minute.ToString() + ":" + second.ToString();

        TimeUI.text = showtime;
    }

    //修改生命
    public static void ChangeLife(int value)
    {
        currentLife += value;
    }
    
    void CheckGameState()
    {
        //生命值为0 game over
        if (currentLife <= 0)
        {
            HealthyUI.text = "0";
            //暂停所有音乐
            AudioSource[] allmusic = FindObjectsOfType<AudioSource>();
            foreach (AudioSource sound in allmusic)
            {
                sound.enabled = false;
            }

            //TipsUI.text = "Game Over!";
            GameObject OverText = TipsUI.transform.FindChild("GameOverText").gameObject;

            OverText.SetActive(true);
            menu.SetActive(true);

            Time.timeScale = 0;
        }

        //时间耗尽 win 
        if (GameTime <= 0)
        {
            //暂停所有音乐
            AudioSource[] allmusic=FindObjectsOfType<AudioSource>();
            foreach(AudioSource sound in allmusic)
            {
                sound.enabled=false;
            }

            menu.SetActive(true);
            scoreUI.SetActive(true);
            GameObject winText = scoreUI.transform.FindChild("WinText").gameObject;
            winText.SetActive(true);

            int starcount = currentLife / (InitLife/3);
            switch (starcount)
            {
                case 3:
                case 2:
                    {
                        foreach (Transform child in scoreUI.transform)
                        {
                            child.gameObject.SetActive(true);
                        }
                        break;
                    }
                case 1:
                    {
                        GameObject star1 = scoreUI.transform.GetChild(0).gameObject;
                        GameObject star2= scoreUI.transform.GetChild(1).gameObject;
                        star1.SetActive(true);
                        star2.SetActive(true);
                        break;
                    }

                case 0:
                    {
                        GameObject star1 = scoreUI.transform.GetChild(0).gameObject;
                        star1.SetActive(true);
                        break;
                    }
                default:break;
            }

            menu.SetActive(true);
            Time.timeScale = 0;
        }

    }


    //确认开始游戏
    public void ConfirmStart()
    {
        GameObject guide = TipsUI.transform.FindChild("GameGuide").gameObject;
        guide.SetActive(false);
        Time.timeScale = 1;
    }


    public  void PauseGame()
    {
        AudioSource[] allmusic = FindObjectsOfType<AudioSource>();
        foreach (AudioSource sound in allmusic)
        {
            sound.Pause();
        }

        Time.timeScale = 0;
        
        menu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;

        AudioSource[] allmusic = FindObjectsOfType<AudioSource>();
        foreach (AudioSource sound in allmusic)
        {
            sound.UnPause();
        }


        menu.SetActive(false);
    }

    public void ReStart()
    {
        
        SceneManager.LoadScene("Level1");
        
    }

    public void BackHome()
    {
        
        SceneManager.LoadScene("Start");
       
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
