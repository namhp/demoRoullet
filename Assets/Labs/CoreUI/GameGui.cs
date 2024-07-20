using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lab.Core.UI;

public class GameGui : PanelStack {
    
	static GameGui _instance;
	public static GameGui instance {
		get {
			return _instance;
		}
	}

	public List<PanelController> panels;

	public PanelController lastPanel;

	public PanelController defaultPanel;

	#region Monobehavior

	void Awake()
	{
		_instance = this;
		DontDestroyOnLoad(gameObject);
		InputDeviceIDPanel.CheckReset();
	}

	IEnumerator Start()
	{
        if (InputDeviceIDPanel.HasLocked)
        {
			defaultPanel = panels[1];
		}
		yield return new WaitForSeconds(0f);

		if ( defaultPanel != null ) {
			pushPanel(defaultPanel);
		}

	}

	public virtual void pushPanel(string panelName)
	{
		PanelController panel = GetPanel (panelName);		
		lastPanel = currentPanel;
		//Debug.LogError ("LAST PANEL : "+lastPanel.name);
		pushPanel(panel);
	}

	public PanelController GetPanel(string panelName) 
	{
		return panels.Where(s => s).FirstOrDefault(s => s.name == panelName);
	}


	public void PushPanelOnTop(string panelName) {
		PanelController panel = GetPanel(panelName);
		pushPanelOnTop(panel);
	}


	public virtual void PopPanel(string panelName){
		PanelController panel = GetPanel (panelName);
		PopPanel (panel);
	}

	#endregion

}