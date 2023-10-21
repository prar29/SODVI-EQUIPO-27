using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string sceneName;


    public static GameManager Instance { get; private set; }
    public int puntosTotales = 0;

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "City")
        {
            SumarPuntos(HellExit.Instance.puntuationBefereExit);
        }
        if (sceneName == "Hell")
        {
            SumarPuntos(PlayerHitBox.Instance.puntuationBeforePortal);
        }
    }

    private void Awake(){
        if (Instance == null){
            Instance = this;
        }else{
            Debug.Log("Cuidado! Mas de un GameManager en escena.");
        }
    }

    public void SumarPuntos(int puntosASumar){
        puntosTotales += puntosASumar;
    }
}