namespace Modules.Content.Characters.Employer.Model
{
    public class ModelEmployer
    {
        private float _immitationTime;
        private bool _isBusy = false;
        
        public bool IsBusy => _isBusy;
        public float ImmitationTime => _immitationTime;

        public ModelEmployer(float immitationTime)
        {
            _immitationTime = immitationTime;
        }
        
        public void SetIsBusyStatus(bool newStatus) => _isBusy = newStatus;

        public void SetImmitationTime(float newTime)
        {
            _immitationTime = newTime;
        }
    }
}