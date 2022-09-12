using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
public class ShaderSwap : MonoBehaviour
{
    private Material[] starting;
    public Material ending;
    public Material wireframe;
    bool cur = true;
    MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        starting = mesh.materials.Clone() as Material[];
    }

    public void Swap()
    {
        cur = !cur;
        if (cur)
        {
            mesh.materials = starting;
        }
        else
        {
            Material[] mats = new Material[mesh.materials.Length];
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = ending;
            }
            mesh.materials = mats;

        }
    }

    public void SwapToWire()
    {
        SwapMat(wireframe);
    }

    public void SwapToEnding()
    {
        SwapMat(ending);
    }

    public void SwapToStart()
    {
        SwapMat(starting[0]);
    }

    private void SwapMat(Material m)
    {
        Material[] mats = new Material[mesh.materials.Length];
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i] = m;
        }
        mesh.materials = mats;
    }
}
