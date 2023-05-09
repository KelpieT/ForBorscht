using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/DropChance")]
public class DropChance : ScriptableObject
{
    [SerializeField] private List<DropChancePair> dropChancePairs;

    public ItemID GetItemID()
    {
        float random = Random.value;
        float current = 0;
        foreach (var item in dropChancePairs)
        {
            if (current > random)
            {
                return item.itemID;
            }
            current += item.chance01;
        }
        return ItemID.None;
    }

    [System.Serializable]
    public class DropChancePair
    {
        public ItemID itemID;
        public float chance01;
    }
}