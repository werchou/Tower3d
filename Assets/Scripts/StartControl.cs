using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //暂时只做了一个关卡，所以这里并没有根据场景索引进行关卡选择，默认进入第一关
    public void  StartGame()
    {
        GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("Level1");
    }

}
