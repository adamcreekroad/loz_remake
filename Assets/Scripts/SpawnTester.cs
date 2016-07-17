using UnityEngine;
using System.Collections;

public class SpawnTester : MonoBehaviour
{
    public bool allClear;

    public void Start()
    {
        allClear = true;
    }

	public void OnTriggerEnter2D(Collider2D col)
    {
        allClear = false;
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
