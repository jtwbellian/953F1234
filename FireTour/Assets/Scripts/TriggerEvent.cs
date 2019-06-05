using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    private GameObject target;
    public string targetName;

    void OnTriggerStay(Collider Other)
    {
        if (Other.gameObject.tag == "Player")
        {
            if (transform.Find(targetName).gameObject != null)
            {
                target = transform.Find(targetName).gameObject;

                if (target.activeSelf == false)
                {
                    target.gameObject.SetActive(true);
                }
            }

        }
    }
    private void OnTriggerExit(Collider Other)
    {
        if (Other.gameObject.tag == "Player")
        {
            if (transform.Find(targetName).gameObject != null)
            {
                target = transform.Find(targetName).gameObject;

                if (target.activeSelf == true)
                {
                    target.gameObject.SetActive(false);
                }
            }
        }
    }
}