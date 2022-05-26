using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class Swipe : MonoBehaviour
    {
        public GameObject scrollbar;
        float scrollPos = 0f;
        float[] pos;
        bool mouseDown=true;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            Scrolling();
        }

        void Scrolling()
        {
            pos = new float[transform.childCount];
            float distance = 1f / (pos.Length - 1f);
            for(int i=0; i<pos.Length; i++)
            {
                pos[i] = distance * i;
            }
            if (mouseDown)
            {
                scrollbar.GetComponent<Scrollbar>().value = 0.5f;
            }
            
            if (Input.GetMouseButton(0))
            {
                scrollPos = scrollbar.GetComponent<Scrollbar>().value;
                mouseDown = false;
            }
            else
            {
                for(int i = 0; i < pos.Length; i++)
                {
                    if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                    {
                        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                    }
                }
            }
            
        }
    }
}
