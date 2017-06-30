using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour {

    [SerializeField]
    GameObject canvasParent;

    [SerializeField]
    GameObject enemyHpBarPrefab;

    [SerializeField]
    Transform enemyTr;
    [SerializeField]
    RectTransform enemyBar;

    struct EnemyBar
    {
        public Transform enemyTr;
        public RectTransform enmeyBar;
    }

    List<EnemyBar> enemyBarList = new List<EnemyBar>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//for(int i =0; i<enemyBarList.Count;i++)
  //      {
  //          Vector3 enemyPos = enemyBarList[i].enemyTr.position;
  //          Vector3 pos = new Vector3(enemyPos.x, enemyPos.y + 10, enemyPos.z);
  //          pos = Camera.main.WorldToScreenPoint(pos);
  //          EnemyBar bar = enemyBarList[i];
  //          bar.enmeyBar.GetComponent<RectTransform>().position = pos;
  //      }

        Vector3 enemyPos = enemyTr.position;
        Vector3 pos;
        pos = new Vector3(enemyPos.x, enemyPos.y, enemyPos.z);
        pos = Camera.main.WorldToScreenPoint(pos);
        pos = new Vector3(pos.x, pos.y - (Screen.height -  Screen.height* 0.1f), 0);
        enemyBar.anchoredPosition = pos;

    }

    public void AddBar(Transform tr)
    {
        EnemyBar enemyBar = new EnemyBar();
        enemyBar.enemyTr = tr;
        GameObject bar = Instantiate(enemyHpBarPrefab);
        enemyBar.enmeyBar = bar.GetComponent<RectTransform>(); 

        enemyBarList.Add(enemyBar);
    }

    public void DeleteBar(int index)
    {
        enemyBarList.Remove(enemyBarList[index]);
    }
}
