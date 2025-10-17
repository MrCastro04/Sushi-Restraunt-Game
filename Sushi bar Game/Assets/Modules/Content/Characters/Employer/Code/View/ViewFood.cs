using TMPro;
using UnityEngine;

namespace Modules.Content.Characters.Employer.Code.View
{
    public class ViewFood : MonoBehaviour
    {
        [SerializeField] private TMP_Text _profitText;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void DisplayProfitText(int newProfitText)
        {
            _profitText.text = newProfitText.ToString();
        }
    }
}