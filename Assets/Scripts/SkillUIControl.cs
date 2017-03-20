using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillUIControl : MonoBehaviour
{

    public float coldTime;

    public Text showText;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private float tick;

    private EventTrigger trigger;

    private Image image;
    // Use this for initialization
    void Start()
    {
        trigger = GetComponent<EventTrigger>();
        image = GetComponent<Image>();
        trigger.enabled = false;
        tick = coldTime;
        showText.text = (coldTime + 1).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCD();
    }

    //技能冷却
    void CheckCD()
    {
        //判断技能是否已经可以使用，如果不能使用，则计时
        if (trigger.enabled != true)
        {
            tick -= Time.deltaTime;
            //计时结束，激活技能
            if (tick < 0)
            {
                trigger.enabled = true;
                image.color = Color.white;
                showText.text = "";
            }
            //计时没结束，要更新showtext
            else
            {
                int a = (int)tick + 1;
                showText.text = a.ToString();
            }
        }

    }

    //重制技能
    public void reSet()
    {
        trigger.enabled = false;
        image.color = Color.red;
        tick = coldTime;
        showText.text = (coldTime + 1).ToString();
    }

    public  void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public  void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
