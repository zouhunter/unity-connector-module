using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
namespace Connector
{
    [Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }

    /// <summary>
    /// 负责元素拾起，连接，高亮等功能的组合
    /// </summary>
    public class ConnectorBehaivor : MonoBehaviour
    {
        [Range(0.1f, 1f)]
        public float nodeUpdateSpanTime = 0.5f;
        [Range(0.02f, 0.1f)]
        public float pickUpSpantime = 0.02f;
        [Range(10, 60)]
        public int scrollSpeed = 20;
        [Range(0, 1)]
        public float sphereRange = 0.1f;
        [Range(3, 15)]
        public float distence = 1f;

        public GameObjectEvent onConnect;
        public GameObjectEvent onPickDown;
        public GameObjectEvent onPickUp;
        public GameObjectEvent onMatch;
        public GameObjectEvent onDisMatch;

        private IPickUpController pickCtrl;
        private INodeConnectController nodeConnectCtrl;
        void Start()
        {
            pickCtrl = new PickUpController(pickUpSpantime, distence, scrollSpeed);
            nodeConnectCtrl = new NodeConnectController(sphereRange, nodeUpdateSpanTime);
            pickCtrl.onPickUp += OnPickUp;
            pickCtrl.onPickDown += OnPickDown;
            pickCtrl.onPickStatu += OnPickStatu;
            nodeConnectCtrl.onDisMatch += OnDisMath;
            nodeConnectCtrl.onMatch += OnDisMath;
        }

        void Update()
        {
            if (UnityEngine.EventSystems.EventSystem.current != null && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
            pickCtrl.Update();
            nodeConnectCtrl.Update();
        }

        void OnMatch(INodeItem[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                onMatch.Invoke(items[i].Render);
            }
        }
        void OnDisMath(INodeItem[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                onDisMatch.Invoke(items[i].Render);
            }
        }
        void OnPickUp(GameObject obj)
        {
            nodeConnectCtrl.SetActiveItem(obj.GetComponent<INodeParent>());
            onPickUp.Invoke(obj);
        }

        void OnPickDown(GameObject obj)
        {
            nodeConnectCtrl.SetDisableItem(obj.GetComponent<INodeParent>());
            onPickDown.Invoke(obj);
        }
        void OnConnected(INodeItem[] nodes)
        {
            foreach (var item in nodes){
                onConnect.Invoke(item.Render);
            }
        }
        void OnPickStatu(GameObject go)
        {
            nodeConnectCtrl.TryConnect();
        }

    
        
    }
}