using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] Player mPlayerGameObjectPerfab;

    Player mPlayerGameObject;

    public Player mPlayer => mPlayerGameObject;

    public static GameMode MainGameMode;

    public BattleManager BattleManager { get; private set; }

    void OnDestroy()
    {
        if (MainGameMode == this)
        {
            MainGameMode = null;
        }        
    }
    void Awake()
    {

        if (MainGameMode != null)
        {
            Destroy(gameObject);
        }

        MainGameMode = this;

        BattleManager = gameObject.AddComponent<BattleManager>();

        PlayerStart playerStart = FindFirstObjectByType<PlayerStart>();
        
        if (!playerStart)
        {
            throw new System.Exception("Need a player start in the scene for the player spawn location and rotation");
        }

        mPlayerGameObject = Instantiate(mPlayerGameObjectPerfab, playerStart.transform.position, playerStart.transform.rotation);
    }

}
