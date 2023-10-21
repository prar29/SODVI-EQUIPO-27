using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HellExit : MonoBehaviour
{
    public int puntuationBefereExit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("City");
            puntuationBefereExit = GameManager.Instance.puntosTotales;
        }
    }

    public static HellExit Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
