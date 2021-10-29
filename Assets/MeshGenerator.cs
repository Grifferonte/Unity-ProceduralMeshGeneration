using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    MeshFilter m_mf;
    private void Awake()
    {
        m_mf = GetComponent<MeshFilter>();
        //m_mf.sharedMesh = CreateQuadXZ(new Vector3(4,0,2));
        //m_mf.sharedMesh = CreateStripXZ(new Vector3(4,0,2),100);
        m_mf.sharedMesh = CreatePlaneXZ(new Vector3(4,0,2),100,100);
        gameObject.AddComponent<MeshCollider>();
    }

    Mesh CreateTriangle()
    {
        Mesh mesh = new Mesh();
        mesh.name = "triangle";
        Vector3[] vertices = new Vector3[3];
        int[] triangles = new int[3];

        vertices[0] = Vector3.right;
        vertices[1] = Vector3.up;
        vertices[2] = Vector3.forward;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();

        return mesh;
    }
    
    Mesh CreateQuadXZ(Vector3 size)
    {
        Vector3 halfsize = size * 0.5f;
        Mesh mesh = new Mesh();
        mesh.name = "Quad";
        Vector3[] vertices = new Vector3[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(-halfsize.x,0,-halfsize.z);
        vertices[1] = new Vector3(-halfsize.x, 0, halfsize.z);
        vertices[2] = new Vector3(halfsize.x, 0, halfsize.z);
        vertices[3] = new Vector3(halfsize.x, 0, -halfsize.z);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();

        return mesh;
    }

    Mesh CreateStripXZ(Vector3 size, int nbSubdivisions)
    {
        Vector3 halfsize = size * 0.5f;
        Mesh mesh = new Mesh();
        mesh.name = "Strip";
        Vector3[] vertices = new Vector3[(nbSubdivisions + 1) * 2];
        int[] triangles = new int[2 * nbSubdivisions * 3];


        //VERTICE
        for (int i = 0; i < nbSubdivisions + 1 ; i++) {
            float k = (float)i / nbSubdivisions;
            float x = Mathf.Lerp(-halfsize.x, halfsize.x, k);
            float y = .125f*Mathf.Sin(k*Mathf.PI*2*2);
            vertices[i] = new Vector3(x, y ,-halfsize.z); // vertice du bas
            vertices[i + nbSubdivisions + 1] = new Vector3(x, y ,halfsize.z); // vertice du haut
        }

        int index = 0;
        //TRIANGLES
        for (int i = 0; i < nbSubdivisions ; i++) {
            triangles[index++] = i;
            triangles[index++] = i+nbSubdivisions+1;
            triangles[index++] = i+nbSubdivisions+2;
            
            triangles[index++] = i;
            triangles[index++] = i+nbSubdivisions+2;
            triangles[index++] = i+1;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();

        return mesh;
    }

        Mesh CreatePlaneXZ(Vector3 size, int Nbsubx,int nbsubz)
    {
        Vector3 halfsize = size * 0.5f;
        Mesh mesh = new Mesh();
        mesh.name = "Plane";
        Vector3[] vertices = new Vector3[(Nbsubx + 1)*(nbsubz+1)];
        int[] triangles = new int[2 * 3 * Nbsubx*nbsubz];

        //vertices
        int index = 0;
        for (int i = 0; i < Nbsubx + 1; i++)
        {
            float kx = (float)i / Nbsubx;
            float x = Mathf.Lerp(-halfsize.x, halfsize.x, kx);
            for (int j = 0; j < nbsubz+1; j++)
            {
                float kz = (float)j / nbsubz;
                float z= Mathf.Lerp(-halfsize.z, halfsize.z, kz);
                
                vertices[index++] = new Vector3(x, 0, z);
            }
        }
        //triangles
        index = 0;
        for (int i = 0; i < Nbsubx; i++)
        {
            int indexoffset = i * (nbsubz+1);
            for(int j = 0; j < nbsubz; j++) { 

            triangles[index++] = indexoffset+j;
            triangles[index++] = indexoffset+j+1;
            triangles[index++] = indexoffset+j+1+nbsubz+1;
            triangles[index++] = indexoffset+j;
            triangles[index++] = indexoffset + j + 1 + nbsubz + 1;
            triangles[index++] = indexoffset+j+nbsubz+1;
           
        }}



        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }
}