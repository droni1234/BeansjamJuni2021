using UnityEngine;

[RequireComponent(typeof(Pausable))]
public class RatSpawnManager : MonoBehaviour
{
    public float spawnDelayMin = 5f;
    public float spawnDelayMax = 15f;
    public RatWaypoint[] spawns;
    public int[] spawnsBeforeLevel;
    public GameObject ratPrefab;
    public int ratLevel = 0;
    public int spawned = 0;
    
    private float _delay = 0f;
    private Pausable _pause;
    
    void Start()
    {
        _pause = GetComponent<Pausable>();
        _delay = Random.Range(spawnDelayMin, spawnDelayMax);
    }

    void Update()
    {
        if (_pause.Paused)
        {
            return;
        }
        
        _delay -= Time.deltaTime;

        if (_delay <= 0.0f)
        {
            _delay = Random.Range(spawnDelayMin, spawnDelayMax);

            RatWaypoint spawnPoint = spawns[Random.Range(0, ratLevel + 1)];
            var rat = Instantiate(ratPrefab, spawnPoint.transform.position, Quaternion.identity, transform);
            //rat.GetComponent<Rat>().currentWaypoint = spawnPoint.nextPoint;
            spawned++;
        }

        UpdateRatLevel();
    }

    public void UpdateRatLevel()
    {
        for (int i = spawnsBeforeLevel.Length - 1; i >= 0; i--)
        {
            if (spawned >= spawnsBeforeLevel[i])
            {
                ratLevel = i;
                break;
            }
        }
    }
}
