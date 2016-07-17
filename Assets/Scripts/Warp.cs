using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour
{
    public Transform warpTarget;
    private ScreenFader sf;
    Camera mycam;

    void Start()
    {
        mycam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
    }

    void MoveCamera(Tiled2Unity.TiledMap map)
    {
        mycam.transform.position = new Vector3(map.transform.position.x + 8, map.transform.position.y - 5, -10);
    }

    IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            TileManager currentTileManager = gameObject.transform.parent.parent.GetComponent<TileManager>();

            Tiled2Unity.TiledMap newMap = warpTarget.GetComponentInParent<Tiled2Unity.TiledMap>();
            TileManager newTileManager = warpTarget.GetComponentInParent<TileManager>();

            yield return StartCoroutine(sf.FadeToBlack());
            col.gameObject.transform.position = warpTarget.position;
            MoveCamera(newMap);
            currentTileManager.DestroyEnemies();
            yield return newTileManager.SpawnEnemies();
            while (!newTileManager.enemiesSpawned)
            {
                yield return null;
            }

            yield return StartCoroutine(sf.FadeToClear());
        }
    }
}
