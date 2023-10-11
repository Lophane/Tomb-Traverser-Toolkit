using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class BuildNavMesh : MonoBehaviour
{

    public NavMeshSurface surface;


    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(BakeNavMesh), 6f);
    }

    private void BakeNavMesh()
    {
        surface.BuildNavMesh();
    }

}
