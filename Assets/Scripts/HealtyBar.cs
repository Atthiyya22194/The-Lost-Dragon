using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtyBar : MonoBehaviour
{
    public float CurrentHealth;
    private Image HPbar;
    private float MaxHealth = 100f;
    FirstPersonController player;

    // Start is called before the first frame update
    void Start()
    {
        HPbar = GetComponent<Image>();
        player = FindObjectOfType<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth = player.currentHealth;
        HPbar.fillAmount = CurrentHealth / MaxHealth;
    }
}
