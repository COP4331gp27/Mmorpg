using UnityEngine;
using UnityEngine.Networking;
/*
 * This script keeps track of game events and tags players when they enter the server
 */
public class GameStateManager : MonoBehaviour {

    public static GameStateManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    private NetworkManager nMan;
    private string GameState;
    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {

            //if not, set instance to this
            instance = this;
        }

        //If instance already exists and it's not this:
        else if (instance != this) {

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        nMan = FindObjectOfType<NetworkManager>();
        getPlayers();
        //Call the InitGame function to initialize the first level 
        //InitGame();
    }

    //Initializes the game for each level.
    //void InitGame()
    //{
    //    //Call the SetupScene function of the BoardManager script, pass it current level number.
    //    boardScript.SetupScene(level);

    //}


    private static PhotonView ScenePhotonView;
	//Gets the photonView attached to this when the game starts
    void Start()
    {
        ScenePhotonView = this.GetComponent<PhotonView>();
    }
	//when the player connects, tag this player
    void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        Debug.Log("OnPhotonPlayerConnected: " + player);

        // when new players join, we send "who's it" to let them know
        // only one player will do this: the "master"

        if (PhotonNetwork.isMasterClient)
        {
            TagPlayer(player.ID);
        }
    }
	//tags the player with an ID
    public static void TagPlayer(int playerID)
    {
        Debug.Log("TagPlayer: " + playerID);
       // ScenePhotonView.RPC("TaggedPlayer", PhotonTargets.All, playerID);
    }
	//gets the players on the server and sets the gamestate to "Players Spawned"
    public void getPlayers()
    {
        Player[] players;
        players = FindObjectsOfType<Player>();
        GameState = "Players Spawned";
    }
	//gets the gamestate string
    public string getGameState()
    {
        return GameState;
    }

}
