using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickables : MonoBehaviour
{
    public GameObject prefab;
    public void Place()
    {
        prefab.SetActive(true);
    }
}
