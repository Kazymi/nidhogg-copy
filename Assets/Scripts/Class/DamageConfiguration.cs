
    public class DamageConfiguration
    {
        public VFXConfiguration VFXConfiguration;
        public float Damage;
        public bool Bleeding;

        public DamageConfiguration(VFXConfiguration vfxConfiguration, float damage, bool bleeding)
        {
            VFXConfiguration = vfxConfiguration;
            Damage = damage;
            Bleeding = bleeding;
        }
    }
