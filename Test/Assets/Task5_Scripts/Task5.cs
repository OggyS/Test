using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Test
{
    public class Task5 : MonoBehaviour
    {
        [SerializeField]
        GameObject small;
        [SerializeField]
        GameObject medium;
        [SerializeField]
        GameObject big;
        [SerializeField]
        GameObject options;
        bool isOptionsActive=false;
        [SerializeField]
        TMP_Dropdown dp;
        [SerializeField]
        Slider slider;
        [SerializeField]
        TextMeshProUGUI txt;
        Color[] colors;
        [SerializeField]
        GameObject sphere;
        GameObject anti_stress_ball;
        bool firstCallSlider = false;
        private void Start()
        {
            Button _small = small.GetComponent<Button>();
            _small.onClick.AddListener(()=>OnSelection(small));
            Button _medium = medium.GetComponent<Button>();
            _medium.onClick.AddListener(() => OnSelection(medium));
            Button _big = big.GetComponent<Button>();
            _big.onClick.AddListener(() => OnSelection(big));

        }
        public void OnSelection(GameObject obj)
        {
            dp.options.Clear();
            if (isOptionsActive == false)
            {
                isOptionsActive = true;
                options.SetActive(true);
            }
           
            Attributes atr = obj.GetComponent<Attributes>();
            TMP_Dropdown.OptionData newOption;
            List<TMP_Dropdown.OptionData> newOptions=new List<TMP_Dropdown.OptionData>();
            colors = new Color[atr.colors.Count];
            for (int i = 0; i < atr.colors.Count; i++)
            {
                newOption = new TMP_Dropdown.OptionData();
                //Debug.Log(i+"="+atr.sprites[i].name);
                newOption.image = atr.sprites[i];
                newOptions.Add(newOption);
                colors[i] = atr.colors[i];
            }
            dp.AddOptions(newOptions);
            slider.minValue = atr.minSize * 10;
            slider.maxValue = atr.maxSize * 10;
            if (anti_stress_ball == null)
            {
                anti_stress_ball = Instantiate(sphere, UnityEngine.Vector3.zero, Quaternion.identity, transform);
                anti_stress_ball.transform.localScale = new UnityEngine.Vector3(slider.value / 100f, slider.value / 100f, slider.value / 100f);
                txt.text =( slider.value / 10f).ToString() + "cm";
            }
            anti_stress_ball.GetComponent<MeshRenderer>().material.color = atr.colors[0];//new size first color
        }
       public void OnSliderValueChange()
        {
            if (slider.value != slider.minValue)
            {
                firstCallSlider = true;
            }
            if (firstCallSlider)
            {
                anti_stress_ball.transform.localScale = new UnityEngine.Vector3(slider.value / 100f, slider.value / 100f, slider.value / 100f);
                txt.text = (slider.value / 10f).ToString() + "cm";
            }
           
        }
        public void OnColorSelectted()
        {
            anti_stress_ball.GetComponent<MeshRenderer>().material.color = colors[dp.value];
            Debug.Log(colors[dp.value]);
            if (colors[dp.value].r==0&& colors[dp.value].g == 0 && colors[dp.value].b == 0)
            {
                Debug.Log("black");
                anti_stress_ball.transform.localScale = new UnityEngine.Vector3(0.5f,0.5f,0.5f);
                txt.text = (0.5f).ToString() + "cm";
            }
            else
            {
                anti_stress_ball.transform.localScale = new UnityEngine.Vector3(slider.value / 100f, slider.value / 100f, slider.value / 100f);
                txt.text = (slider.value / 10f).ToString() + "cm";
            }
        }
    }
}
