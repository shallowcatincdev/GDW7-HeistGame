using UnityEngine;

public class TwoLegStepController : MonoBehaviour
{
    [SerializeField] private footStepController leftFoot;
    [SerializeField] private footStepController rightFoot;

    private bool preferLeftFoot = true;

    private void Update()
    {
        if (leftFoot == null || rightFoot == null) return;

        if (leftFoot.isMoving || rightFoot.isMoving) return;

        bool leftFootWantsToStep = leftFoot.wantToStep;
        bool rightFootWantsToStep = rightFoot.wantToStep;

        if (!leftFootWantsToStep && !rightFootWantsToStep) return;
        
        if (leftFootWantsToStep && rightFootWantsToStep)
        {
            if (preferLeftFoot)
            {
                leftFoot.TryStep();
            }
            else
            {
                rightFoot.TryStep();
            }
            preferLeftFoot = !preferLeftFoot;
            return;
        }

        if(leftFootWantsToStep)
        {
            leftFoot.TryStep();
            preferLeftFoot = false;
        }
        else
        {
            rightFoot.TryStep();
            preferLeftFoot = true;
        
        }
        
    }
}
