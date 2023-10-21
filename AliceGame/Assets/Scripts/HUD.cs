using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI puntos;

  public static HUD Instance { get; private set; }

	private void Awake(){
        if (Instance == null){
            Instance = this;
        }
    }
    public void updateScore(){
		puntos.text = GameManager.Instance.puntosTotales.ToString();
	}
}