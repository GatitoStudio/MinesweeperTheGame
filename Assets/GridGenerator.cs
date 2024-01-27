using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i < DataGame.w; ++i)
        {
            for(int j = 0; j < DataGame.h; ++j)
            {
                Instantiate(prefab, new Vector3(i, j), Quaternion.identity);
            }
        }
    }
   

}
