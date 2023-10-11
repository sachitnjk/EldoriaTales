using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractions : MonoBehaviour
{
	public void ReturnFromConversation()
	{
		GameManager.Instance.isInteracting = false;
	}
}
