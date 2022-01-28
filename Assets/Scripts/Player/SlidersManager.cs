using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidersManager: MonoBehaviour
{
    //HP and Sugar bars controller script

    public Slider sliderHp; //HP bar assigned to this field
    public Slider sliderSugar; //sugar bar assigned to this field


    //Prepare the maximum value for the slider component
    public void SetMaxHealth(int health)
    {
        sliderHp.maxValue = health;
        sliderHp.value = health;
    }

    //Prepare the maximum value for the slider component
    public void SetMaxSugar (int sugar)
    {
        sliderSugar.maxValue = sugar;
        sliderSugar.value = sugar;
    }

    //Prepare the "filling" value for the slider component
    public void SetHealth(int health)
    {
        sliderHp.value = health;
    }
    
    //Prepare the "filling" value for the slider component
    public void SetSugar(int sugar)
    {
        sliderSugar.value = sugar;
    }
}
