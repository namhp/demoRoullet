using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lab.Core.UI
{


    public class PopupPanel : AnimatedPanel
    {

        public virtual void showPopup()
        {
            pushPanelOnTop(this);
            alpha = 1f;
            blocksRaycasts = true;
        }

        public virtual void hidePopup()
        {
            PopPanel();
            blocksRaycasts = false;
        }

        protected void Awake()
        {
            this.alpha = 0f;
            this.blocksRaycasts = false;
        }


    }

}