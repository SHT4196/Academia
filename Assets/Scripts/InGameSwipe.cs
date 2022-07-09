using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSwipe : MonoBehaviour
{

	public GameObject Canvas;
	private float swipeDistance = 50.0f;
	private float startTouchX;              // ��ġ ���� ��ġ
	private float endTouchX;                    // ��ġ ���� ��ġ
	private bool isSwipeMode = false;       // ���� Swipe�� �ǰ� �ִ��� üũ

	private void Update()
	{
		UpdateInput();


	}

	private void UpdateInput()
	{
		// ���� Swipe�� �������̸� ��ġ �Ұ�
		if (isSwipeMode == true) return;

#if UNITY_EDITOR
		// ���콺 ���� ��ư�� ������ �� 1ȸ
		if (Input.GetMouseButtonDown(0))
		{
			// ��ġ ���� ���� (Swipe ���� ����)
			startTouchX = Input.mousePosition.x;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			// ��ġ ���� ���� (Swipe ���� ����)
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
				// ��ġ ���� ���� (Swipe ���� ����)
				startTouchX = touch.position.x;
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				// ��ġ ���� ���� (Swipe ���� ����)
				endTouchX = touch.position.x;

				UpdateSwipe();
			}
		}
#endif
	}

	private void UpdateSwipe()
	{
		// �ʹ� ���� �Ÿ��� �������� ���� Swipe X
		if (Mathf.Abs(startTouchX - endTouchX) < swipeDistance)
		{
			isSwipeMode = false;
		}

		// Swipe ����
		bool isLeft = startTouchX < endTouchX ? true : false;
		bool isRight = startTouchX > endTouchX ? true : false;

		// �̵� ������ ������ ��
		if (isLeft == true)
		{
			Canvas.GetComponent<OptionTrigger>().Option_Btn();
			Debug.Log ("LeftSwipe");
		}

		// �̵� ������ �������� ��
		else if (isRight == true)
		{
			Canvas.GetComponent<OptionTrigger>().Achive_Btn();
			Debug.Log("RightSwipe");
		}
		
	}
}
