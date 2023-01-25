using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberhk : MonoBehaviour
{
    int[] numberslist = {1,4,34,56};
    
    // Start is called before the first frame update
    void Start()
    {
        checkeven(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void checkeven(int inp)
    {

        for (uint i = 0; i <= 3; i++)
        {
            if (numberslist[i] == inp)
            {
                Debug.Log("NUmberFound");
                break;
            }
            else 
            { 
                Debug.Log("NUmber not Found"); 
            }
        }

    }

}
