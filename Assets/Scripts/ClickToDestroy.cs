using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickToDestroy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] TextMeshProUGUI score;
     int currentIndexToSpawn = 0;
    int scoreCount = 0;
  [SerializeField] ParticleSystem particles;
    void Start()
    {
        StartCoroutine(FristObjectToSpawn());
        particles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider!=null)
            {
                GameObject clickedOnObject = hit.collider.gameObject;
                if (clickedOnObject == SpawnManager.Instance.PoolingTheObjectsTOSpawn(currentIndexToSpawn))
                {
                    scoreCount++;
                    score.text = scoreCount.ToString();
                    particles.transform.position = clickedOnObject.transform.localPosition;
                    particles.Play();
                    clickedOnObject.SetActive(false);
                    currentIndexToSpawn++;
                   
                    if (currentIndexToSpawn >= SpawnManager.Instance.AmountOfObejects)
                        currentIndexToSpawn = 0;
                    SpawnManager.Instance.SpawningObjectsAtRandomPosition(currentIndexToSpawn);
                   
                }
            }
        }
    }
    IEnumerator FristObjectToSpawn()
    {
      
        yield return new WaitUntil(() => SpawnManager.Instance.isReady);

     
        SpawnManager.Instance.SpawningObjectsAtRandomPosition(currentIndexToSpawn);
    }

}
