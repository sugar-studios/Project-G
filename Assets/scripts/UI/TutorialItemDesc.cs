using ProjectG.Items;
using UnityEngine;

namespace ProjectG.UI
{
    public class TutorialItemDesc : MonoBehaviour
    {
        public TutorialItemDisplay tTD;
        public Item item;

        public void Highlighted()
        {
            tTD.UpdateDesc(item.name);
        }
        public void Clicked()
        {
            tTD.UpdateDesc(item.description);
            tTD.UpdateModel(item.model);
        }
    }
}
