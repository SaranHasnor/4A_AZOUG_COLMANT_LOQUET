﻿using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

public class GUIRenderer : MonoBehaviour {

	#region General
	[SerializeField]
	private GameObject _ButtonPlayPause;
	[SerializeField]
	private GameObject _ButtonReplay;
	#endregion General

	#region Action
	[SerializeField]
	private GameObject _ButtonWait;


	[SerializeField]
	private GameObject _ButtonMove;
	[SerializeField]
	private GameObject _ButtonMoveUp;
	[SerializeField]
	private GameObject _ButtonMoveDown;
	[SerializeField]
	private GameObject _ButtonMoveLeft;
	[SerializeField]
	private GameObject _ButtonMoveRight;

	private bool _isMoveDraw = false;

	[SerializeField]
	private GameObject _ButtonPush;
	[SerializeField]
	private GameObject _ButtonPushUp;
	[SerializeField]
	private GameObject _ButtonPushDown;
	[SerializeField]
	private GameObject _ButtonPushLeft;
	[SerializeField]
	private GameObject _ButtonPushRight;

	private bool _isPushDraw = false;

	[SerializeField]
	private GameObject _ButtonQueueDelete;
	[SerializeField]
	private GameObject _ButtonQueueBefore;
	[SerializeField]
	private GameObject _ButtonQueueAfter;
	#endregion Action

	private MapEntity _selectedEntity;

	private ActionQueue _selectedQueue {
		get {
			return (_selectedEntity != null) ? GameData.currentState.actions[_selectedEntity.id] : null;
		}
	}

	private int _selectedActionIndex;

	// Temporary hard-coded list before a cleaner and more generic system is implemented
	/*private static System.Type[] _actionTypes = {
												   typeof(EntityActionMove)
											   };*/

	private Vector2 _timelineScrollPos;

	void Start() {
		GameData.guiRenderer = this;
		_selectedActionIndex = -1;
	}

	void OnGUI() {
		if (GameData.gameMaster.isVictory()) {
			DrawVictory();
		} else if (GameData.gameMaster.isLose()) {
			DrawLose();
		} else {
			if (_selectedEntity != null) {
				_ButtonMove.SetActive(true);
				_ButtonPush.SetActive(true);
				_ButtonWait.SetActive(true);

				DrawActionTimeLine(_selectedQueue.actions, new Rect(0.0f, 0.7f * Screen.height, Screen.width, 0.3f * Screen.height));
				if (_selectedActionIndex != -1) {
					DrawActionList(new Rect());
				}
			}
		}
		DrawTurn();
	}

	private void DrawActionList(Rect rect) {
		/*foreach (System.Type type in _actionTypes)
		{
			
		}*/
	}

	private void DrawActionTimeLine(List<EntityAction> timeline, Rect rect) {
		float itemWidth = 70.0f;

		GUI.Box(rect, GUIContent.none);

		GUI.BeginGroup(rect);

		float scrollAreaWidth = Mathf.Max(itemWidth * (timeline.Count + 1), rect.width);
		_timelineScrollPos = GUI.BeginScrollView(new Rect(0.0f, 0.0f, rect.width, rect.height), _timelineScrollPos, new Rect(0.0f, 0.0f, scrollAreaWidth, rect.height - 20.0f), true, false);

		int cursor = 0;
		foreach (EntityAction action in timeline) {
			DrawAction(action, new Rect(cursor * itemWidth, 0.0f, itemWidth, rect.height - 20.0f), cursor);
			cursor++;
		}
		DrawAction(null, new Rect(cursor * itemWidth, 0.0f, itemWidth, rect.height - 20.0f), cursor);

		GUI.EndScrollView(false);

		GUI.EndGroup();
	}

	private void DrawAction(EntityAction action, Rect rect, int index) {
		if (GUI.Button(rect, action != null ? action.GetType().ToString() : null)) {
			//_selectedActionIndex = index;
			if (action == null) { // Temporary
				_selectedQueue.SetAction(new EntityActionMove(_selectedEntity.id, new MapPosition(0, 0, 0)));
			}
		}
	}

	private void DrawVictory() {
		GUI.Box(
			new Rect(0.25f * Screen.width, 0.25f * Screen.height, 0.5f * Screen.width, 0.5f * Screen.height),
			"WINNER");
	}

	private void DrawLose() {
		GUI.Box(
			new Rect(0.25f * Screen.width, 0.25f * Screen.height, 0.5f * Screen.width, 0.5f * Screen.height),
			"LOSER");
	}

	private void DrawTurn() {
		if (GameData.gameMaster.GetMaxTurn() == -1) {
			GUI.Label(
				new Rect(0.49f * Screen.width, 0.01f * Screen.height, 0.02f * Screen.width, 0.05f * Screen.height),
				GameData.timeMaster.GetTurn().ToString()
				);
		} else {
			GUI.Label(
				new Rect(0.45f * Screen.width, 0.01f * Screen.height, 0.05f * Screen.width, 0.05f * Screen.height),
				GameData.timeMaster.GetTurn() + "/" + GameData.gameMaster.GetMaxTurn()
				);
		}
	}

	public void SelectEntity(MapEntity entity) {
		EraseControllActionButton();
		_selectedEntity = entity;
	}


	public void OnPlayPause() {
		GameData.timeMaster.ToggleRun();
	}

	public void OnReplay() {
		EraseAllActionButton();
		_selectedEntity = null;
	}


	public void OnMove() {
		EraseControllActionButton();
		if (!_isMoveDraw) {
			_ButtonMoveUp.SetActive(true);
			_ButtonMoveDown.SetActive(true);
			_ButtonMoveLeft.SetActive(true);
			_ButtonMoveRight.SetActive(true);
		}
	}

	public void OnPush() {
		EraseControllActionButton();
		if (!_isPushDraw) {
			_ButtonPushUp.SetActive(true);
			_ButtonPushDown.SetActive(true);
			_ButtonPushLeft.SetActive(true);
			_ButtonPushRight.SetActive(true);
		}
	}

	public void OnWait() {
		EraseControllActionButton();
	}

	public void OnMoveUp() {
		EraseControllActionButton();
	}

	public void OnMoveDown() {
		EraseControllActionButton();
	}

	public void OnMoveLeft() {
		EraseControllActionButton();
	}

	public void OnMoveRight() {
		EraseControllActionButton();
	}


	public void OnPushUp() {
		EraseControllActionButton();
	}

	public void OnPushDown() {
		EraseControllActionButton();
	}

	public void OnPushLeft() {
		EraseControllActionButton();
	}

	public void OnPushRight() {
		EraseControllActionButton();
	}


	public void OnQueueDelete() {
		EraseControllActionButton();
	}

	public void OnQueueBefore() {
		EraseControllActionButton();
	}

	public void OnQueueAfter() {
		EraseControllActionButton();
	}

	private void EraseAllActionButton()
	{
		EraseControllActionButton();
		_ButtonMove.SetActive(false);
		_ButtonPush.SetActive(false);
		_ButtonWait.SetActive(false);
	}

	private void EraseControllActionButton() {
		_ButtonMoveUp.SetActive(false);
		_ButtonMoveDown.SetActive(false);
		_ButtonMoveLeft.SetActive(false);
		_ButtonMoveRight.SetActive(false);

		_ButtonPushUp.SetActive(false);
		_ButtonPushDown.SetActive(false);
		_ButtonPushLeft.SetActive(false);
		_ButtonPushRight.SetActive(false);

		_ButtonQueueBefore.SetActive(false);
		_ButtonQueueAfter.SetActive(false);
		_ButtonQueueDelete.SetActive(false);
	}
}
