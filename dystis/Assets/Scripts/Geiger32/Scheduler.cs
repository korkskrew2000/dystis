using UnityEngine;
using System.Collections;
using System.Collections.Generic;

struct ScheduleItem {
    public float time;
    public string action;
    public string strParam;
    public MonoBehaviour objParam;
    public ScheduleItem(float time, string action, string strParam, MonoBehaviour objParam) {
        this.time = time;
        this.action = action;
        this.strParam = strParam;
        this.objParam = objParam;
    }
}



public class Scheduler : MonoBehaviour {
    List<ScheduleItem> actions;
    public float defaultDelay = 2f;
    public float markDelay = 0.1f;
    float timer;
    public List<GameObject> textUiObject;
    public GameObject textUiManager;
    TalkUIScript talkScript;
    ScheduleItem previousAction;


    // Use this for initialization
    void Awake() {
        actions = new List<ScheduleItem>();
        talkScript = textUiManager.GetComponent<TalkUIScript>();

    }

    void Start() {
        /*
		TalkAndy("abc abcabc abcabc abcabc abcabc abcabc abcabc abcabc abcabc abc");
		PointText("jee");
		Delay(1.5f);
		TalkDavid("no more");
		ClearPointText();
		InvokeLater(this, "TestInvoke");
		*/

    }
    public void TestInvoke() {
        print("lul");
    }

    void EndSequence() {
        // todo: clear dialog if any
        // todo: restore control to player
        textUiObject[0].SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;
        //pressing space skips dialog
        if (Input.GetKeyDown(KeyCode.Space) && actions.Count != 0) {
            if (previousAction.action != "Delay" && previousAction.action != "InvokeLater") {
                previousAction.action = "Delay";
                timer = Mathf.Clamp(timer, 0f, 0.3f);
                print("Test");
            }

        }
        if (timer > 0) return;
        if (actions.Count == 0) {
            EndSequence();
            return;
        }

        // todo: disable player control

        var a = actions[0];
        if (a.action != "Delay" && a.action != "InvokeLater") {
            previousAction = a;
        }
        //if else hässäkkä


        if (a.action == "TalkAndy") {
            textUiObject[0].SetActive(false);
            timer = a.time + a.strParam.Length * markDelay;
            textUiObject[0].SetActive(true);
            talkScript.SendUiInfo(0, a.strParam);
            //sorkitaan tekstimikälieolikaan juttua
            print(a.action + " " + a.strParam + " and time delay is " + timer);
            actions.RemoveAt(0);

        }
        if (a.action == "TalkTed") {
            textUiObject[0].SetActive(false);
            timer = a.time + a.strParam.Length * markDelay;
            textUiObject[0].SetActive(true);
            talkScript.SendUiInfo(1, a.strParam);
            //sorkitaan tekstimikälieolikaan juttua
            print(a.action + " " + a.strParam + " and time delay is " + timer);
            actions.RemoveAt(0);

        }
        if (a.action == "TalkSteph") {
            textUiObject[0].SetActive(false);
            timer = a.time + a.strParam.Length * markDelay;
            textUiObject[0].SetActive(true);
            talkScript.SendUiInfo(2, a.strParam);
            //sorkitaan tekstimikälieolikaan juttua
            print(a.action + " " + a.strParam + " and time delay is " + timer);
            actions.RemoveAt(0);

        }
        if (a.action == "TalkDavid") {
            textUiObject[0].SetActive(false);
            timer = a.time + a.strParam.Length * markDelay;
            textUiObject[0].SetActive(true);
            talkScript.SendUiInfo(3, a.strParam);
            //sorkitaan tekstimikälieolikaan juttua
            print(a.action + " " + a.strParam + " and time delay is " + timer);
            actions.RemoveAt(0);

        }
        if (a.action == "PointText") {
            timer = 0f;
            textUiObject[1].SetActive(true);
            talkScript.SendHintInfo("Hint:", a.strParam);
            //sorkitaan tekstimikälieolikaan juttua
            print(a.action + " " + a.strParam + " and time delay is " + timer);
            actions.RemoveAt(0);

        }
        if (a.action == "ClearPointText") {
            textUiObject[1].SetActive(false);
            timer = 0f;
            print("closing pointtext");
            actions.RemoveAt(0);

        }
        if (a.action == "InvokeLater") {
            textUiObject[0].SetActive(false);
            //juittuu
            a.objParam.Invoke(a.strParam, 0.0f);
            actions.RemoveAt(0);
        }
        if (a.action == "Delay") {
            textUiObject[0].SetActive(false);
            if (a.time == 0) {
                timer = 1f;
            } else timer = a.time;
            //juittuu
            print("Delay of " + timer + " seconds");
            actions.RemoveAt(0);
        }

    }

    public void TalkAndy(string s) {
        ScheduleItem sch = new ScheduleItem(defaultDelay, "TalkAndy", s, null);
        actions.Add(sch);

    }
    public void TalkTed(string s) {
        ScheduleItem sch = new ScheduleItem(defaultDelay, "TalkTed", s, null);
        actions.Add(sch);

    }
    public void TalkSteph(string s) {
        ScheduleItem sch = new ScheduleItem(defaultDelay, "TalkSteph", s, null);
        actions.Add(sch);
    }
    public void TalkDavid(string s) {
        ScheduleItem sch = new ScheduleItem(defaultDelay, "TalkDavid", s, null);
        actions.Add(sch);

    }
    public void PointText(string s) {
        ScheduleItem sch = new ScheduleItem(defaultDelay, "PointText", s, null);
        actions.Add(sch);
    }
    public void ClearPointText() {
        ScheduleItem sch = new ScheduleItem(defaultDelay, "ClearPointText", "", null);
        actions.Add(sch);
    }

    public void InvokeLater(MonoBehaviour g, string s) {
        ScheduleItem sch = new ScheduleItem(0.0f, "InvokeLater", s, g);
        actions.Add(sch);
    }
    public void Delay(float f) {
        ScheduleItem sch = new ScheduleItem(f, "Delay", "", null);
        actions.Add(sch);
    }
}
