using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public Transform target;

    public GameObject trailPrefab;

    private GameObject[] instantiated;

    private GameObject current;

    public float leashRange = 1f;
    public bool doingTrail = false;

    [ContextMenu("Start")]
    public void StartTrail()
    {
        NewCurrent();
        doingTrail=true;
    }

    private void NewCurrent()
    {
        current = Instantiate(trailPrefab,new Vector3(target.position.x, 0f, target.position.z), target.rotation );
        current.transform.SetParent(transform, true);
    }

    [ContextMenu("End")]
    public void EndTrail()
    {
        doingTrail=false; 
    }


    private void Update()
    {
        if (doingTrail)
        {
            if (Vector3.Distance(current.transform.position, target.position) > leashRange)
            {
                NewCurrent();
            }
            Vector3 trgNoY = target.position;
            trgNoY.y=0f;
            Vector3 crntNoY = current.transform.position;
            crntNoY.y=0f;


            Vector3 displacement = (trgNoY - crntNoY);
            if (!Mathf.Approximately(displacement.normalized.magnitude, 0f))
            {
                current.transform.forward = new Vector3(displacement.normalized.x, 0f, displacement.normalized.z);
                current.transform.GetChild(0).localScale = new Vector3(current.transform.GetChild(0).localScale.x, current.transform.GetChild(0).localScale.y,(displacement.magnitude/2f)+1f);
                current.transform.GetChild(0).localPosition = Vector3.forward*(displacement.magnitude/2f);

            }
        }
    }
}
