using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Lab.Core.UI
{


    [RequireComponent(typeof(CanvasGroup))]
    public class PanelController : PanelStack
    {

        protected CanvasGroup _panel;

        public CanvasGroup panel
        {
            get
            {
                if (_panel == null)
                {
                    _panel = GetComponent<CanvasGroup>();
                }
                return _panel;
            }
        }


        protected PanelStack _parentStack;
        public PanelStack parentStack
        {
            get
            {
                return _parentStack;
            }
            set
            {
                _parentStack = value;
            }
        }

        public bool isShowing
        {
            get
            {
                return (panel.alpha > 0 && gameObject.activeSelf);
            }
        }


        public float alpha
        {
            get
            {
                return panel.alpha;
            }
            set
            {
                panel.alpha = value;
            }
        }


        public bool blocksRaycasts
        {
            get
            {
                return panel.blocksRaycasts;
            }
            set
            {
                panel.blocksRaycasts = value;
            }
        }


        public virtual void show(System.Action onStart = null, System.Action onFinish = null)
        {
            if (this.panel.alpha == 1f)
            {
                return;
            }

            this.gameObject.SetActive(true);

            panelWillShow();
            this.panel.alpha = 1f;
            panelDidShow();

            if (onFinish != null)
            {
                onFinish();
            }
        }

        public virtual void hide(System.Action onStart = null, System.Action onFinish = null)
        {
            if (this.panel.alpha == 0f)
            {
                return;
            }

            panelWillHide();
            this.panel.alpha = 0f;
            panelDidHide();

            this.gameObject.SetActive(false);

            if (onFinish != null)
            {
                onFinish();
            }
        }

        public virtual void panelWillShow()
        {

        }

        public virtual void panelDidShow()
        {

        }

        public virtual void panelWillHide()
        {

        }

        public virtual void panelDidHide()
        {

        }



    }

}