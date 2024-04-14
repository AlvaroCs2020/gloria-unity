using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UiSystem
{
    
    public class UiHandler : MonoBehaviour
    {
        private UiText _ammoText;
        private UiHealthBar _healthBar;
        public static UiHandler Instance { get; private set; }
        private void Awake() 
        {
            Instance = this;
        }

        //Ammo handling
        public void UpdateAmmoText(int ammo)
        {
            _ammoText?.UpdateText(ammo.ToString());
        }
        public void UpdateHealthBar(float health)
        {
            _healthBar?.UpdateHealthBar(health);
        }
        public void AssignAmmoText(UiText ammoText)
        {
            _ammoText = ammoText;
        }
        public void AssignHealthBar(UiHealthBar healthBar)
        {
            _healthBar = healthBar;
        }
    }

    public abstract class UiText : MonoBehaviour
    {
        
        private TextMeshProUGUI _scoreText;
        private void Awake() 
        {
            _scoreText = GetComponent<TextMeshProUGUI>();   
        }
        protected abstract void Start();//cada instancia se asigna en el input handler

        public void UpdateText(string text){ _scoreText.text = text; }
    }
    public abstract class UiHealthBar : MonoBehaviour
    {
        
        private RawImage _rawImage;
        private void Awake() 
        {
            _rawImage = GetComponent<RawImage>();   
        }
        protected abstract void Start();//cada instancia se asigna en el input handler

        public void UpdateHealthBar(float healt)
        {
            _rawImage.rectTransform.sizeDelta = new Vector2(healt, _rawImage.rectTransform.sizeDelta.y);
        }
    }
}
