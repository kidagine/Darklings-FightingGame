using System.Collections.Generic;
using UnityEngine;
public class ObjectPoolingManager : MonoBehaviour
{
    [SerializeField] private List<ObjectPool> _objectPools;
    [SerializeField] private ObjectPool _jumpEffectPools;
    [SerializeField] private Dictionary<string, Queue<GameObject>> _objectPoolDictionary = new Dictionary<string, Queue<GameObject>>();
    private List<GameObject> _objects = new List<GameObject>();
    public static ObjectPoolingManager Instance { get; private set; }
    private List<GameObject> _jumpOneEffects = new List<GameObject>();
    private List<GameObject> _jumpTwoEffects = new List<GameObject>();


    void Awake()
    {
        CheckInstance();
    }

    public void PoolInitialize()
    {
        // foreach (ObjectPool objectPool in _objectPools)
        // {
        //     Queue<GameObject> objectPoolQueue = new Queue<GameObject>();
        //     for (int i = 0; i < objectPool.size; i++)
        //     {
        //         GameObject poolObject = Instantiate(objectPool.prefab, transform);
        //         poolObject.SetActive(false);
        //         objectPoolQueue.Enqueue(poolObject);
        //         _objects.Add(poolObject);
        //     }
        //     _objectPoolDictionary.Add(objectPool.prefab.name, objectPoolQueue);
        // }
        for (int i = 0; i < _jumpEffectPools.size; ++i)
        {
            GameObject effect = Instantiate(_jumpEffectPools.prefab, transform).gameObject;
            GameObject effect2 = Instantiate(_jumpEffectPools.prefab, transform).gameObject;
            effect.SetActive(false);
            _jumpOneEffects.Add(effect);
            _jumpTwoEffects.Add(effect2);
        }
    }

    private void CheckInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject[] GetList(int index, string name)
    {
        if (index == 0)
        {
            return _jumpOneEffects.ToArray();
        }
        else
        {
            return _jumpTwoEffects.ToArray();
        }
    }
    public DemonicsAnimator GetObjectAnimation(string name)
    {

        return _jumpOneEffects[0].GetComponent<DemonicsAnimator>();
    }

    public void DisableAllObjects()
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            if (_objects[i] != null)
            {
                _objects[i].SetActive(false);
            }
        }
    }
}

