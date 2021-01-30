using UnityEngine;
using UnityEngine.UI;
namespace be.nocomputer.ncUnityUtils
{
    public class ScrubAnimation : MonoBehaviour
    {
        public Slider scrubSlider;
        public Toggle playButton;
        public Button firstFrameButton;
        public Button lastFrameButton;
        public string stateName;

        private Animator animator;

        private bool isPlaying = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
            UpdatePlaying(false);
        }

        private void OnEnable()
        {
            playButton.onValueChanged.AddListener(OnPlayClick);
            firstFrameButton.onClick.AddListener(OnFirstFrameClick);
            lastFrameButton.onClick.AddListener(OnLastFrameClick);
        }

        private void OnDisable()
        {
            playButton.onValueChanged.RemoveListener(OnPlayClick);
            firstFrameButton.onClick.RemoveListener(OnFirstFrameClick);
            lastFrameButton.onClick.RemoveListener(OnLastFrameClick);
        }

        private void OnPlayClick(bool value)
        {
            UpdatePlaying(value);
        }

        private void UpdatePlaying(bool value)
        {
            isPlaying = value;

            if (isPlaying)
            {
                animator.speed = 1f;
            }
            else
            {
                animator.speed = 0f;
            }
        }

        private void OnFirstFrameClick()
        {
            Skip(0);
        }

        private void OnLastFrameClick()
        {
            Skip(1);
        }

        private void Skip(float normTime)
        {
            UpdatePlaying(false);
            scrubSlider.value = normTime;
        }

        private void Update()
        {
            if (isPlaying)
            {
                float time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                scrubSlider.value = time - Mathf.Floor(time);
            }
            else
            {
                animator.Play(stateName, 0, scrubSlider.value);
            }
        }
    }
}
