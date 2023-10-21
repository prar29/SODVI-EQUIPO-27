using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int valor;
    public AudioClip coinSFX;

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            GameManager.Instance.SumarPuntos(valor);
            Destroy(this.gameObject);
            HUD.Instance.updateScore();
            AudioManager.Instance.ReproducirSonido(coinSFX);
        }
    }
}