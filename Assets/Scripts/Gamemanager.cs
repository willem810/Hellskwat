using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    public Player player { get; private set; }
    public List<GameObject> enemPrefs = new List<GameObject>();
    public List<GameObject> bossPrefs = new List<GameObject>();
    public float Spawnrandomness;
    public float BossRandomness;
    public float spawnRange;
    public float maxSpawnHeight;
    public float minSpawnHeight;
    public GameObject crossHair;
    List<GameObject> enemies = new List<GameObject>();
    List<GameObject> bosses = new List<GameObject>();

    public static Gamemanager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }       
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkSpawn();
    }

    public void setPlayer(Player pl)
    {
        if (this.player == null)
        {
            this.player = pl;
        }
    }

    void checkSpawn()
    {
        float random = Random.Range(0, Spawnrandomness);
        if (random <= 1 && enemPrefs.Count > 0)
        {
            Spawn(getEnemy());
        }

        random = Random.Range(0, BossRandomness);
        if (random <= 1 &&  bossPrefs.Count > 0)
        {
            Spawn(getBoss());
        }
    }    

    void Spawn(GameObject obj)
    {
        Vector3 spawnPosition = Random.onUnitSphere * (spawnRange * 0.5f) + player.transform.position;
        spawnPosition.y = Random.Range(minSpawnHeight, maxSpawnHeight);
        obj.transform.position = spawnPosition;
        obj.transform.LookAt(player.transform);
    }

    GameObject getEnemy()
    {
        foreach (GameObject en in enemies)
        {
            if (!en.activeSelf)
            {
                en.GetComponent<Enemy>().ResetEnemy();
                en.SetActive(true);
                return en;
            }
        }

        GameObject randomEnemPrefs = getRandomFromList(enemPrefs);
        GameObject newEn = Instantiate(randomEnemPrefs, Vector3.zero, Quaternion.identity);
        newEn.transform.SetParent(randomEnemPrefs.transform.parent);
        enemies.Add(newEn);
        return newEn;
    }

    GameObject getBoss()
    {
        foreach (GameObject boss in bosses)
        {
            if (!boss.activeSelf)
            {
                boss.GetComponent<Enemy>().ResetEnemy();
                boss.SetActive(true);
                return boss;
            }
        }

        GameObject randomBossPref = getRandomFromList(bossPrefs);
        GameObject newEn = Instantiate(randomBossPref, Vector3.zero, Quaternion.identity);
        newEn.transform.SetParent(randomBossPref.transform.parent);
        enemies.Add(newEn);
        return newEn;
    }

    private T getRandomFromList<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

   



}
