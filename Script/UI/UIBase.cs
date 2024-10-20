using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UI
{
    public abstract class UIBase : MonoBehaviour
    {
        public abstract void OpenUI();
        public abstract void CloseUI();
        public abstract void Init();
        public abstract void UpdateDataAll();
        public abstract void UpdateData();
        public virtual IEnumerator OpenCanvas(float fullTime)
        {
            float time = 0.0f;

            while (time < fullTime)
            {
                time += Time.unscaledDeltaTime;

                transform.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0.0f, 1.0f, time / fullTime);

                yield return null;
            }

            UIManager.Inst().SetUsing(false);

            yield return null;
        }
        public virtual IEnumerator CloseCanvas(float fullTime)
        {
            float time = 0.0f;

            while (time < fullTime)
            {
                time += Time.unscaledDeltaTime;

                transform.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1.0f, 0.0f, time / fullTime);

                yield return null;
            }

            gameObject.SetActive(false);
            UIManager.Inst().SetUsing(false);

            yield return null;
        }
    }
}
