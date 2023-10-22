
using UnityEngine;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static bool GameActive = false;
    public static bool BossDefeted = false;
    public static float StartTimer = 0f;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public int globalVariable;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
