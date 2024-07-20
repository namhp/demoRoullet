using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Lab.Core.UI
{


    public class AnimatedPanel : PanelController
    {


        //public AnimationClip showAnimation;
        public OpenAnimation showAnimation;

        //public AnimationClip hideAnimation;
        public CloseAnimation hideAnimation;

        public string sfxWillAppear = "";
        public string sfxDidAppear = "";

        public string sfxWillHide = "";
        public string sfxDidHide = "";

        public float defaultFadeTime = 0.1f;

        System.Action onShowStartCallback = null;
        System.Action onShowFinishCallback = null;

        System.Action onHideStartCallback = null;
        System.Action onHideFinishCallback = null;

        public override void show(System.Action onStart = null, System.Action onFinish = null)
        {
            if (this.panel.alpha == 1f && this.gameObject.activeSelf)
            {
                return;
            }
            onShowStartCallback = onStart;
            // hack for this game
            onShowFinishCallback = onFinish;

            this.gameObject.SetActive(true);

            //TODO: Block user input here	
            //TouchBlockerPanel.Block();

            if (!string.IsNullOrEmpty(sfxWillAppear))
            {
                //			MasterAudio.PlaySoundAndForget(sfxWillAppear); 
                //SoundManager.Instance.PlayEffectSound (sfxWillAppear);
            }

            try
            {
                panelWillShow();
            }
            catch (System.Exception e)
            {
                Debug.LogError("PanelWillShow Failed " + e.Message + "\n " + e.StackTrace);
            }

            //this.panel.alpha = 1f;

            if (showAnimation != null)
            {
                showAnimation.play(OnShowStarted, OnShowFinished);
            }
            else
            {
                //Default to 1 second tween
                //this.panel.alpha = 0;
                //LeanTween.alpha(this.gameObject, 1f, defaultFadeTime).setOnComplete(OnShowFinished);
                this.panel.alpha = 1f;
                OnShowFinished();

            }

        }

        public override void hide(System.Action onStart = null, System.Action onFinish = null)
        {

            if (this.alpha == 0)
            {
                return;
            }
            onHideStartCallback = onStart;
            // hack for this game
            onHideFinishCallback = onStart;

            if (!string.IsNullOrEmpty(sfxWillHide))
            {
                //			MasterAudio.PlaySoundAndForget(sfxWillHide);
                //SoundManager.Instance.PlayEffectSound (sfxWillHide);
            }

            try
            {
                panelWillHide();
            }
            catch
            {
                Debug.LogError("PanelWillHide Failed " + this.name);
            }

            if (hideAnimation != null)
            {
                hideAnimation.play(OnHideStarted, OnHideFinished);
            }
            else
            {
                //Default Hide Fade
                this.panel.alpha = 0f;
                //LeanTween.value(this.panel.alpha, this.panel.alpha, 0f, defaultFadeTime).setOnComplete(OnHideFinished);
                OnHideFinished();
            }

        }


        protected virtual void OnShowStarted()
        {

            blocksRaycasts = false;

            if (!string.IsNullOrEmpty(sfxWillAppear))
            {
                //SoundManager.Instance.PlayEffectSound (sfxDidAppear);
                SoundManager.instance.playParralaxEffect(sfxWillAppear);
            }

            if (onShowStartCallback != null)
            {
                onShowStartCallback();
                onShowStartCallback = null;
            }

        }

        protected virtual void OnShowFinished()
        {

            if (!string.IsNullOrEmpty(sfxDidAppear))
            {
                //SoundManager.Instance.PlayEffectSound (sfxDidAppear);
            }

            try
            {
                panelDidShow();
            }
            catch
            {
                Debug.LogError("PanelDidShow Failed " + this.name);
            }

            blocksRaycasts = true;
            //alpha = 1f;
            if (onShowFinishCallback != null)
            {
                onShowFinishCallback();
                onShowFinishCallback = null;
            }

        }

        protected virtual void OnHideStarted()
        {

            blocksRaycasts = false;

            if (!string.IsNullOrEmpty(sfxWillHide))
            {
                //			MasterAudio.PlaySoundAndForget(sfxDidHide);
                //SoundManager.Instance.PlayEffectSound (sfxDidHide);
                SoundManager.instance.playParralaxEffect(sfxWillHide);

            }

            if (onHideStartCallback != null)
            {
                onHideStartCallback();
                onHideStartCallback = null;
            }

        }

        protected virtual void OnHideFinished()
        {

            if (!string.IsNullOrEmpty(sfxDidHide))
            {
                //			MasterAudio.PlaySoundAndForget(sfxDidHide);
                //SoundManager.Instance.PlayEffectSound (sfxDidHide);

            }

            //TODO: Stop blocking user input here
            //TouchBlockerPanel.Unblock();

            this.panel.alpha = 0f;
            this.gameObject.SetActive(false);

            try
            {
                panelDidHide();
            }
            catch
            {
                Debug.LogError("PanelDidHide Failed " + this.name);
            }

            if (onHideFinishCallback != null)
            {
                onHideFinishCallback();
                onHideFinishCallback = null;
            }

        }





    }

}