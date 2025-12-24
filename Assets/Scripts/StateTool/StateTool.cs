using System;
using PurrNet;
using UnityEngine;

namespace Assets.Scripts.StateTool
{
    internal abstract class StateTool : NetworkIdentity
    {
        [SerializeField] public Transform parentTranform;

    }
}
