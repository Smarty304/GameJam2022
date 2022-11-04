using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public GameObject YellowBottlePrefab;

    [SerializeField]
    public GameObject BlueBottleObject;

    [SerializeField]
    public GameObject RedBottleObject;

    public int NumYellows = 0;
    public int NumBlues = 0;
    public int NumReds = 0;
}
