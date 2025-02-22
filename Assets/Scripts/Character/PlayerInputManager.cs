using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{

    public static PlayerInputManager instance;

    PlayerControls playerControls;

    [SerializeField] Vector2 movementInput;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        SceneManager.activeSceneChanged += OnSceneChange;
        instance.enabled = false;
    }

    private void OnSceneChange(Scene  oldScene, Scene newScene)
    {
        // IF WE ARE LOADING INTO OUR WORLD SCENE, ENABLE OUR PLAYERS CONTROLS
        if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
        {
            instance.enabled = true;
        }
        // OTHERWISE WE MUST BE AT THE MAIN MENUI, DISABLE OUR PLAYER CONTROLS
        // THIS IS SO OUR PLAYER CANT MOVE AROUND IF WER ENTER THINGS LIKE ACHARACTTER  CREATION MENU ETDC
        else
        {
            instance.enabled = false;
        }
    }


    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            playerControls.Enable();
        }
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

}
