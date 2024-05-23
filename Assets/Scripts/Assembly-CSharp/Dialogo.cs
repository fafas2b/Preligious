using System;
using UnityEngine;

[Serializable]
public class Dialogo
{
	public string Lugar;

	public bool leido;

	[TextArea(3, 10)]
	public string[] sentences;
}
