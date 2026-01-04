using UnityEngine;
using System.Collections.Generic;
using System;

public class GamePlayManager : Singleton<GamePlayManager>
{
    
    public List<GameObject> fruitList = new List<GameObject>();

    public Transform fruitSpawnPoint;

    private GameObject cloneObj;
    private Dictionary<string, int> fruitMergeLevelDict = new Dictionary<string, int>()
    {
        {"Cherry", 30},
        {"Blueberry", 30},
        {"Lemon", 30},
        {"Grape", 20},
        {"Apple", 10},
        {"Orange", 10},
        // {"Peach", 6},
        // {"Coconut", 7},
        // {"PineApple", 8},
        // {"DragonFruit", 9},
        // {"Melon", 10},
    };
    private void Awake()
    {
        SpawnFruit();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private string GetFruitByRate()
    {
            if (fruitMergeLevelDict == null || fruitMergeLevelDict.Count == 0) return string.Empty;

            int totalWeight = 0;
            foreach (var item in fruitMergeLevelDict)
            {
                if (item.Key == null || item.Value <= 0) continue;
                totalWeight += item.Value;
            }

            if (totalWeight == 0) return string.Empty;

            int randomValue = UnityEngine.Random.Range(0, totalWeight);
            int cumulative = 0;

            foreach (var item in fruitMergeLevelDict)
            {
                if (item.Key == null || item.Value <= 0) continue;
                cumulative += item.Value;
                if (randomValue < cumulative)
                {
                    return item.Key;
                }
            }
            return string.Empty;
    }

    private int GetFruitIndex()
    {
        string fruitName = GetFruitByRate();
        for (int i = 0; i < fruitList.Count; i++)
        {
            if (fruitList[i].name == fruitName)
            {
                return i;
                break;
            }
        }
        return 0;

    }
    public void SpawnFruit()
    {
        int fruitIndex = GetFruitIndex();
        cloneObj =Instantiate(fruitList[fruitIndex], fruitSpawnPoint.position, Quaternion.identity);
        cloneObj.name = fruitList[fruitIndex].name;
        cloneObj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb);
        rb.simulated = false;
        

    }

}
