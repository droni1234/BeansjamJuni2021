using UnityEngine;

public class RatSpawnManager : MonoBehaviour
{
    public float spawnDelayMin = 5f;
    public float spawnDelayMax = 15f;
    public RatWaypoint[] spawns;
    public GameObject ratPrefab;

    private float _delay = 0f;
    private int _ratLevel = 0;
    
    void Start()
    {
        _delay = Random.Range(spawnDelayMin, spawnDelayMax);
    }

    void Update()
    {
        _delay -= Time.deltaTime;

        if (_delay <= 0.0f)
        {
            _delay = Random.Range(spawnDelayMin, spawnDelayMax);

            RatWaypoint spawnPoint = spawns[Random.Range(0, _ratLevel + 1)];
            var rat = Instantiate(ratPrefab, spawnPoint.transform.position, Quaternion.identity);
            rat.GetComponent<Rat>().currentWaypoint = spawnPoint.nextPoint;
        }
    }
    
    // TODO: A method to increase ratLevel which will spawn more difficult rats
}
