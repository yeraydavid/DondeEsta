using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveIt() {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
