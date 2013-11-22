using UnityEngine;
using System.Collections.Generic;
using System;

public class SticksToEdge : MonoBehaviour
{
    [Serializable]
    public enum Edge { Left, Right, Center };

    public Edge edge = Edge.Center;

    Camera parentCamera;

    void Awake()
    {
    }

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

        Vector3 bottom = parentCamera.ScreenToWorldPoint(new Vector3(x, 0, Mathf.Abs(transform.position.z - parentCamera.transform.position.z)));
        Vector3 top = parentCamera.ScreenToWorldPoint(new Vector3(x, Screen.height, Mathf.Abs(transform.position.z - parentCamera.transform.position.z)));

        transform.localScale = Vector3.one * Mathf.Abs(bottom.y - top.y);
        transform.position = (bottom + top) / 2;
    }

    void OnDrawGizmos()
    {
        Vector3 tabStart;
        Vector3 tabEnd;

        if (edge == Edge.Left)
        {
            tabStart = Vector3.zero;
            tabEnd = Vector3.right;
        }
        else if (edge == Edge.Right)
        {
            tabStart = Vector3.zero;
            tabEnd = Vector3.left;
        }
        else
        {
            tabStart = Vector3.left;
            tabEnd = Vector3.right;
        }

        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(transform.position - Vector3.up * transform.localScale.y / 2, transform.position + Vector3.up * transform.localScale.y / 2);
        Gizmos.DrawLine(transform.position - Vector3.up * transform.localScale.y / 2 + tabStart, transform.position - Vector3.up * transform.localScale.y / 2 + tabEnd);
        Gizmos.DrawLine(transform.position + Vector3.up * transform.localScale.y / 2 + tabStart, transform.position + Vector3.up * transform.localScale.y / 2 + tabEnd);
        Gizmos.DrawLine(transform.position + tabStart, transform.position + tabEnd);
    }
}
