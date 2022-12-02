using UnityEngine;

namespace CodeBase.Enemy.PickUpTest
{
    public class ShieldPickUpTest : PickUpTest
    {
        protected override void DoAction()
        {
        Test();
        Test2();
        }


        private void Test()
        {
            Debug.Log("ShieldActivate");
        }
        private void Test2()
        {
            Debug.Log("ShieldActivate2222");
        }
    }
}