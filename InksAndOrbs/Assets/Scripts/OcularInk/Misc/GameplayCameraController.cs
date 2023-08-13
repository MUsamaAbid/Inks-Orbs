using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class GameplayCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineConfiner confiner;

    [SerializeField] private float offsetY;

    private Tween shakeTweenIn, shakeTweenOut;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // RaycastHit hit;
        // var layerMask = LayerMask.GetMask("Terrain");
        // if (Physics.Raycast(transform.position + (Vector3.forward * 5f), Vector3.down, out hit, 1000f, layerMask))
        // {
        //     var body = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        //     body.m_FollowOffset.y = hit.point.y + offsetY;
        // }
    }

    public void Shake(float intensity, float time)
    {
        if (shakeTweenIn is { active: true })
            shakeTweenIn.Kill();

        if (shakeTweenOut is { active: true })
            shakeTweenOut.Kill();

        var mcPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        var ampStart = mcPerlin.m_AmplitudeGain;

        shakeTweenIn = DOTween.To(() => ampStart, x => ampStart = x, intensity, time);

        shakeTweenIn.onUpdate = () => { mcPerlin.m_AmplitudeGain = ampStart; };

        shakeTweenIn.onComplete = () =>
        {
            var ampStart = mcPerlin.m_AmplitudeGain;

            shakeTweenOut = DOTween.To(() => ampStart, x => ampStart = x, 0, 0.2f);

            shakeTweenOut.onUpdate = () => { mcPerlin.m_AmplitudeGain = ampStart; };
        };
    }

    public void SetConfineArea(Collider collider)
    {
        confiner.m_BoundingVolume = collider;
        confiner.m_Damping = 25;
    }

    public void SetFocus(Transform target)
    {
        virtualCamera.Follow = target;
        virtualCamera.LookAt = target;
    }
}