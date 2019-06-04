using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralMesh : MonoBehaviour
{
    public int length;
    public int depth;
    public int height;
    public int thickness;

    private float meshLength;
    private float meshDepth;
    private float meshHeight;
    private float meshThickness;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    List<Vector3> meshAll = new List<Vector3> { };

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        meshLength = length / 1000f;
        meshDepth = depth / 1000f;
        meshHeight = height / 1000f;
        meshThickness = thickness / 1000f;
        MakeMeshData();
        CreateMesh();
    }

    void MakeMeshData()
    {
        List<Vector3> meshLHEnd = new List<Vector3> { new Vector3(0, 0, 0), new Vector3(0, meshHeight, 0), new Vector3(0, 0, -meshDepth), new Vector3(0, meshHeight, -meshDepth),
                                    new Vector3(meshThickness, 0, -meshDepth), new Vector3(meshThickness, meshHeight, -meshDepth),
                                    new Vector3(meshThickness, meshHeight, 0), new Vector3(meshThickness, 0, 0) };
        float offsetRHEnd = meshLength - meshThickness;
        List<Vector3> meshRHEnd = new List<Vector3> { new Vector3(offsetRHEnd, 0, 0), new Vector3(offsetRHEnd, meshHeight, 0), new Vector3(offsetRHEnd, 0, -meshDepth), new Vector3(offsetRHEnd, meshHeight, -meshDepth),
                                    new Vector3(meshThickness + offsetRHEnd, 0, -meshDepth), new Vector3(meshThickness + offsetRHEnd, meshHeight, -meshDepth),
                                    new Vector3(meshThickness + offsetRHEnd, meshHeight, 0), new Vector3(meshThickness + offsetRHEnd, 0, 0)};
        Debug.Log(meshLHEnd);
        Debug.Log(meshRHEnd);
        meshAll.AddRange(meshLHEnd);
        meshAll.AddRange(meshRHEnd);
        vertices = meshAll.ToArray();
        triangles = new int[] { 0, 1, 2, 2, 1, 3, 2, 3, 4, 4, 3, 5, 3, 1, 5, 5, 1, 6, 6, 7, 4, 4, 5, 6, 2, 4, 0, 0, 4, 7,
                                0+8, 1+8, 2+8, 2+8, 1+8, 3+8, 2+8, 3+8, 4+8, 4+8, 3+8, 5+8, 3+8, 1+8, 5+8, 5+8, 1+8, 6+8, 6+8, 7+8, 4+8, 4+8, 5+8, 6+8, 2+8, 4+8, 0+8, 0+8, 4+8, 7+8 };
    }

    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
