using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


public class LockInspectorParameter : ScriptableSingleton<LockInspectorParameter>
{
    public List<EditorWindow> windowList = new List<EditorWindow>();
}

public class LockInspectpr
{
    [MenuItem("Window/LockInspector/Tab %l")]
    [System.Obsolete]
    public static void ShowInspector()
    {
		Object target = Selection.activeObject;
		var currentInspector = EditorWindow.focusedWindow;
		if (target == null)	return;

		var inspectorType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");
		var inspectorInstance = ScriptableObject.CreateInstance(inspectorType) as EditorWindow;
		inspectorInstance.title = Selection.activeObject.name;
		inspectorInstance.Show();

		var isLocked = inspectorType.GetProperty("isLocked", BindingFlags.Instance | BindingFlags.Public);
		isLocked.GetSetMethod().Invoke(inspectorInstance, new object[] { true });

		LockInspectorParameter.instance.windowList.Add(inspectorInstance);

		//currentInspector.Focus();
	}

	[MenuItem("Window/LockInspector/Close Last Tab %#l")]
	public static void CloseLastWindow()
	{
		var instance = LockInspectorParameter.instance;
		if (instance.windowList.Count == 0)
			return;

		var last = instance.windowList[instance.windowList.Count - 1];
		instance.windowList.RemoveAt(instance.windowList.Count - 1);
		last.Close();
	}

	[MenuItem("Window/LockInspector/Close All Window")]
	public static void CloseAllWindows()
	{
		var instance = LockInspectorParameter.instance;
		foreach (var window in instance.windowList)
		{
			if (window != null)
				window.Close();
		}
		instance.windowList.Clear();
	}
}
