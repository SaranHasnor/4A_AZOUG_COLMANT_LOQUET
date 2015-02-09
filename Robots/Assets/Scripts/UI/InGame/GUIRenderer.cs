using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIRenderer : MonoBehaviour
{
	private MapEntity _selectedEntity;

	private ActionQueue _selectedQueue
	{
		get
		{
			return (_selectedEntity != null) ? GameData.currentState.actions[_selectedEntity.id] : null;
		}
	}

	private int _selectedActionIndex;

	// Temporary hard-coded list before a cleaner and more generic system is implemented
	/*private static System.Type[] _actionTypes = {
												   typeof(EntityActionMove)
											   };*/

	private Vector2 _timelineScrollPos;

	void Start()
	{
		GameData.guiRenderer = this;
		_selectedActionIndex = -1;
	}

	void OnGUI()
	{

		if (GameData.gameMaster.isVictory()) {
			DrawVictory();
		} else if (GameData.gameMaster.isLose()) {
			DrawLose();
		}
		else
		{
			if (_selectedEntity != null)
			{
				DrawActionTimeLine(_selectedQueue.actions, new Rect(0.0f, 0.7f*Screen.height, Screen.width, 0.3f*Screen.height));
				if (_selectedActionIndex != -1)
				{
					DrawActionList(new Rect());
				}
			}
			if (GUI.Button(
				new Rect(Screen.width - 0.2f*Screen.width, 0.01f*Screen.height, 0.2f*Screen.width, 0.2f*Screen.height), "Play/Pause"))
			{
				GameData.timeMaster.ToggleRun();
			}
			if (GUI.Button(
				new Rect(Screen.width - 0.2f*Screen.width, 0.21f*Screen.height, 0.2f*Screen.width, 0.2f*Screen.height), "Rewind"))
			{

			}
		}
		DrawTurn();
	}

	private void DrawActionList(Rect rect)
	{
		/*foreach (System.Type type in _actionTypes)
		{
			
		}*/
	}

	private void DrawActionTimeLine(List<EntityAction> timeline, Rect rect)
	{
		float itemWidth = 70.0f;

		GUI.Box(rect, GUIContent.none);

		GUI.BeginGroup(rect);

		float scrollAreaWidth = Mathf.Max(itemWidth * (timeline.Count + 1), rect.width);
		_timelineScrollPos = GUI.BeginScrollView(new Rect(0.0f, 0.0f, rect.width, rect.height), _timelineScrollPos, new Rect(0.0f, 0.0f, scrollAreaWidth, rect.height - 20.0f), true, false);

		int cursor = 0;
		foreach (EntityAction action in timeline)
		{
			DrawAction(action, new Rect(cursor * itemWidth, 0.0f, itemWidth, rect.height - 20.0f), cursor);
			cursor++;
		}
		DrawAction(null, new Rect(cursor * itemWidth, 0.0f, itemWidth, rect.height - 20.0f), cursor);

		GUI.EndScrollView(false);

		GUI.EndGroup();
	}

	private void DrawAction(EntityAction action, Rect rect, int index)
	{
		if (GUI.Button(rect, action != null ? action.GetType().ToString() : null))
		{
			//_selectedActionIndex = index;
			if (action == null)
			{ // Temporary
				_selectedQueue.SetAction(new EntityActionMove(_selectedEntity.id, new MapPosition(0, 0, 0)));
			}
		}
	}

	private void DrawVictory()
	{
		GUI.Box(
			new Rect(0.25f*Screen.width, 0.25f*Screen.height, 0.5f*Screen.width, 0.5f*Screen.height),
			"WINNER");
	}

	private void DrawLose() {
		GUI.Box(
			new Rect(0.25f * Screen.width, 0.25f * Screen.height, 0.5f * Screen.width, 0.5f * Screen.height),
			"LOSER");
	}

	private void DrawTurn() {
		if (GameData.gameMaster.GetMaxTurn() == -1)
		{
			GUI.Label(
				new Rect(0.49f * Screen.width, 0.01f * Screen.height, 0.02f * Screen.width, 0.05f * Screen.height),
				GameData.timeMaster.GetTurn().ToString()
				);
		}
		else
		{
			GUI.Label(
				new Rect(0.45f * Screen.width, 0.01f * Screen.height, 0.05f * Screen.width, 0.05f * Screen.height),
				GameData.timeMaster.GetTurn() + "/" + GameData.gameMaster.GetMaxTurn()
				);
		}
	}

	public void SelectEntity(MapEntity entity)
	{
		_selectedEntity = entity;
	}
}
