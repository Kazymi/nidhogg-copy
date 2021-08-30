
    using System;
    using UnityEngine;

    public interface IPlayerMovement
    {
        public Action<bool> DefaultMovement { get; set; }
        public Rigidbody Rigidbody { get; }
    }
