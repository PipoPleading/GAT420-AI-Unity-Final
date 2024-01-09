using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiPerception : MonoBehaviour
{
    [SerializeField] string tagName = "";
    [SerializeField] float distance = 1;
    [SerializeField] float maxAngle = 45;

    public string TagName { get { return tagName; } }
    public float Distance { get { return Distance; } }
    public float MaxAngle { get { return MaxAngle; } }

    public abstract GameObject[] GetGameObjects();

}
