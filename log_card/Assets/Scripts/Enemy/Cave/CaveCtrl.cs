using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCtrl : MonoBehaviour
{
    public GameObject bat;
    public GameObject enemy;
    float spawnCool = 20f;
    float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        enemy = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy==null)enemy = GameManager.instance.grid.transform.parent.Find("Enemy").gameObject;
        if (spawnCool <= spawnTimer) { 
            GameObject newBat = Instantiate(bat);
            newBat.transform.SetParent(enemy.transform);
            newBat.transform.position = this.transform.position;
            spawnTimer = 0;
            newBat.GetComponent<EnemyHp>().dropPer = 0;
        }
        spawnTimer += Time.deltaTime;
    }
}
