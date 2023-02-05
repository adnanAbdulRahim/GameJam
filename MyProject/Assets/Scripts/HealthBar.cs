using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{

    private RectTransform bar;
    private Image barImage;

    // Start is called before the first frame update
    void Start()
    {
        
        bar = GetComponent<RectTransform>();
        SetSize(Health.totalHealth);
        if(Health.totalHealth < 0.3f)
        {
            barImage.color = Color.red;
        }
        barImage = GetComponent<Image>();

    }

    public void Damage(float damage)
    {
        if((Health.totalHealth -= damage) >= 0f)
        {
                Health.totalHealth -= damage;
        }
        else
        {
            Health.totalHealth = 1f;
            SceneManager.LoadScene("Main");

        }
        if(Health.totalHealth < 0.3f)
        {
            barImage.color = Color.red;
        }
        
        SetSize(Health.totalHealth);
        
    }

    public void SetSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);
    }
}
