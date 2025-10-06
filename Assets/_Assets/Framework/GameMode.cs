using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] Player mPlayerGameObjectPerfab;

    Player mPlayerGameObject;

    void Awake()
    {
        PlayerStart playerStart = FindFirstObjectByType<PlayerStart>();
        
        if (!playerStart)
        {
            throw new System.Exception("Need a player start in the scene for the player spawn location and rotation");
        }

        mPlayerGameObject = Instantiate(mPlayerGameObjectPerfab, playerStart.transform.position, playerStart.transform.rotation);
    }

}
