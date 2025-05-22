using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;
public class GirlAnimationController : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private InGameUIManager inGameUIManager;

    private int lastScoreThreshold = 0;

    void Update()
    {
        int score = inGameUIManager.ReturnScore();
        int currentThreshold = (score / 100) * 100;

        if (score >= 100 && currentThreshold > lastScoreThreshold)
        {
            
            animator.SetTrigger("Jump"); 
            Debug.Log("Score reached: " + currentThreshold);
            lastScoreThreshold = currentThreshold;
        }
    }
}
