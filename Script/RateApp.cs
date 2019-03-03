using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateApp : MonoBehaviour {

    public Image[] stars;
    public Sprite starSpriteON;
    public Sprite starSpriteOFF;

    private void OnEnable()
    {
        foreach(Image star in stars)
        {
            star.sprite = starSpriteOFF;
        }
    }

    public void Rate(int index)
    {
        for(int i = 0; i <= index; i++)
        {
            stars[i].sprite = starSpriteON;
        }
        if(index >= 2)
        {
            //Переход на страницу с игрой
        }
        MenuSettings.instance.SoundInterface(0);
    }
}
