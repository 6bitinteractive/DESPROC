using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sorting
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]

    [System.Serializable] public class OnDropToBin : UnityEvent<GameObject> { }
    [System.Serializable] public class OnVerify : UnityEvent<GameObject, RecycleCode> { }

    public class SortingBin : MonoBehaviour
    {
        [SerializeField] RecycleCode recycleCode;

        [HideInInspector] public OnDropToBin OnDropToBin = new OnDropToBin();
        [HideInInspector] public OnVerify OnVerify = new OnVerify();

        private void Start()
        {
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnEnable()
        {
            OnDropToBin.AddListener(Verify);
        }

        private void OnDisable()
        {
            OnDropToBin.RemoveListener(Verify);
        }

        private void Verify(GameObject obj)
        {
            OnVerify.Invoke(obj, recycleCode);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // TODO: When a plastic is hovering, add visual feedback (ex. scale up or add glowing outline)
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // TODO: Undo visual feedback
        }
    }
}