using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private float _repeatRate = 0.5f;
    [SerializeField] private int _poolCapacity = 20;
    [SerializeField] private int _poolMaxSize = 40;

    private ObjectPool<Cube> _pool;
    private float _randomCoordinate = 5f;
    private float _spawnHeight = 40f;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () =>  Instantiate(_prefab),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false));
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private void ActionOnGet(Cube obj)
    {
        obj.transform.position = new Vector3(Random.Range(-_randomCoordinate, _randomCoordinate), _spawnHeight, Random.Range(-_randomCoordinate, _randomCoordinate));
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.gameObject.SetActive(true);
        obj.Disappeard += Release;
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void Release(Cube obj)
    {
        obj.Disappeard -= Release;
        _pool.Release(obj);
    }
}
