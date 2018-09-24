using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image FillImage;
    public RunnerController Player;

    public Gradient gradient;

    private void Update()
    { 
        if (FillImage && Player)
        {
            float percentHealth = Player.CurrentHealth / Player.MaxHealth;
            FillImage.fillAmount = percentHealth;

            FillImage.color = gradient.Evaluate(percentHealth);
        }
    }
}
