using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour
{
    public GameObject l, i, v, e, border, pinkLight;
    public GameObject losePanel;
    [HideInInspector]
    public int numHits;
    public bool won;


    void Start()
    {
        numHits = 0;
    }

    
    void Update()
    {
        if (numHits == 1) l.SetActive(true);
        else if (numHits == 2) i.SetActive(true);
        else if (numHits == 3) v.SetActive(true);
        else if (numHits >= 4 && won == false)
        {
            e.SetActive(true);
            border.SetActive(true);
            pinkLight.SetActive(true);
            losePanel.SetActive(true);
        }        
    }


}
