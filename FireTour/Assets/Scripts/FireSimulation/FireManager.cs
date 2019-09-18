using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FireManager : MonoBehaviour
{
    private Mesh mesh;
    public MeshFilter targetMesh;
    public GameObject targetNodeGroup;
    public List<FireProbe> probes;
    public WindHorn windHorn;
    private FXManager fx;
    public bool simulateFire = true;

    [SerializeField, Range(0.01f, 50f)]
    private float timeToDouble = 30f;

    private List<Color32> vertColorList;


    ////////////////////////////////////////////
    public GameObject testPoint = null;
    ///////////////////////////////////////////

    #region singleton

    public static FireManager instance = null;

    delegate void ProbeDelegate();
    ProbeDelegate probeUpdate;

    void Awake() 
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(instance);
                instance = this;
            }
        }
    }
    
    public static FireManager GetInstance()
    {
        return instance;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        fx = FXManager.GetInstance();
        mesh = targetMesh.sharedMesh;
        vertColorList = new List<Color32>();
        //prepare blank color list
        for (int i = 0; i < mesh.vertexCount; i++)
            vertColorList.Add(Color.white);
        Catalog();
        StartCoroutine("SpreadFire");
    }

    public void AddProbe(FireProbe p)
    {
        probeUpdate += p.Grow;
        probes.Add(p);
    }

    public void RemoveProbe(FireProbe p)
    {
        probeUpdate -= p.Grow;
        probes.Remove(p);
    }
     [ContextMenu("Put Out")]
    public void PutOut()
    {
        foreach( FireProbe f in probes)
        {
            f.TurnOff();
        }
    }

    IEnumerator SpreadFire()
    {
        while(simulateFire)
        {
            if (probeUpdate != null)
                probeUpdate.Invoke();
                
            yield return new WaitForSeconds(timeToDouble);
        }

        yield return null;
    }

    public void SetVertexColor(int index, Color color)
    {
        //mesh = targetMesh.sharedMesh;
        vertColorList[index] = color;
        //Debug.Log("Changed one vertex color " + index);
        mesh.SetColors(vertColorList);
        //mesh.colors[index] = color;
    }

    [ContextMenu("Collect All Probes")]

    void CollectProbes()
    {
        probes = targetNodeGroup.GetComponentsInChildren<FireProbe>().ToList();
        foreach (var probe in probes)
        {
            probe.Start();
        }
    }

    [ContextMenu("Force Verts to Burnt")]

    void ColorVert()
    {
    mesh = targetMesh.sharedMesh;
    List<Color32> newClr = new List<Color32>();
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                newClr.Add(Color.black);
            }
    mesh.SetColors(newClr);
    }


        [ContextMenu("Force Verts to Clean")]

    void BlankVert()
    {
    mesh = targetMesh.sharedMesh;
    Color[] colors = new Color[mesh.vertices.Length];
    List<Color32> newClr = new List<Color32>();
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                newClr.Add(Color.white);
            }
    mesh.SetColors(newClr);
    /* 
    for (int l = 0; l < mesh.vertices.Length; l++)
        {
        colors[l] = Color.red;
        }
    mesh.colors = colors;*/
    }

    [ContextMenu("Bake Vertex Catalog")]
    void Catalog()
    {
        mesh = targetMesh.sharedMesh;
        //Matrix4x4 localToWorld = transform.localToWorldMatrix;

        //Create empty array of proper size to manage mesh Vertex count

        Vector3[] vertPos = new Vector3[mesh.vertices.Length];

        //Find the worldspace equivalent of every vertex location. 
        //Then, add that location to our vertLocs array. 

        for(int i = 0; i < mesh.vertices.Length; ++i)
        {
            Vector3 world_v = mesh.vertices[i]; //localToWorld.MultiplyPoint3x4(mesh.vertices[i]);
            vertPos[i] = world_v + new Vector3(-2.69f, 0.0568691f, 20.52f); //+ targetMesh.transform.position;
            Debug.Log("Index: " + i.ToString() + "  Worldspace Location:" + vertPos[i].ToString());

            /*if (i < 2000)
            {            
                var obj = Instantiate(testPoint);
                obj.transform.position = vertPos[i];
                //obj.transform.SetParent(testPoint.transform);
            }*/
        }

            //Delegate vertex points to each Node
        foreach (var probe in probes)
        {
            //Debug.Log("Probe " + probe.ToString());
            int i = 0;
            var currentShellGroup = new List<int>();
            //Debug.Log("This is Vertpos Length: " + vertPos.Length.ToString());

            for(int k = 0; k < vertPos.Length; k++)
                {//Debug.Log("Checking vertex " + k.ToString());

                    if (Vector3.Distance(probe.transform.position, vertPos[k]) < probe.trigger.radius)
                    {
                        currentShellGroup.Add(k);
                        //Debug.Log("Probe " + probe.ToString() + " was given Vertex "  + k.ToString());
                    }
                }

            probe.VertexGroup[i] = currentShellGroup;
            Debug.Log("Probe " + probe.ToString() + "was given " + probe.VertexGroup[i].ToString());
        }
        
        }
 
}
