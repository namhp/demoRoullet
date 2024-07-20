using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lab.Core.UI
{

    public class AlertPanel : AnimatedPanel
    {

        static AlertPanel _instance;

        public static AlertPanel instance
        {
            get
            {
                //GameObject go = GameObject.Find ("AlertScreen");
                //go.SetActive (true);
                return _instance;
            }
        }

        public static void Show(string text, System.Action<bool> onFinish, bool showCancelButton = true)
        {
            if (_instance == null)
            {
                return;
            }

            _instance._Show(text, onFinish, showCancelButton);

        }
        public static void Show(string header, string text, System.Action<bool> onFinish, bool showCancelButton = true)
        {
            if (_instance == null)
            {
                return;
            }

            _instance._Show(header, text, onFinish, showCancelButton);

        }
        public static void ShowWithShop(string header, string text, System.Action<bool> onFinish, bool showCancelButton = true)
        {
            if (_instance == null)
            {
                return;
            }

            _instance._ShowWithShop(header, text, onFinish, showCancelButton);

        }
        [SerializeField] GridLayoutGroup buttonGrid;
        [SerializeField] Button okButton;
        [SerializeField] Button okButtonWithoutCancel;
        [SerializeField] Button cancelButton;
        [SerializeField] GameObject headerGameObject;
        [SerializeField] GameObject blocker;
        [SerializeField] Text headerLabel;
        [SerializeField] Text textLabel;

        [SerializeField] Text okTextLabel;
        [SerializeField] Text cancelTextLabel;


        System.Action<bool> finishCallback = null;

        #region Monobehavior

        void Awake()
        {
            _instance = this;
        }

        // Use this for initialization
        void Start()
        {

        }

        #endregion

        void _Show(string text, System.Action<bool> onFinish, bool showCancelButton = true)
        {
            headerGameObject.SetActive(false);
            if (showCancelButton)
            {
                cancelButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(true);
                okButtonWithoutCancel.gameObject.SetActive(false);
            }
            else
            {
                cancelButton.gameObject.SetActive(false);
                okButton.gameObject.SetActive(false);
                okButtonWithoutCancel.gameObject.SetActive(true);
            }
            //buttonGrid.repositionNow = true;
            blocker.SetActive(true);

            cancelTextLabel.text = "Cancel";
            okTextLabel.text = "OK";
            this.textLabel.text = text;
            finishCallback = onFinish;
            this.show();

        }
        void _Show(string header, string text, System.Action<bool> onFinish, bool showCancelButton = true)
        {
            headerGameObject.SetActive(true);
            if (showCancelButton)
            {
                cancelButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(true);
                okButtonWithoutCancel.gameObject.SetActive(false);
            }
            else
            {
                cancelButton.gameObject.SetActive(false);
                okButton.gameObject.SetActive(false);
                okButtonWithoutCancel.gameObject.SetActive(true);
            }
            //buttonGrid.repositionNow = true;

            cancelTextLabel.text = "Cancel";
            okTextLabel.text = "OK";

            this.headerLabel.text = header;
            this.textLabel.text = text;
            finishCallback = onFinish;
            this.show();

        }
        void _ShowWithShop(string header, string text, System.Action<bool> onFinish, bool showCancelButton = true)
        {
            if (header != string.Empty)
                headerGameObject.SetActive(true);
            else headerGameObject.SetActive(false);
            if (showCancelButton)
            {
                cancelButton.gameObject.SetActive(true);
                okButton.gameObject.SetActive(true);
                okButtonWithoutCancel.gameObject.SetActive(false);
            }
            else
            {
                cancelButton.gameObject.SetActive(false);
                okButton.gameObject.SetActive(false);
                okButtonWithoutCancel.gameObject.SetActive(true);
            }
            //buttonGrid.repositionNow = true;

            cancelTextLabel.text = "NO";
            okTextLabel.text = "YES";
            this.headerLabel.text = header;
            this.textLabel.text = text;
            finishCallback = onFinish;
            this.show();

        }
        public void Ok()
        {
            if (finishCallback != null)
            {
                finishCallback(true);
            }
            blocker.SetActive(false);
            this.hide();
        }

        public void Cancel()
        {
            if (finishCallback != null)
            {
                finishCallback(false);
            }
            blocker.SetActive(false);
            this.hide();
        }

    }

}