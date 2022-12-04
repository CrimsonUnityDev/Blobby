using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysUnit : MonoBehaviour
{
    
    private void Update()
    {
        transform.localScale = new Vector3(1f/transform.parent.localScale.x,
       1f/ transform.parent.localScale.y,
        1f/transform.parent.localScale.z);
    }
}
