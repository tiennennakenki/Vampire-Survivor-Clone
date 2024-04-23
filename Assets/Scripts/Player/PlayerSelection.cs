using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : SaiMonoBehaviour
{
    private static PlayerSelection instance;
    public static PlayerSelection Instance => instance;

    [SerializeField] protected List<GameObject> playerPrefabs = new List<GameObject>();
    public List<GameObject> PlayerPrefabs => playerPrefabs;

    [SerializeField] protected string characterName;
    public string CharacterIndex => characterName;

    [SerializeField] protected CameraController cameraController;
    public CameraController CameraController => cameraController;

    public delegate void CharacterSetEventHandler();
    public static event CharacterSetEventHandler CharacterSetEvent;

    protected override void Start()
    {
        base.Start();
        this.SetCharacter();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerPrefabs();
        this.LoadCameraController();
    }

    protected virtual void LoadCameraController()
    {
        if (this.cameraController != null) return;
        this.cameraController = GameObject.Find("CameraController").GetComponent<CameraController>();
        Debug.LogWarning(transform.name + ": LoadCameraController", gameObject);
    }

    protected virtual void SetCharacter()
    {
        this.characterName = PlayerPrefs.GetString("SelectedCharacter");
        foreach(GameObject player in playerPrefabs)
        {
            //Debug.LogWarning(player.name + "=====" + this.characterName);
            if(player.name == this.characterName)
            {
                player.SetActive(true);
                CharacterSetEvent?.Invoke();
                return;
            }
        }

        //GameObject player = Instantiate(playerPrefabs[this.characterName], transform.position, Quaternion.identity);
        //cameraController.Player = player.transform;
        //player.SetActive(true);
    }

    protected virtual void LoadPlayerPrefabs()
    {
        if (this.playerPrefabs.Count > 0) return;

        Transform collection = transform;

        foreach (Transform col in collection)
        {
            GameObject player = col.gameObject; // Get the GameObject from the Transform
            this.playerPrefabs.Add(player);
        }

        Debug.LogWarning(transform.name + ": LoadPlayerPrefabs", gameObject);
    }
}
