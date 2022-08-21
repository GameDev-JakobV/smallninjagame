using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementCut : MonoBehaviour
{
    [SerializeField] Vector3 position;
    [SerializeField] GameObject Sphere;
    [SerializeField] Material RedMaterial;
    [SerializeField] Material TransparentMaterial;
    [SerializeField] int sizeOfSphere = 3;

    [SerializeField] int amountOfCuts = 4;

    //Study the judgement cut 
    //make a line 
    //TODO: animate line 
    //spawn sphere 
    //specify random point on spheres edge
    //make a line between two points
    //differentiate between every pair of lines
    //make them happen consecutively

    // Start is called before the first frame update
    void Start()
    {
        points = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnSphere();
        }
    }

    private void SpawnSphere()
    {
        Debug.Log("Sphere spawned");
        Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Sphere.transform.position = position;
        Sphere.transform.localScale = Sphere.transform.localScale * sizeOfSphere;
        Sphere.GetComponent<MeshRenderer>().material = TransparentMaterial;
        Sphere.AddComponent<Rigidbody>();
        Sphere.GetComponent<Rigidbody>().isKinematic = true;
        Sphere.GetComponent<SphereCollider>().isTrigger = true;

        SpawnPointsOnSphere();
    }

    private List<Vector3> points;
    private void SpawnPointsOnSphere()
    {
        //TODO: tror at de skal flyttes ind i den if statement men tester det imorgen
        for (int i = 0; i < amountOfCuts * 2; i++)
        {
            Vector3 tempPos = Sphere.transform.position + Random.onUnitSphere * (sizeOfSphere / 2);
            GameObject Temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            // er kun halv så lang pga radius er halvdelen af diameter
            if (amountOfCuts % 2 == 0)
            {
                LineRenderer line = Temp.AddComponent<LineRenderer>();
                line.positionCount = amountOfCuts * 2;
                line.SetPosition(i, tempPos);
            }


            Temp.transform.position = tempPos;
            Temp.transform.localScale = Temp.transform.localScale * 0.1f;
            Temp.GetComponent<MeshRenderer>().material = RedMaterial;
            //line.SetPosition(i, tempPos);
            //points.Add(Sphere.transform.position + Random.onUnitSphere * 1.5f);
        }

    }

    private void LineBetweenPoints()
    {

    }


    private void RemoveSphere()
    {

    }
}
