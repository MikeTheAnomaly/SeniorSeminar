using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
public class ShaderSwap : MonoBehaviour
{
    public Material starting;
    public Material ending;
    bool cur = true;
    MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void Swap()
    {
        cur = !cur;
        if (cur)
        {
            mesh.material = starting;
        }
        else
        {
            mesh.material = ending;
        }
    }
}
