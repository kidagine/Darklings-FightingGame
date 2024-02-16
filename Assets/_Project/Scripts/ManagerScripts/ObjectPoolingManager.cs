using System.Collections.Generic;
using UnityEngine;
public class ObjectPoolingManager : MonoBehaviour
{
    [SerializeField] private Transform _playerOnePool = default;
    [SerializeField] private Transform _playerTwoPool = default;
    [SerializeField] private Transform _playerOneProjectilePool = default;
    [SerializeField] private Transform _playerTwoProjectilePool = default;
    [SerializeField] private Transform _playerOneParticlesPool = default;
    [SerializeField] private Transform _playerTwoParticlesPool = default;
    private List<GameObject> _objects = new List<GameObject>();
    public static ObjectPoolingManager Instance { get; private set; }
    private List<GameObject> _jumpOneEffects = new List<GameObject>();
    private List<GameObject> _jumpTwoEffects = new List<GameObject>();
    private List<ObjectPoolGroup> _objectsPoolOne = new List<ObjectPoolGroup>();
    private List<ObjectPoolGroup> _objectsPoolTwo = new List<ObjectPoolGroup>();
    private List<ObjectPoolGroup> _objectsParticlesPoolOne = new List<ObjectPoolGroup>();
    private List<ObjectPoolGroup> _objectsParticlesPoolTwo = new List<ObjectPoolGroup>();
    private List<ObjectPoolGroup> _objectsProjectilePoolOne = new List<ObjectPoolGroup>();
    private List<ObjectPoolGroup> _objectsProjectilePoolTwo = new List<ObjectPoolGroup>();
    private List<ObjectPoolGroup> _objectsAssistsPoolOne = new List<ObjectPoolGroup>();
    private List<ObjectPoolGroup> _objectsAssistsPoolTwo = new List<ObjectPoolGroup>();
    public static bool HasPooled;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        CheckInstance();
    }

    public void PoolInitialize(EffectsLibrarySO effectsLibraryOne, EffectsLibrarySO effectsLibraryTwo)
    {
        if (!HasPooled)
        {
            for (int i = 0; i < effectsLibraryOne._objectPools.Count; i++)
            {
                _objectsPoolOne.Add(new ObjectPoolGroup() { groupName = effectsLibraryOne._objectPools[i].groupName, objects = new List<GameObject>() });
                for (int j = 0; j < effectsLibraryOne._objectPools[i].size; ++j)
                {
                    GameObject effect = Instantiate(effectsLibraryOne._objectPools[i].prefab, _playerOnePool).gameObject;
                    effect.gameObject.SetActive(false);
                    _objectsPoolOne[i].objects.Add(effect);
                }
            }
            for (int i = 0; i < effectsLibraryTwo._objectPools.Count; i++)
            {
                _objectsPoolTwo.Add(new ObjectPoolGroup() { groupName = effectsLibraryTwo._objectPools[i].groupName, objects = new List<GameObject>() });
                for (int j = 0; j < effectsLibraryTwo._objectPools[i].size; ++j)
                {
                    GameObject effect = Instantiate(effectsLibraryTwo._objectPools[i].prefab, _playerTwoPool).gameObject;
                    effect.gameObject.SetActive(false);
                    _objectsPoolTwo[i].objects.Add(effect);
                }
            }
        }
    }

    public void PoolParticlesInitialize(EffectsLibrarySO particlesLibraryOne, EffectsLibrarySO particlesLibraryTwo)
    {
        if (!HasPooled)
        {
            for (int i = 0; i < particlesLibraryOne._objectPools.Count; i++)
            {
                _objectsParticlesPoolOne.Add(new ObjectPoolGroup() { groupName = particlesLibraryOne._objectPools[i].groupName, objects = new List<GameObject>() });
                for (int j = 0; j < particlesLibraryOne._objectPools[i].size; ++j)
                {
                    GameObject effect = Instantiate(particlesLibraryOne._objectPools[i].prefab, _playerOneParticlesPool).gameObject;
                    effect.gameObject.SetActive(false);
                    _objectsParticlesPoolOne[i].objects.Add(effect);
                }
            }
            for (int i = 0; i < particlesLibraryTwo._objectPools.Count; i++)
            {
                _objectsParticlesPoolTwo.Add(new ObjectPoolGroup() { groupName = particlesLibraryTwo._objectPools[i].groupName, objects = new List<GameObject>() });
                for (int j = 0; j < particlesLibraryTwo._objectPools[i].size; ++j)
                {
                    GameObject effect = Instantiate(particlesLibraryTwo._objectPools[i].prefab, _playerTwoParticlesPool).gameObject;
                    effect.gameObject.SetActive(false);
                    _objectsParticlesPoolTwo[i].objects.Add(effect);
                }
            }
        }
    }

    public void PoolProjectileInitialize(EffectsLibrarySO effectsLibraryOne, EffectsLibrarySO effectsLibraryTwo)
    {
        if (!HasPooled)
        {
            for (int i = 0; i < effectsLibraryOne._objectPools.Count; i++)
            {
                _objectsProjectilePoolOne.Add(new ObjectPoolGroup() { groupName = effectsLibraryOne._objectPools[i].groupName, objects = new List<GameObject>() });
                for (int j = 0; j < effectsLibraryOne._objectPools[i].size; ++j)
                {
                    GameObject effect = Instantiate(effectsLibraryOne._objectPools[i].prefab, _playerOneProjectilePool).gameObject;
                    effect.gameObject.SetActive(false);
                    _objectsProjectilePoolOne[i].objects.Add(effect);
                }
            }
            for (int i = 0; i < effectsLibraryTwo._objectPools.Count; i++)
            {
                _objectsProjectilePoolTwo.Add(new ObjectPoolGroup() { groupName = effectsLibraryTwo._objectPools[i].groupName, objects = new List<GameObject>() });
                for (int j = 0; j < effectsLibraryTwo._objectPools[i].size; ++j)
                {
                    GameObject effect = Instantiate(effectsLibraryTwo._objectPools[i].prefab, _playerTwoProjectilePool).gameObject;
                    effect.gameObject.SetActive(false);
                    _objectsProjectilePoolTwo[i].objects.Add(effect);
                }
            }
        }
    }
    public void PoolAssistInitialize(ObjectPool assistOne, ObjectPool assistTwo)
    {
        if (!HasPooled)
        {

            _objectsAssistsPoolOne.Add(new ObjectPoolGroup() { groupName = assistOne.groupName, objects = new List<GameObject>() });
            GameObject assistObjectOne = Instantiate(assistOne.prefab, _playerOneProjectilePool).gameObject;
            assistObjectOne.gameObject.SetActive(false);
            _objectsAssistsPoolOne[0].objects.Add(assistObjectOne);
            _objectsAssistsPoolTwo.Add(new ObjectPoolGroup() { groupName = assistTwo.groupName, objects = new List<GameObject>() });
            GameObject assistObjectTwo = Instantiate(assistTwo.prefab, _playerTwoProjectilePool).gameObject;
            assistObjectTwo.gameObject.SetActive(false);
            _objectsAssistsPoolTwo[0].objects.Add(assistObjectTwo);
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
    public GameObject[] GetParticlePool(int index, string name)
    {
        if (index == 0)
        {
            for (int i = 0; i < _objectsParticlesPoolOne.Count; i++)
            {
                if (_objectsParticlesPoolOne[i].groupName == name)
                {
                    return _objectsParticlesPoolOne[i].objects.ToArray();
                }
            }
        }
        else
        {
            for (int i = 0; i < _objectsParticlesPoolTwo.Count; i++)
            {
                if (_objectsParticlesPoolTwo[i].groupName == name)
                {
                    return _objectsParticlesPoolTwo[i].objects.ToArray();
                }
            }
        }
        return null;
    }
    public GameObject[] GetProjectilePool(int index, string name)
    {
        if (index == 0)
        {
            for (int i = 0; i < _objectsProjectilePoolOne.Count; i++)
            {
                if (_objectsProjectilePoolOne[i].groupName == name)
                {
                    return _objectsProjectilePoolOne[i].objects.ToArray();
                }
            }
        }
        else
        {
            for (int i = 0; i < _objectsProjectilePoolTwo.Count; i++)
            {
                if (_objectsProjectilePoolTwo[i].groupName == name)
                {
                    return _objectsProjectilePoolTwo[i].objects.ToArray();
                }
            }
        }
        return null;
    }
    public GameObject GetAssistPool(int index, string name)
    {
        if (index == 0)
        {
            for (int i = 0; i < _objectsAssistsPoolOne.Count; i++)
            {
                if (_objectsAssistsPoolOne[i].groupName == name)
                {
                    return _objectsAssistsPoolOne[i].objects[0];
                }
            }
        }
        else
        {

            for (int i = 0; i < _objectsAssistsPoolTwo.Count; i++)
            {
                if (_objectsAssistsPoolTwo[i].groupName == name)
                {
                    return _objectsAssistsPoolTwo[i].objects[0];
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
    public DemonicsAnimator GetObjectPoolAnimation(int index, string name)
    {
        if (index == 0)
        {
            for (int i = 0; i < _objectsProjectilePoolOne.Count; i++)
            {
                if (_objectsProjectilePoolOne[i].groupName == name)
                {
                    return _objectsProjectilePoolOne[i].objects[0].GetComponent<DemonicsAnimator>();
                }
            }
        }
        else
        {
            for (int i = 0; i < _objectsProjectilePoolTwo.Count; i++)
            {
                if (_objectsProjectilePoolTwo[i].groupName == name)
                {
                    return _objectsProjectilePoolTwo[i].objects[0].GetComponent<DemonicsAnimator>();
                }
            }
        }
        return null;
    }
    public DemonicsAnimator GetAssistPoolAnimation(int index, string name)
    {
        if (index == 0)
        {
            for (int i = 0; i < _objectsAssistsPoolOne.Count; i++)
            {
                if (_objectsAssistsPoolOne[i].groupName == name)
                {
                    return _objectsAssistsPoolOne[i].objects[0].GetComponent<DemonicsAnimator>();
                }
            }
        }
        else
        {
            for (int i = 0; i < _objectsAssistsPoolTwo.Count; i++)
            {
                if (_objectsAssistsPoolTwo[i].groupName == name)
                {
                    return _objectsAssistsPoolTwo[i].objects[0].GetComponent<DemonicsAnimator>();
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

    public void DestroyAllObjects()
    {
        foreach (Transform child in _playerOnePool)
            Destroy(child.gameObject);
        foreach (Transform child in _playerTwoPool)
            Destroy(child.gameObject);
        _objectsPoolOne.Clear();
        _objectsProjectilePoolOne.Clear();
        _objectsParticlesPoolOne.Clear();
        _objectsAssistsPoolOne.Clear();
        _objectsPoolTwo.Clear();
        _objectsProjectilePoolTwo.Clear();
        _objectsParticlesPoolTwo.Clear();
        _objectsAssistsPoolTwo.Clear();
        HasPooled = false;
    }
}

public struct ObjectPoolGroup
{
    public string groupName;
    public List<GameObject> objects;
}

