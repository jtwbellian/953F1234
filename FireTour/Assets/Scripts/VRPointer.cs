using UnityEngine;
using System.Collections;

public class VRPointer : MonoBehaviour {

    private GameObject hand;
    private float maxDist = 100f;
    private VRButton lastButton;

    public GameObject primaryHand;
    public GameObject secondaryHand;
    public GameObject cursor;

    public bool canTeleport;

    public GameObject teleportField;
    public GameObject laser;
    public Transform playerTransform;

    private CharacterController character;

    private Transform headTransform;

    [Range(0.001f, 3f)]
    public float selectTime = 1f;
    private UICircleFill UICircle = null;
    [ReadOnly]
    public float wait = 0f;

    private Vector3 smallScale = new Vector3(0.125f, 0.125f, 0.125f);
    private Vector3 largeScale = new Vector3(0.5f, 0.5f, 0.5f);
    
    // Use this for initialization
    void Start () 
    {
        cursor = Instantiate(cursor);
        UICircle = cursor.GetComponentInChildren<UICircleFill>();

        if (canTeleport)
        {
            teleportField.transform.SetParent(cursor.transform);
            teleportField.transform.localPosition = Vector3.zero;
            teleportField.transform.localRotation = Quaternion.identity;
            // Set small cursor because we are in an open environment with smaller UIs
            cursor.transform.localScale = smallScale;
        }

        laser.SetActive(false);
        hand = primaryHand;
        character = playerTransform.GetComponent<CharacterController>();
        headTransform = Camera.main.transform;
    }
    
    // Update is called once per frame
    void Update () 
    {
        CheckControllerInput();
        
        RaycastHit hit;
        var layerMask = LayerMask.GetMask("UI", "Surface");

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(hand.transform.position, hand.transform.TransformDirection(Vector3.forward), out hit, maxDist, layerMask ))
        {
            var distToHit = Vector3.Distance(transform.position, hit.point);
            laser.transform.localScale = new Vector3(1f, 1f, distToHit);

            if (cursor.activeSelf)
            {
                cursor.transform.position = hit.point;
                cursor.transform.rotation = Quaternion.LookRotation(hit.normal);
                Vector2 rotVector = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                float angle = Mathf.Atan2(rotVector.x, rotVector.y);
            }
            else
            {
                cursor.SetActive(true);
                laser.SetActive(true);
            }

            if (canTeleport)
            {
                // Hit a teleportable surface
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Surface"))
                {
                    cursor.transform.localScale = largeScale;
                    teleportField.SetActive(true);
                    return;
                }
                else
                {
                    cursor.transform.localScale = smallScale;
                    teleportField.SetActive(false);
                }
            }

            // Get button component to see if button is highlighted
            var button = hit.transform.GetComponent<VRButton>();

            // unhover last button
            if (!button && lastButton != null)
            {
                lastButton.SetHover(false);
                lastButton = null;
            }

            // Hover button
            if (button && (lastButton == null || lastButton != button))
            {
                button.SetHover(true);

                if (lastButton)
                    lastButton.SetHover(false);

                lastButton = button;
            }
        }
        else
        {
            // unhover last button
            if (lastButton)
            {
                lastButton.SetHover(false);
                lastButton = null;
            }

            if (cursor.activeSelf)
            {
                cursor.SetActive(false);
                laser.SetActive(false);

                if (canTeleport)
                {
                    teleportField.SetActive(false);
                    cursor.transform.localScale = smallScale;
                }
            }
        }
    }

    public void CheckControllerInput()
    {
        // Shorter names
        var b1 = OVRInput.Button.One;
        var b2 = OVRInput.Button.Two;
        var b3 = OVRInput.Button.Three;
        var b4 = OVRInput.Button.Four;
        
        var rt = OVRInput.RawButton.RIndexTrigger;
        var lt = OVRInput.RawButton.LIndexTrigger;

        bool anyButtonDown = ((hand == primaryHand && (OVRInput.Get(b2) || OVRInput.Get(b1) || OVRInput.Get(rt))) || 
                              (hand == secondaryHand && (OVRInput.Get(b4) || OVRInput.Get(b3) || OVRInput.Get(lt)))) ? true : false;
                              
        bool triggerDown = ((hand == primaryHand && OVRInput.Get(rt)) || 
                              (hand == secondaryHand && OVRInput.Get(lt))) ? true : false;

        if (anyButtonDown)
        {
            if (DelegationManager.Instance)
            {
                DelegationManager.Instance.menu.SetCharacterPanel(true);
            }
        }

        // update cursor
        if (wait < 1f && triggerDown)
        {
            wait += selectTime * Time.deltaTime;
            //Debug.Log("( " + wait + " ) waiting " + selectTime / Time.deltaTime);

            UICircle.SetFillAmount(wait);
        }
        else
        if (wait < 1f && wait > 0f) // The input wait tolerance (80% of ring full)
        {
            wait -= selectTime * Time.deltaTime;
            UICircle.SetFillAmount(wait);
            return;
        }

        if (OVRInput.GetUp(rt))
        {
            //Debug.Log("Input recognized 1");
            if (hand == secondaryHand)
            {
                hand = primaryHand;
                laser.transform.SetParent(hand.transform);
                laser.transform.localPosition = Vector3.zero;
                laser.transform.localRotation = Quaternion.identity;
            }
            else 
            {
                if (lastButton)
                    lastButton.Click();
                //else
                if (teleportField.activeInHierarchy)
                    Teleport();
            }
            wait = 0f;
        }

        if (OVRInput.GetUp(lt))
        {
            //Debug.Log("Input recognized 2");
            if (hand == primaryHand)
            {
                hand = secondaryHand;
                laser.transform.SetParent(hand.transform);
                laser.transform.localPosition = Vector3.zero;
                laser.transform.localRotation = Quaternion.identity;
            }
            else 
            {
                if (lastButton)
                    lastButton.Click();
                //else
                if (teleportField.activeInHierarchy)
                    Teleport();
            }

            wait = 0;
        }

        UICircle.SetFillAmount(wait);
    }

    [ContextMenu("teleport")]
    public void Teleport()
    {
        character.enabled = false;

        Vector3 posOffset = transform.position - headTransform.position;// + new Vector3(0f, 0.1f, 0f);
        posOffset[1] = -0.1f;

        transform.position = teleportField.transform.position + posOffset;
        character.enabled = true;
    }

}
