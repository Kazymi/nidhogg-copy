
    public interface IShieldSystem
    {
        public void OpenShield();
        public void CloseShield();
        public InputAction ShieldCrash { get; set; }
        public bool IsShieldActivated { get; }

    }
