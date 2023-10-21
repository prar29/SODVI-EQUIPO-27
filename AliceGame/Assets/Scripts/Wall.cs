using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [Header("Wall velocity")]
    public float wallVelocity; 
    public float dangerousDistance;
    [SerializeField] LayerMask playerWallTrigger;

    public float portalRealPositionX;
    public float portalRealPositionY;
    public float portalPositionY;

    private Rigidbody2D rigidBody;
    Player player;

    public static Wall Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        rigidBody = GetComponent <Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        portalPositionY = player.transform.position.y + 1.0f;
        rigidBody.transform.position = new Vector3 (rigidBody.transform.position.x, portalPositionY, 0);
        wallMovement();
        if(playerWall()){
            BackgroundMusic.Instance.closePortalPitch(1.25f);
        }else{
            BackgroundMusic.Instance.normalPitch();
        }

        portalRealPositionX = rigidBody.transform.position.x - 10f;
        portalRealPositionY = rigidBody.transform.position.y;
    }

    private void wallMovement(){
        rigidBody.velocity = new Vector2(wallVelocity, rigidBody.velocity.y);
        rigidBody.AddForce(Vector2.right * Player.Instance.playerVelocityX, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * dangerousDistance);
    }
    
    bool playerWall(){
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.right, dangerousDistance, playerWallTrigger);
        return raycastHit.collider != null;
    }

}
