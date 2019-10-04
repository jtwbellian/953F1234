using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


[System.Serializable]
public class StringTransformDictionary : SerializableDictionary<string, Transform> {}

public class DelegationMenu : MonoBehaviour
{
    const float MAX_DIST_TO_HEAD = 0.3f;
    Transform cam; 
    public List<Sprite> imageIcons;
    public TextMeshProUGUI currentCharacter;
    public TextMeshProUGUI currentCharacterAction;

    [Tooltip("0 - Action\n 1 - location\n 2 - ???")]
    public GameObject [] tabs; // Action tab should be 0, location 1, ect...

    public List<CharacterButton> characters;
    public GameObject characterPrefab;

    public GameObject actionPanel;
    public GameObject characterPanel;
//    public StringTransformDictionary locations;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main ? Camera.main.transform : transform;
    }

    // Update is called once per frame
    void Update()
    {
        cam = Camera.main.transform;

        if (cam)
        {
            var distToHead = Vector3.Distance(transform.position, cam.position);

            if (distToHead > MAX_DIST_TO_HEAD)
                transform.position = cam.position;
        }	
    }

    public CharacterButton AddCharacter(FireFighter source)
    {
        var offset = 0.35f; 

        Debug.Log("Creating fire fighter...");
        GameObject obj = Instantiate(characterPrefab, characterPrefab.transform.position + characterPrefab.transform.right * (offset * (characters.Count - 1)) , Quaternion.identity);
        obj.transform.SetParent(characterPrefab.transform.parent);
        obj.transform.localScale = characterPrefab.transform.localScale;
        obj.transform.localRotation = Quaternion.identity;

        CharacterButton charaButton = obj.GetComponent<CharacterButton>();
        charaButton.SetSource(source);
        charaButton.SetImage(imageIcons[source.GetCurrentHead()]);

        obj.SetActive(true);

        characters.Add(charaButton);
        Debug.Log("Welcome to the force, " + source.name + ".");
        
        return charaButton;
    }

    public void ClearSelection()
    {
        foreach (var cb in characters)
        {
            cb.progressMeter.SetRing(false);
            cb.actor.SetOutline(false);
        }
    }

    public void SetActionPanel(bool active)
    {
        actionPanel.SetActive(active);
    }

    public void SetCharacterPanel(bool active)
    {
        characterPanel.SetActive(active);
    }

    public void SetTab(int tab)
    {
        for( int i = 0; i < tabs.Length; i ++)
        {
            if (i == tab)
            {
                tabs[i].gameObject.SetActive(true);
            }
            else
            {
                tabs[i].gameObject.SetActive(false);
            }
        }
    }

    void SetName(string name)
    {
        currentCharacter.text = name;
    }

    public void SetCurrentAction(string action)
    {
        currentCharacterAction.text = action;
    }


    public void CreateAction(GameObject prefab)
    {
        var action = Instantiate(prefab);
        DelegationManager.Instance.Selection(action);
        SetTab(1);
    }

}
