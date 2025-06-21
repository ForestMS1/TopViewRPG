using System;
using UnityEngine;
using System.Collections.Generic;

public class AttackableTracker : MonoBehaviour
{
    public List<string> targetTags;
    public GameObject trackPrefab;

    public List<GameObject> objectsInCollider = new List<GameObject>( );
    public GameObject currentTarget;
    public GameObject currentTrack;
    
    private void Update()
    {
        GameObject newClosest = GetClosestObject();

        if (newClosest != currentTarget)
        {
            currentTarget = newClosest;
            UpdateTrack();
        }
    }

    private void OnTriggerEnter( Collider other )
    {
        if(targetTags.Contains( other.tag ) && !objectsInCollider.Contains( other.gameObject ))
            objectsInCollider.Add( other.gameObject );
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInCollider.Remove(other.gameObject))
        {
            if (currentTarget == other.gameObject)
            {
                currentTarget = null;
                if (currentTrack != null)
                    currentTrack.SetActive(false);
            }
        }
    }


    private GameObject GetClosestObject( )
    {
        GameObject closest = null;
        float minDist = float.MaxValue;

        foreach (var obj in objectsInCollider)
        {
            if (obj == null) continue;

            float dist = Vector3.Distance(transform.position, obj.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = obj;
            }
        }

        return closest;
    }
    
    private void UpdateTrack()
    {
        if (currentTrack != null)
        {
            currentTrack.SetActive(false);
        }

        if (currentTarget == null) return;

        if (currentTrack == null)
        {
            currentTrack = Instantiate(
                trackPrefab,
                Vector3.zero,
                Quaternion.Euler(90f, 0f, 0f),
                currentTarget.transform
            );
        }

        currentTrack.transform.localPosition = new Vector3(0, 0.01f, 0);
        currentTrack.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        currentTrack.transform.SetParent(currentTarget.transform, false);
        currentTrack.SetActive(true);
    }
}
