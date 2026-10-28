using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public enum EntityType
    {
        Player,
        Platform,
        Spike,
        Portal,
        None
    }
    
    [Serializable]
    public class EntityDictEntry
    {
        public EntityType EntityType;
        public GameObject GameObject;
        
        public EntityDictEntry(EntityType entityType, GameObject gameObject)
        {
            EntityType = entityType;
            GameObject = gameObject;
        }
    }
    
    [SerializeField] private List<EntityDictEntry> _entityList = new List<EntityDictEntry>();
    private Dictionary<EntityType, GameObject> _entityDict = new Dictionary<EntityType, GameObject>();
    
    [SerializeField] private List<MapData> _mapDatas = new List<MapData>();

    private int _curIdx = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(EntityDictEntry entityDictEntry in _entityList)
        {
            _entityDict[entityDictEntry.EntityType] = entityDictEntry.GameObject;
        }

        LoadMap(0);
        _curIdx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public event Action<string> OnMapLoaded;
    private List<GameObject> _spawnedObjects = new List<GameObject>();

    private void LoadMap(int idx)
    {
        int height = _mapDatas[idx].mapDataRows.Count;
        for (int i = 0; i < height; i++)
        {
            int width = _mapDatas[idx].mapDataRows[i].entityTypes.Count;
            for (int j = 0; j < width; j++)
            {
                Vector3 coord = IndexToCoord(i, j, width, height);
                EntityType entityType = _mapDatas[idx].mapDataRows[i].entityTypes[j];
                if (entityType != EntityType.None)
                {
                    _spawnedObjects.Add(Instantiate(_entityDict[entityType], coord, Quaternion.identity));
                }
            }
        }
        OnMapLoaded?.Invoke(_mapDatas[idx].mapName);
    }

    private Vector3 IndexToCoord(int rowIdx, int colIdx, int width, int height)
    {
        return new Vector3(colIdx - width / 2.0f, height / 2.0f - rowIdx, 0);
    }

    private void DestroyLoadedMap()
    {
        foreach (GameObject go in _spawnedObjects)
        {
            Destroy(go);
        }
        _spawnedObjects.Clear();
    }

    public void ResetMap()
    {
        DestroyLoadedMap();
        LoadMap(_curIdx);
    }
    
    public void ToNextMap()
    {
        DestroyLoadedMap();
        _curIdx = (_curIdx + 1) % _mapDatas.Count;
        LoadMap(_curIdx);
    }
}