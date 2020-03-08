using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerBeatEmUp : MonoBehaviour
{
    public Slider HealthUI;
    public Image PlayerSprite;
    public Text Lives;
    public Text PlayerName;

    public GameObject EnemyUI;
    public Slider EnemySlider;
    public Text EnemyName;
    public Image EnemySprite;

    private float EnemyUITime = 4f;
    private float EnemyTimer;

    private LynBeatEmUp player;

    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<LynBeatEmUp>();
        HealthUI.maxValue = player.MaxHealth;
        HealthUI.value = HealthUI.maxValue;
        PlayerName.text = player.Name;
        PlayerSprite.sprite = player.LynSprite;

    }

    private void Update()
    {
        EnemyTimer += Time.deltaTime;
        if (EnemyTimer >= EnemyUITime)
        {
            EnemyUI.SetActive(false);
            EnemyTimer = 0;
        }
    }

    public void UpdateHealth(int valorVida)
    {
        HealthUI.value = valorVida;
    }

    public void UpdateEnemyUI(int maxHealth, int currentHealth, string name, Sprite image)
    {
        EnemySlider.maxValue = maxHealth;
        EnemySlider.value = currentHealth;
        EnemyName.text = name;
        EnemySprite.sprite = image;
        EnemyTimer = 0;
        EnemyUI.SetActive(true);
    }
}
