using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    #region Singleton

    static PlayerSystem instance;
    public static PlayerSystem Instance => instance ? instance : new GameObject("PlayerSystem").AddComponent<PlayerSystem>();

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);
    }

    #endregion

    #region Variable

    [SerializeField] GameObject player;
    [SerializeField] GameObject playerCamPos;

    #endregion

    #region Property

    public GameObject Player => player != null ? player : player = GameObject.FindGameObjectWithTag("Player");

    public GameObject PlayerCamPos => playerCamPos != null ? playerCamPos : playerCamPos = GameObject.FindGameObjectWithTag("CamPos");

    #endregion

    #region Unity Life Cycle

    private void Start()
    {
        transform.SetParent(FolderSystem.Instance.SystemFolder);
    }

    #endregion
}
