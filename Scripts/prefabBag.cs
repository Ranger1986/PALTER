using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class prefabBag : ScriptableObject
{
    [SerializeField] List<GameObject> prefabs;
    [SerializeField] List<float> chances;

    bool isValid(){
        return prefabs.Count==chances.Count;
    }
    float getTotalChance(){
        float res=0;
        foreach (float chance in chances)
        {
            res+=chance;
        }
        return res;
    }
    public GameObject getPrefab(float givenChance){
        for (int i = 0; i < prefabs.Count; i++)
        {
            if (chances[i]>givenChance)
            {
                return prefabs[i];
            }
            else
            {
                givenChance-=chances[i];
            }
        }
        return null;
    }
    public GameObject getRandomPrefab(){
        return getPrefab(Random.Range(0, getTotalChance()));
    }
}
