using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class DrawCardToRiver : MonoBehaviour
{
     public GameObject hand1;
     public GameObject hand2;
     public GameObject hand3;
     public GameObject hand4;

    public HandManagerScript handScript;
    public HandManagerScript handScript2;
    public HandManagerScript handScript3;
    public HandManagerScript handScript4;
    public Calculator calculatorScript;

    public bool player1Turn;
    public bool player2Turn;
    public bool player3Turn;
    public bool player4Turn;


     private void Start()
     {
          
     }
  /* public void CardToRiver()
   { 
     SetAllHandsActive();

        
        calculatorScript.Calculate();

        SetAllHandsInactive();

        if (calculatorScript.Calculate() == 1)
          {
               hand1.SetActive(true);
          }
            if (calculatorScript.Calculate() == 2)
           {
                    hand2.SetActive(true);
           }
           
            if (calculatorScript.Calculate() == 3)
           {
                    hand3.SetActive(true);
           }

            if (calculatorScript.Calculate() == 4)
           {
                    hand4.SetActive(true);
           }
           
      
   }
*/
   private void Update()
   {
     if (Keyboard.current.spaceKey.wasPressedThisFrame)
     {
          TheRightDraw();

     }    
   }

   public void SetAllHandsActive()
   {
        hand1.SetActive(true);
        hand2.SetActive(true);
        hand3.SetActive(true);
        hand4.SetActive(true);
   }

   public void SetAllHandsInactive()
   {
        hand1.SetActive(false);
        hand2.SetActive(false);
        hand3.SetActive(false);
        hand4.SetActive(false);
   }


   public void DrawAll()
     {
        handScript.DrawToRiver();
        handScript.Draw();
        
        
     }

     public void PlayersDraw()
     {
        handScript.Draw();
        handScript2.Draw();
        handScript3.Draw();
        handScript4.Draw();
        
     }

     public void TheRightDraw()
     {
         
         handScript.Draw();
         handScript.Draw();
         DrawAll(); 




     }

     



}
