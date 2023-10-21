using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHitBox : MonoBehaviour
{
    public int puntuationBeforePortal;

    public static PlayerHitBox Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Wall")){
            puntuationBeforePortal = GameManager.Instance.puntosTotales;
            SceneManager.LoadScene("Hell");
        }
    }
}
