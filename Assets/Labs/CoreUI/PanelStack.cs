using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lab.Core.UI
{


    public class PanelStack : MonoBehaviour
    {

        protected Stack<PanelController> panelStack = new Stack<PanelController>();

        public PanelController currentPanel
        {
            get
            {
                if (panelStack.Count > 0)
                {
                    return panelStack.Peek();
                }
                return null;
            }
        }

        public int count
        {
            get
            {
                return panelStack.Count;
            }
        }


        //Push panel will hide the panel before it
        public virtual void pushPanel(PanelController panel)
        {

            if (currentPanel != null)
            {

                currentPanel.hide(() =>
                {

                    panelStack.Push(panel);
                    panel.parentStack = this;
                    panel.show();

                });

            }
            else
            {

                panelStack.Push(panel);
                panel.parentStack = this;
                panel.show();

            }


        }

        //Push panel without hiding panel below it
        public virtual void pushPanelOnTop(PanelController panel)
        {
            panelStack.Push(panel);
            panel.parentStack = this;
            panel.show();
        }

        //Pop the top panel off the stack and show the one beheath it
        public virtual void PopPanel()
        {

            var oldPanel = panelStack.Pop();

            oldPanel.hide(() =>
            {

                var newPanel = this.currentPanel;
                if (newPanel != null && !newPanel.isShowing)
                {
                    newPanel.show();
                }

            });

        }


        //Pop all panels till there is only one panel left in the stack
        public virtual void PopToTop()
        {
            //Pop Panels Till Top
            while (panelStack.Count > 1)
            {
                currentPanel.hide();
                panelStack.Pop();
            }

            if (!currentPanel.isShowing)
            {
                currentPanel.show();
            }

        }

        //Pop till we remove specific panel
        public virtual void PopPanel(PanelController panel)
        {

            if (!panelStack.Contains(panel))
            {
                return;
            }

            PanelController oldPanel = null;

            //Pop panels until we find the right one we're trying to pop
            do
            {
                oldPanel = panelStack.Pop();
                oldPanel.hide();
            } while (oldPanel != panel && panelStack.Count > 0);

            var newPanel = this.currentPanel;
            if (newPanel != null && !newPanel.isShowing)
            {

                newPanel.show();
            }

        }

    }

}