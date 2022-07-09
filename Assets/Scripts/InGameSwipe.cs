using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSwipe : MonoBehaviour
{

	public GameObject Canvas;
	private float swipeDistance = 50.0f;
	private float startTouchX;              // 터치 시작 위치
	private float endTouchX;                    // 터치 종료 위치
	private bool isSwipeMode = false;       // 현재 Swipe가 되고 있는지 체크

	private void Update()
	{
		UpdateInput();


	}

	private void UpdateInput()
	{
		// 현재 Swipe를 진행중이면 터치 불가
		if (isSwipeMode == true) return;

#if UNITY_EDITOR
		// 마우스 왼쪽 버튼을 눌렀을 때 1회
		if (Input.GetMouseButtonDown(0))
		{
			// 터치 시작 지점 (Swipe 방향 구분)
			startTouchX = Input.mousePosition.x;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			// 터치 종료 지점 (Swipe 방향 구분)
			endTouchX = Input.mousePosition.x;

			UpdateSwipe();
		}
#endif

#if UNITY_ANDROID
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				// 터치 시작 지점 (Swipe 방향 구분)
				startTouchX = touch.position.x;
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				// 터치 종료 지점 (Swipe 방향 구분)
				endTouchX = touch.position.x;

				UpdateSwipe();
			}
		}
#endif
	}

	private void UpdateSwipe()
	{
		// 너무 작은 거리를 움직였을 때는 Swipe X
		if (Mathf.Abs(startTouchX - endTouchX) < swipeDistance)
		{
			isSwipeMode = false;
		}

		// Swipe 방향
		bool isLeft = startTouchX < endTouchX ? true : false;
		bool isRight = startTouchX > endTouchX ? true : false;

		// 이동 방향이 왼쪽일 때
		if (isLeft == true)
		{
			Canvas.GetComponent<OptionTrigger>().Option_Btn();
			Debug.Log ("LeftSwipe");
		}

		// 이동 방향이 오른쪽일 떄
		else if (isRight == true)
		{
			Canvas.GetComponent<OptionTrigger>().Achive_Btn();
			Debug.Log("RightSwipe");
		}
		
	}
}
