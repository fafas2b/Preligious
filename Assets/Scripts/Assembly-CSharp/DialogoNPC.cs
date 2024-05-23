using System;
using UnityEngine;

[Serializable]
public class DialogoNPC
{
	public string nombreNPC;

	[TextArea(3, 10)]
	public string[] sentences;
}
