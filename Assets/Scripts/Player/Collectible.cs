using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private List<string> keys = new List<string>();

    public void AddKey(string key)
    {
        keys.Add(key);
    }

    public List<string> KeyList()
    {
        return keys;
    }

}
