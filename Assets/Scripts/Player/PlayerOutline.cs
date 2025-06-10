using UnityEngine;
using System.Collections.Generic;

public class PlayerOutline : MonoBehaviour
{
    private List<Outline> outlines = new List<Outline>();
    private Camera mainCam;
    private float checkRadius = 0.3f;

    void Awake()
    {
        mainCam = Camera.main;

        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in childRenderers)
        {
            if (rend.gameObject.GetComponent<Outline>() != null)
                continue;

            Outline outline = rend.gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineWidth = 2f;
            outline.enabled = false;
            outlines.Add(outline);
        }
    }

    void Update()
    {
        Vector3 dirToCam = mainCam.transform.position - transform.position;
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        bool isObstructed = Physics.SphereCast(origin, checkRadius, dirToCam.normalized, out RaycastHit hit, dirToCam.magnitude);

        if (isObstructed && hit.transform != null && hit.transform.root != transform)
            EnableOutline(true);
        else
            EnableOutline(false);
    }

    void EnableOutline(bool enable)
    {
        foreach (var outline in outlines)
            outline.enabled = enable;
    }
}