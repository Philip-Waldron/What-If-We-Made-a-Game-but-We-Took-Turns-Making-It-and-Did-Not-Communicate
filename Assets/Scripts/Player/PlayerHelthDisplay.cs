using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHelthDisplay : MonoBehaviour
{
    public Text helthText;
    public Image helthImage;

    public Sprite feelinFine;
    public Sprite tisJustAScratch;
    public Sprite feelinDicey;
    public Sprite ahNo;

    void Update()
    {
        UpdateHelthText();
        UpdateHelthImage();
    }

    private void UpdateHelthText()
    {
        helthText.text = $"Helth: {PlayerController.Instance.Helth}";
    }

    private void UpdateHelthImage()
    {
        float percentage = PlayerController.Instance.Helth / PlayerController.Instance.GetMaxHelth();

        if (percentage >= 1)
        {
            helthImage.sprite = feelinFine;
        }

        else if (percentage > 0.33)
        {
            helthImage.sprite = tisJustAScratch;
        } 
        
        else if (percentage > 0)
        {
            helthImage.sprite = feelinDicey;
        }

        else
        {
            helthImage.sprite = ahNo;
        }
    }
}
