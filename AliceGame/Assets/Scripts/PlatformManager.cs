using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public Transform playerTransform;
    public float spawnOffset = 5f;
    public float destroyDistance = 100f;
    public float portalSpawnPositionX;
    private bool spawnPortal;
    public GameObject portal;
    private string sceneName;

    private GameObject currentPlatform;
    private float nextSpawnPoint;
    private float lastPlatformEndX = 0f;
    private float lastPlatformStartX = 0f;

    private void Start()
    {
        spawnPortal = false;
        if (transform.childCount > 0)
        {
            sceneName = SceneManager.GetActiveScene().name;
            currentPlatform = transform.GetChild(0).gameObject;
            float halfPlatformWidth = currentPlatform.GetComponent<BoxCollider2D>().bounds.size.x / 2;
            lastPlatformStartX = currentPlatform.transform.position.x - halfPlatformWidth;
            lastPlatformEndX = currentPlatform.transform.position.x + halfPlatformWidth;
            nextSpawnPoint = lastPlatformEndX;
        }
    }

    private void Update()
    {
        if ((playerTransform.position.x > nextSpawnPoint - spawnOffset) && canGenerratePlattform())
        {
            SpawnPlatform();
        }
    }

    private void SpawnPlatform()
    {
        GameObject randomPlatformPrefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
        Vector3 spawnPosition = new Vector3(lastPlatformEndX + 10.0f, 0, 0);
        currentPlatform = Instantiate(randomPlatformPrefab, spawnPosition, Quaternion.identity);
        float halfPlatformWidth = currentPlatform.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        lastPlatformStartX = currentPlatform.transform.position.x - halfPlatformWidth;
        lastPlatformEndX = currentPlatform.transform.position.x + halfPlatformWidth;
        nextSpawnPoint = lastPlatformEndX;
        Destroy(currentPlatform, destroyDistance / Mathf.Abs(playerTransform.GetComponent<Rigidbody2D>().velocity.x));
    }
    private bool canGenerratePlattform()
    {
        if(lastPlatformEndX < portalSpawnPositionX || sceneName == "City")
        {
            return true;
        }
        else
        {
            if (!spawnPortal)
            {
                SpawnPortal();
                spawnPortal = true;
            }
            return false;
        }
    }

    private void SpawnPortal()
    {
        Instantiate(portal, new Vector3(lastPlatformEndX + 15.0f, 7.0f, 0), Quaternion.identity);
    }
}