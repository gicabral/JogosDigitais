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
		slider.maxValue = 3;
		slider.value = 3;
        gm = GameManager.GetInstance();

	}

    void Update()
	{
		slider.value = gm.vidas;

	}

}