using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public Slider slider;
    GameManager gm;

	void Start()
	{
		slider.maxValue = 10;
		slider.value = 10;
        gm = GameManager.GetInstance();

	}

    void Update()
	{
		slider.value = gm.vidas;
		if (gm.gameState != GameManager.GameState.GAME) return;
	}

}