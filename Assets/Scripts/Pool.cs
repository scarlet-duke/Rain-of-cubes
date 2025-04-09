using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private float _repeatRate = 0.5f;
    [SerializeField] private int _poolCapacity = 20;
    [SerializeField] private int _poolMaxSize = 40;

    private ObjectPool<GameObject> _pool;
    private float _randomCoordinate = 5f;
    private float _spawnHeight = 40f;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => {
                GameObject obj = Instantiate(_prefab);

                if (obj.GetComponent<Cube>() == null)
                {
                    obj.AddComponent<Cube>();
                }

                return obj;
            },
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void ActionOnGet(GameObject obj)
    {
        obj.transform.position = new Vector3(Random.Range(-_randomCoordinate, _randomCoordinate), _spawnHeight, Random.Range(-_randomCoordinate, _randomCoordinate));
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void OnTriggerEnter(Collider other)
    {
        _pool.Release(other.gameObject);
    }
}
