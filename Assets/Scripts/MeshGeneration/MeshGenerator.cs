using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    public Dictionary<Vector3,float> points = new Dictionary<Vector3, float>();
    public float threshold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        points.Clear();
        GeneratePoints();
    }

    public void GeneratePoints()
    {
        for (int x = 0; x < 20; x++)
        {
            for (int z = 0; z < 20; z++)
            {
                for (int y = 0; y < 20; y++)
                {
                    points.Add(new Vector3(x, y, z), Random.Range(0f, 2f));
                }
            }
        }

    }

    public void OnDrawGizmos()
    {
        if (points.Count == 0)
            GeneratePoints();


        foreach (var item in points)
        {
            //Draw
            if(item.Value >= 1-threshold)
            {
                Gizmos.color = new Color(1, 1, 1, item.Value);
                Gizmos.DrawCube(item.Key, Vector3.one/2f);
            }

        }
    }

    public void UpdatePoints()
    {

    }

}
