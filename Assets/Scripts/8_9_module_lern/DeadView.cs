using UnityEngine;

public class DeadView: MonoBehaviour
{
    [SerializeField] private ParticleSystem _deadEffectPrefab;

    private IDiedNotifier _diedNotifier;

    private void Start()
    {
        _diedNotifier = GetComponentInParent<IDiedNotifier>();

        _diedNotifier.Died += OnDead;
    }

    private void OnDestroy()
    {
        _diedNotifier.Died -= OnDead;
    }

    private void OnDead()
    {
        Instantiate(_deadEffectPrefab, transform.position, Quaternion.identity, null);
    }
}