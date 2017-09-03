using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Connector;
/// <summary>
/// 负责元素拾起，连接，高亮等功能的组合
/// </summary>
public class ConnectorBehaivor : MonoBehaviour
{
    public ConnectorCtrl connectCtrl;
    private void Start()
    {
        connectCtrl.Start();
    }
    private void Update()
    {
        connectCtrl.Update();
    }

}
