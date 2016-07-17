using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public Octorok octorok;
    public SpawnTester spawnTester;
    public int octorokCount;

    public bool enemiesSpawned;

    private List<Octorok> octoroks;

    private Vector3 spawnPos;

    private Tiled2Unity.TiledMap map;

	// Use this for initialization
	void Start () {
        map = gameObject.GetComponent<Tiled2Unity.TiledMap>();
        octoroks = new List<Octorok>();
        enemiesSpawned = false;

	}
	
	public IEnumerator SpawnEnemies()
    {
        enemiesSpawned = false;
        SpawnOctoroks();
        yield return null;
    }

    public void DestroyEnemies()
    {
        if (octoroks.Count > 0)
        {
            for (int i = 0; i < octoroks.Count; i++)
            {
                octoroks[i].Kill();
            }
            octoroks = new List<Octorok>();
        }
    }

    private IEnumerator SpawnOctorok()
    {
        yield return StartCoroutine(CheckPosition());
        Octorok octo = Instantiate(octorok, spawnPos, Quaternion.identity) as Octorok;
        octoroks.Add(octo);
        if (octoroks.Count == octorokCount)
            enemiesSpawned = true;
    }

    private IEnumerator CheckPosition()
    {
        bool locationFound = false;

        while (!locationFound)
        {
            float posX = (int)Random.Range(gameObject.transform.position.x,
                gameObject.transform.position.x + map.NumTilesHigh) + 0.5f;
            float posY = (int)Random.Range(gameObject.transform.position.y,
                gameObject.transform.position.y - map.NumTilesHigh) + 0.5f;
            Vector3 targetPos = new Vector3(posX, posY, 0);
            SpawnTester testSpawnPos = Instantiate(spawnTester, targetPos, Quaternion.identity) as SpawnTester;
            yield return new WaitForFixedUpdate();
            if (testSpawnPos.allClear)
            {
                spawnPos = targetPos;
                testSpawnPos.Remove();
                locationFound = true;
            }
            else
            {
                testSpawnPos.Remove();
                yield return null;
            }

        }
    }

    private void SpawnOctoroks()
    {
        if (octorokCount == 0)
        {
            enemiesSpawned = true;
        }
        else
        {
            for (int i = 0; i < octorokCount; i++)
            {
                StartCoroutine(SpawnOctorok());
            }
        }
    }
}
