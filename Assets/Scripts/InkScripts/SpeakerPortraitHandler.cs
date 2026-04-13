using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerPortraitHandler : MonoBehaviour
{
    public Sprite portrait_yuriko, portrait_iron, portrait_bird, portrait_dragon, portrait_rollcat, portrait_moon;
    public Image portraitImage; //UI element
    public InkDialoguePlayer InkDialogueManager; //reference to dialogue manager to access tags
    
    public void UpdatePortrait()
    {
        Tag portraitTag = null;

        //find the portrait tag in the current line
        portraitTag = InkDialoguePlayer.tags.Find(tag => tag.key == "portrait");

        if (portraitTag != null)
        {
            switch (portraitTag.value)
            {
                case "yuriko":
                    portraitImage.sprite = portrait_yuriko;
                    break;
                case "iron":
                    portraitImage.sprite = portrait_iron;
                    break;
                case "bird":
                    portraitImage.sprite = portrait_bird;
                    break;
                case "dragon":
                    portraitImage.sprite = portrait_dragon;
                    break;
                case "rollcat":
                    portraitImage.sprite = portrait_rollcat;
                    break;
                case "moon":
                    portraitImage.sprite = portrait_moon;
                    break;
                default:
                    break;
            }
        } else
        {
            print("no portrait tag detected in current line.");
        }
    }
}
