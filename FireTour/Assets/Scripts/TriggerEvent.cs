using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public GameObject target;
    public bool invertActivation;
    public bool persistAfterDeparture;

    void OnTriggerEnter(Collider Other)
    {
        if (target !=null && Other.gameObject.transform.root.tag == "Player")
        {
                if (invertActivation)
                {
                    if (target.activeSelf)
                        target.gameObject.SetActive(false);
                }  
                else
                {
                    if (!target.activeSelf)
                        target.gameObject.SetActive(true);
                }   
        }
    }
    private void OnTriggerExit(Collider Other)
        {
        if (target !=null && Other.gameObject.transform.root.tag == "Player")
        {
                if (invertActivation)
                {
                    if (!target.activeSelf)
                        target.gameObject.SetActive(true);
                }  
                else
                {
                    if (target.activeSelf)
                        target.gameObject.SetActive(false);
                }   
        }
    }
}