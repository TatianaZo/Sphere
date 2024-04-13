using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text killCount;
    private int count;

    private void Start()
    {
        count = 0;
        killCount.text = $"killed: 0";
    }

    public void AddCounter()
    {
        count++;
        killCount.text = $"killed: {count.ToString()}";
    }
}
