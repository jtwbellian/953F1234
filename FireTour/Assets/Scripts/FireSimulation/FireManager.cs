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

            mesh.SetColors(vertColorList);

            yield return new WaitForSeconds(timeToDouble);
        }

        yield return null;
    }

    public void SetVertexColor(int index, Color color)
    {
        vertColorList[index] = color;
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
        Debug.Log("Attempting to bake...");
        mesh = targetMesh.sharedMesh;
        //Create empty array of proper size to manage mesh Vertex count
        Vector3[] vertPos = new Vector3[mesh.vertices.Length];

        //Find the worldspace equivalent of every vertex location. 
        //Then, add that location to our vertLocs array. 
        for(int i = 0; i < mesh.vertices.Length; ++i)
        {
            Vector3 world_v = mesh.vertices[i]; //localToWorld.MultiplyPoint3x4(mesh.vertices[i]);
            vertPos[i] = world_v + new Vector3(-2.69f, 0.0568691f, 20.52f); //+ targetMesh.transform.position;
            //Debug.Log("Index: " + i.ToString() + "  Worldspace Location:" + vertPos[i].ToString());
        }

            //Delegate vertex points to each Node
        foreach (var probe in probes)
        {
            var currentShellGroup = new List<int>();
            for(int i = 0; i < FireProbe.shellCount; i++) //For all shells
            {
                for(int j = 0; j < vertPos.Length; j++) //For one specific shell
                {

                    if (Vector3.Distance(probe.transform.position, vertPos[j]) < probe.trigger.radius)
                    {
                        currentShellGroup.Add(j);
                    }
                }

                if (i > 0)
                for(int j = 0; j < currentShellGroup.Count-1; j++) //Clean out duplicates
                {
                    if (j > 0)
                    if (currentShellGroup[j] == currentShellGroup[j-1])
                    {
                       currentShellGroup.Remove(j);
                    }
                }
                
                probe.VertexGroup[i] = currentShellGroup;
            }

        }
        Debug.Log("Vertex Catalog successfully baked.");
        }
 
}
