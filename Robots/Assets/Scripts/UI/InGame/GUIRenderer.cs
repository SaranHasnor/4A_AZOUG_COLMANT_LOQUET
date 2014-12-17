using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIRenderer : MonoBehaviour
{
	private RunnableEntity _selectedEntity;
	private int _selectedActionIndex;

	// Temporary hard-coded list before a cleaner and more generic system is implemented
	private static System.Type[] _actionTypes = {
												   typeof(EntityActionMove)
											   };

	private Vector2 _timelineScrollPos;

	void Start()
	{
		GameData.guiRenderer = this;
		_selectedActionIndex = -1;
	}

	void OnGUI()
	{
		if (_selectedEntity != null)
		{
			DrawActionTimeLine(_selectedEntity.actions, new Rect(0.0f, 0.7f * Screen.height, Screen.width, 0.3f * Screen.height));
			if (_selectedActionIndex != -1)
			{
				DrawActionList(new Rect());
			}
		}
	}

	private void DrawActionList(Rect rect)
	{
		foreach (System.Type type in _actionTypes)
		{
			
		}
	}

	private void DrawActionTimeLine(List<EntityAction> timeline, Rect rect)
	{
		float itemWidth = 70.0f;

		GUI.Box(rect, GUIContent.none);

		GUI.BeginGroup(rect);
		
		float scrollAreaWidth = Mathf.Min(itemWidth * timeline.Count, rect.width);
		_timelineScrollPos = GUI.BeginScrollView(new Rect(0.0f, 0.0f, rect.width, rect.height), _timelineScrollPos, new Rect(0.0f, 0.0f, scrollAreaWidth, rect.height), false, true);
		
		int cursor = 0;
		foreach (EntityAction action in timeline)
		{
			DrawAction(action, new Rect(cursor * itemWidth, 0.0f, itemWidth, rect.height), cursor);
			cursor++;
		}
		DrawAction(null, new Rect(cursor * itemWidth, 0.0f, itemWidth, rect.height), cursor);

		GUI.EndScrollView(false);

		GUI.EndGroup();
	}

	private void DrawAction(EntityAction action, Rect rect, int index)
	{
		if (GUI.Button(rect, action != null ? action.GetType().ToString() : null))
		{
			_selectedActionIndex = index;
		}
	}

	public void SelectEntity(RunnableEntity entity)
	{
		_selectedEntity = entity;
	}
}
