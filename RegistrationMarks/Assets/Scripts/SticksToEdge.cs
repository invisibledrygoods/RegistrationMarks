using UnityEngine;
using System.Collections.Generic;
using System;

public class SticksToEdge : MonoBehaviour
{
    [Serializable]
    public enum Edge { Left, Right, Center };

    public Edge edge = Edge.Center;
    public float distanceFromCamera = 1.0f;

    Camera parentCamera;

    void Start()
    {
        Transform ancestor = transform.parent;

        while (ancestor.camera == null)
        {
            ancestor = ancestor.parent;
        }

        parentCamera = ancestor.camera;
    }

    void Update()
    {
        float x;

        if (edge == Edge.Left)
        {
            x = 0;
        }
        else if (edge == Edge.Right)
        {
            x = Screen.width;
        }
        else
        {
            x = Screen.width / 2;
        }

        // TODO: redo this logic using vectors

        Vector3 bottom = parentCamera.ScreenToWorldPoint(new Vector3(x, 0, distanceFromCamera));
        Vector3 top = parentCamera.ScreenToWorldPoint(new Vector3(x, Screen.height, distanceFromCamera));

        transform.localScale = Vector3.one * Vector3.Distance(bottom, top);
        transform.position = (bottom + top) / 2;
    }

    void OnDrawGizmos()
    {
        Transform ancestor = transform.parent;

        while (ancestor.camera == null)
        {
            ancestor = ancestor.parent;
        }

        Camera parentCamera = ancestor.camera;

        Vector3 tabStart;
        Vector3 tabEnd;

        if (edge == Edge.Left)
        {
            tabStart = Vector3.zero;
            tabEnd = parentCamera.transform.right;
        }
        else if (edge == Edge.Right)
        {
            tabStart = Vector3.zero;
            tabEnd = -parentCamera.transform.right;
        }
        else
        {
            tabStart = -parentCamera.transform.right;
            tabEnd = parentCamera.transform.right;
        }

        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(transform.position - parentCamera.transform.up * transform.localScale.y / 2, transform.position + parentCamera.transform.up * transform.localScale.y / 2);
        Gizmos.DrawLine(transform.position - parentCamera.transform.up * transform.localScale.y / 2 + tabStart, transform.position - parentCamera.transform.up * transform.localScale.y / 2 + tabEnd);
        Gizmos.DrawLine(transform.position + parentCamera.transform.up * transform.localScale.y / 2 + tabStart, transform.position + parentCamera.transform.up * transform.localScale.y / 2 + tabEnd);
        Gizmos.DrawLine(transform.position + tabStart, transform.position + tabEnd);
    }
}
