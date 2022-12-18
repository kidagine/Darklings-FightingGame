using System.Collections.Generic;
using UnityEngine;
public class ObjectPoolingManager : MonoBehaviour
{
    [SerializeField] private Transform _playerOnePool = default;
    [SerializeField] private Transform _playerTwoPool = default;
    [SerializeField] private List<ObjectPool> _objectPools;
    private List<GameObject> _objects = new List<GameObject>();
    public static ObjectPoolingManager Instance { get; private set; }
    private List<GameObject> _jumpOneEffects = new List<GameObject>();
    private List<GameObject> _jumpTwoEffects = new List<GameObject>();
    private List<ObjectPoolGroup> _objectsPoolOne = new List<ObjectPoolGroup>();
    private List<ObjectPoolGroup> _objectsPoolTwo = new List<ObjectPoolGroup>();
    private bool hasPooled;

    void Awake()
    {
        CheckInstance();
    }

    public void PoolInitialize(EffectsLibrarySO effectsLibraryOne, EffectsLibrarySO effectsLibraryTwo)
    {
        if (!hasPooled)
        {
            for (int i = 0; i < effectsLibraryOne._objectPools.Count; i++)
            {
                _objectsPoolOne.Add(new ObjectPoolGroup() { groupName = effectsLibraryOne._objectPools[i].groupName, objects = new List<GameObject>() });
                for (int j = 0; j < effectsLibraryOne._objectPools[i].size; ++j)
                {
                    GameObject effect = Instantiate(effectsLibraryOne._objectPools[i].prefab, _playerOnePool).gameObject;
                    _objectsPoolOne[i].objects.Add(effect);
                }
            }
            for (int i = 0; i < effectsLibraryTwo._objectPools.Count; i++)
            {
                _objectsPoolTwo.Add(new ObjectPoolGroup() { groupName = effectsLibraryTwo._objectPools[i].groupName, objects = new List<GameObject>() });
                for (int j = 0; j < effectsLibraryTwo._objectPools[i].size; ++j)
                {
                    GameObject effect = Instantiate(effectsLibraryTwo._objectPools[i].prefab, _playerTwoPool).gameObject;
                    _objectsPoolTwo[i].objects.Add(effect);
                }
            }
            hasPooled = true;
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

    public GameObject[] GetPool(int index, string name)
    {
        if (index == 0)
        {
            for (int i = 0; i < _objectsPoolOne.Count; i++)
            {
                if (_objectsPoolOne[i].groupName == name)
                {
                    return _objectsPoolOne[i].objects.ToArray();
                }
            }
        }
        else
        {
            for (int i = 0; i < _objectsPoolTwo.Count; i++)
            {
                if (_objectsPoolTwo[i].groupName == name)
                {
                    return _objectsPoolTwo[i].objects.ToArray();
                }
            }
        }
        return null;
    }
    public DemonicsAnimator GetObjectAnimation(int index, string name)
    {
        if (index == 0)
        {
            for (int i = 0; i < _objectsPoolOne.Count; i++)
            {
                if (_objectsPoolOne[i].groupName == name)
                {
                    return _objectsPoolOne[i].objects[0].GetComponent<DemonicsAnimator>();
                }
            }
        }
        else
        {
            for (int i = 0; i < _objectsPoolTwo.Count; i++)
            {
                if (_objectsPoolTwo[i].groupName == name)
                {
                    return _objectsPoolTwo[i].objects[0].GetComponent<DemonicsAnimator>();
                }
            }
        }
        return null;
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

public struct ObjectPoolGroup
{
    public string groupName;
    public List<GameObject> objects;
}

