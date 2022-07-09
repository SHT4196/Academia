using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainSwipeUI : MonoBehaviour
{
	[SerializeField]
	private Scrollbar scrollBar;                    // Scrollbar�� ��ġ�� �������� ���� ������ �˻�
	private float swipeTime = 0.2f;         // �������� Swipe �Ǵ� �ð�
	[SerializeField]
	private float swipeDistance = 50.0f;        // �������� Swipe�Ǳ� ���� �������� �ϴ� �ּ� �Ÿ�

	private float[] scrollPageValues;           // �� �������� ��ġ �� [0.0 - 1.0]
	private float valueDistance = 0;            // �� ������ ������ �Ÿ�
	private int currentPage = 1;            // ���� ������
	private int maxPage = 0;                // �ִ� ������
	private float startTouchX;              // ��ġ ���� ��ġ
	private float endTouchX;                    // ��ġ ���� ��ġ
	private bool isSwipeMode = false;       // ���� Swipe�� �ǰ� �ִ��� üũ


	private void Awake()
	{
		// ��ũ�� �Ǵ� �������� �� value ���� �����ϴ� �迭 �޸� �Ҵ�
		scrollPageValues = new float[transform.childCount];

		// ��ũ�� �Ǵ� ������ ������ �Ÿ�
		valueDistance = 1f / (scrollPageValues.Length - 1f);

		// ��ũ�� �Ǵ� �������� �� value ��ġ ���� [0 <= value <= 1]
		for (int i = 0; i < scrollPageValues.Length; ++i)
		{
			scrollPageValues[i] = valueDistance * i;
		}

		// �ִ� �������� ��
		maxPage = transform.childCount;
	}

	private void Start()
	{
		// ���� ������ �� 1�� �������� �� �� �ֵ��� ����
		SetScrollBarValue(1);
	}

	public void SetScrollBarValue(int index)
	{
		currentPage = index;
		scrollBar.value = scrollPageValues[index];
	}

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
			// ���� �������� Swipe�ؼ� ���ư���
			StartCoroutine(OnSwipeOneStep(currentPage));
			return;
		}

		// Swipe ����
		bool isLeft = startTouchX < endTouchX ? true : false;

		// �̵� ������ ������ ��
		if (isLeft == true)
		{
			// ���� �������� ���� ���̸� ����
			if (currentPage == 0) return;

			// �������� �̵��� ���� ���� �������� 1 ����
			currentPage--;
		}
		// �̵� ������ �������� ��
		else
		{
			// ���� �������� ������ ���̸� ����
			if (currentPage == maxPage - 1) return;

			// ���������� �̵��� ���� ���� �������� 1 ����
			currentPage++;
		}

		// currentIndex��° �������� Swipe�ؼ� �̵�
		StartCoroutine(OnSwipeOneStep(currentPage));
	}

	/// <summary>
	/// �������� �� �� ������ �ѱ�� Swipe ȿ�� ���
	/// </summary>
	private IEnumerator OnSwipeOneStep(int index)
	{
		float start = scrollBar.value;
		float current = 0;
		float percent = 0;

		isSwipeMode = true;

		while (percent < 1)
		{
			current += Time.deltaTime;
			percent = current / swipeTime;

			scrollBar.value = Mathf.Lerp(start, scrollPageValues[index], percent);

			yield return null;
		}

		isSwipeMode = false;
	}


}




