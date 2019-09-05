#if (UNITY_EDITOR) 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class DelegationSystemEditor : EditorWindow
{
    ActorManager actorManager;
    [MenuItem("Window/Binari Studios/Delegation System")]
    public static void ShowWindow(){
        EditorWindow w = EditorWindow.GetWindow(typeof(DelegationSystemEditor));

        w.maxSize = new Vector2(500, 160);
        w.minSize = new Vector2(500, 160);


        VisualTreeAsset uiAsset = AssetDatabase
                                    .LoadAssetAtPath<VisualTreeAsset>(
                                        "Assets/Scripts/DelegationSystem/UI/DelegationSystemEditor.uxml");
        StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/DelegationSystem/UI/Editor.uss");
        VisualElement ui = uiAsset.CloneTree();
        ui.styleSheets.Add(style);
        var quickButton = ui.Query<Button>("quickstartButton").First();
        var genActor = ui.Query<Button>("generateActor").First();
        var genLoc = ui.Query<Button>("generateLocation").First();
        var genAction = ui.Query<Button>("generateAction").First();

        quickButton.RegisterCallback<MouseUpEvent>(ev => {
            GenerateActor(3);
            GenerateLocation(3);
            GenerateAction(3);

            GenerateActorManager();
            GenerateDelegationManager();
            GenerateInputManager();
        });

        genActor.RegisterCallback<MouseUpEvent>(ev =>{
            GenerateActor(1);
        });

        genLoc.RegisterCallback<MouseUpEvent>(ev =>{
            GenerateActor(1);
        });

        genAction.RegisterCallback<MouseUpEvent>(ev =>{
            GenerateActor(1);
        });

        w.rootVisualElement.Add(ui);
    }

    public static void GenerateActor(int numOfActors){
        var numActors = GameObject.FindObjectsOfType<DelegationActor>().Length;
        DelegationActor actor;
        GameObject gameObject;
        for(int i = 0; i < numOfActors; i++){
            gameObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            gameObject.tag = "actor";
            gameObject.name = string.Format("Actor {0}", i+1);
            actor = gameObject.AddComponent<DelegationActor>() as DelegationActor;
            actor.uid = i;
            gameObject.AddComponent<KnockDownDoor>();
        }
    }

    public static void GenerateLocation(int numOfLocations){
        var numLocations = GameObject.FindObjectsOfType<DelegationLocation>().Length;
        GameObject gameObject;
        for(int i = 0; i < numOfLocations; i++){
            gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObject.tag = "location";
            gameObject.name = string.Format("Location {0}", i+1);
        }
    }

    public static void GenerateAction(int numOfActions){
        var numActions = GameObject.FindObjectsOfType<DelegationAction>().Length;
        GameObject gameObject;
        for(int i = 0; i < numOfActions; i++){
            gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameObject.tag = "action";
            gameObject.name = string.Format("Action {0}", i+1);
        }
    }

    public static void GenerateInputManager(){
        GameObject gameObject;
        InputManager inputManager;
        var delManager = GameObject.FindObjectOfType<DelegationManager>();
        gameObject = new GameObject(string.Format("Input Manager"));
        inputManager = gameObject.AddComponent<InputManager>() as InputManager;
        inputManager.delegationManager = delManager;
    }

    public static void GenerateActorManager(){
        GameObject gameObject;
        gameObject = new GameObject(string.Format("Actor Manager"));
        gameObject.AddComponent<ActorManager>();
    }

    public static void GenerateDelegationManager(){
        GameObject gameObject;
        DelegationManager delManager;
        var actorManager = GameObject.FindObjectOfType<ActorManager>();
        gameObject = new GameObject(string.Format("Delegation Manager"));
        delManager = gameObject.AddComponent<DelegationManager>() as DelegationManager;
        delManager.actorManager = actorManager;
    }


}

#endif