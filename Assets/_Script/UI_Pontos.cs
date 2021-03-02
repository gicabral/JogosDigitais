using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Pontos : MonoBehaviour
{
    // Start is called before the first frame update
   Text textComp;
   GameManager gm;
   void Start()
   {
       textComp = GetComponent<Text>();
       gm = GameManager.GetInstance();
   }
   
   void Update()
   {
       textComp.text = $"Pontos: {gm.pontos}";
   }
}
