using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueManager : MonoBehaviour {

    int outputIndex;
    struct TalkBox
    {
        public string name;
        public string spriteNumber;
        public string talkText;
    }

    List<TalkBox> talkList = new List<TalkBox>();

	// Use this for initialization
	void Start () {

        LoadTxt(1);

    }

   
    public void TalkListClear()
    {
        talkList.Clear();
    }

    public void Get()
    {

    }

    public void LoadTxt(int index)
    {
        TextAsset data = Resources.Load(GetTalkFullPath(index), typeof(TextAsset)) as TextAsset;
        string[] loadStrArr = data.text.Split('\n');
        string txt = null;
        for (int i = 0; i < loadStrArr.Length; i++)
            txt += loadStrArr[i];

        string[] splitNumAndTalk = txt.Split('*');
        txt = null;
        for (int i = 0; i < splitNumAndTalk.Length; i++)
            txt += splitNumAndTalk[i];

        string[] splitTalk = txt.Split('@');

        txt = null;
        for (int q = 0; q < splitTalk.Length; q++)
            txt += splitTalk[q];

        for (int i = 1; i < splitTalk.Length; i++)
        {
            TalkBox talk = new TalkBox();
            string strAdd = null;
            for (int j = 0; j < splitTalk[i].Length; j++)
                strAdd += splitTalk[i][j];

            string[] split = strAdd.Split('#');
            talk.name = split[1];
            talk.spriteNumber = split[2];
            talk.talkText = split[3];

            talkList.Add(talk);

        }
    }

    string GetTalkFullPath(int index)
    {
        return Define.talkRoot + GetTalkPath(index);
    }

    string GetTalkPath(int index)
    {
        string indexStr = index.ToString();
        return "/" + indexStr + "_talk";
    }
}
