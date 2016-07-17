using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Tiled2Unity.TiledMap map;

    public float mSpeed;

    Camera mycam;

	// Use this for initialization
	void Start () {
        mycam = GetComponent<Camera>();
        mycam.orthographicSize = 6;
    }
	
	// Update is called once per frame
	void Update () {
        
        
	}

    public void SetPosition()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Tiled2Unity.TiledMap>();
        transform.position = new Vector3(map.transform.position.x + 8, map.transform.position.y - 5, 0);
    }
}
