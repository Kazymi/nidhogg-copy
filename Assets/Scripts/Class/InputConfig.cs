
    using System;
    using UnityEngine;

    [Serializable]
    public class InputConfig
    {
        [SerializeField] private float clickThreshld;

        // TODO: *Threshold*
        public float ClickThreshld => clickThreshld;
    }
