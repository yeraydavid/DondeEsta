using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchableElement : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void touched() {
        GetComponent<ElementManager>().hitted();
    }

	void Update()
    {   
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                touched();
            }
        }
    }

    void OnMouseUpAsButton() 
    {
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        RaycastHit hit;
         
         if( Physics.Raycast( ray, out hit, 100 ) )
         {
             touched();
         }
    }
}
