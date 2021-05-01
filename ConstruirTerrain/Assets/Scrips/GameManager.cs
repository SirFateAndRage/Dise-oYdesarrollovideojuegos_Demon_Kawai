using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public MainPlayer player;
    public Transform respawn;
    public Image lifeBar;


    [SerializeField]
    private HealtBarr _healtbar;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public float NyaTime=1;
    void Update()
    {
        if (!player.IsAlive)
            RespawnPlayer();
        else
        {
            lifeBar.fillAmount = (player.health / player.maxHealth);
            
        }

        //Time.timeScale = NyaTime;
    }
    // Update is called once per frame
    private void RespawnPlayer()
    {
        player.transform.position = respawn.position;
        player.health = player.maxHealth;
        //_healtbar.SetSize(1f);
    }
}
