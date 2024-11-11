namespace Better_FPS_Controller.Scripts.Weapons
{
    public abstract class WeaponBase
    {
        public bool IsMelee { get; set; }
        public bool IsFullAuto = false;
        public bool IsInfiniteAmmo = false;
        public bool IsInitialFullMag = true;
        public int MagSize { get; set; }
        public int AmmoLeft { get; set; }
        public float FireDelaySeconds { get; set; }
        public float ReloadDelaySeconds { get; set; }
        private bool _isWaitingForFireDelay = false;
        private bool _isWaitingForReloadDelay = false;

        public void ReloadWeapon()
        {
            // TODO: Add reload delay
        }

        public void UseWeapon()
        {
            // TODO: Add fire delay
        }
    }
}