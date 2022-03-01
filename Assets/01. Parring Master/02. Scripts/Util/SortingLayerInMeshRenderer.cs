using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerInMeshRenderer : MonoBehaviour
{
    private MeshRenderer mesh;
    public string sortingLayerName = "Default";
    public int sortingOrder = 0;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        mesh.sortingLayerName = sortingLayerName;
        mesh.sortingOrder = sortingOrder;
    }
}
