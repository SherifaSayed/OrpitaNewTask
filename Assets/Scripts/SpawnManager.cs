using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject TargetPrefab;
    private List<GameObject> objectsToSpawn = new List<GameObject>();
    int amountOfObejects=5;
    float offset = 1f;

    public static SpawnManager Instance;
    public bool isReady = false;

    public int AmountOfObejects { get => amountOfObejects; set => amountOfObejects = value; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        for(int i =0;i<AmountOfObejects; i++)
        {
            GameObject spawnObject = Instantiate(TargetPrefab);
            spawnObject.SetActive(false);
            objectsToSpawn.Add(spawnObject);
        }
        isReady = true;
    }

  public GameObject PoolingTheObjectsTOSpawn(int index)
    {
        if (index>=0 && index < objectsToSpawn.Count)
        {
            
                return objectsToSpawn[index];
        }
        return null;

    }

  public void SpawningObjectsAtRandomPosition(int index)
    {
        GameObject obj = PoolingTheObjectsTOSpawn(index);
        if (obj != null)
        {
            float screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + offset;
            float screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - offset;
            float screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - offset;
            float screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y + offset;

            float randX = Random.Range(screenLeft, screenRight);
            float randY = Random.Range(screenBottom, screenTop);

            Vector3 randomPositionToSpawn = new Vector3(randX, randY, 0);
            obj.transform.position = randomPositionToSpawn;
            obj.SetActive(true);
        }
    }
}
