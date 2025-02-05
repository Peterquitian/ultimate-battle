using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterMovement : MonoBehaviour
{
    private int _movement;
    [SerializeField] private bool _hasMoved;
    [SerializeField] private int _remainingMovement;
    [SerializeField] private Vector3 _initialPosition;

    [SerializeField] private float _duration = 0.8f;
    [SerializeField] private float _delay = 0.2f;

    private Character _character;

    public bool HasMoved => _hasMoved;
    public int Movement => _movement;
    public int RemainingMovement => _remainingMovement;

    public void DrecreaseMovement(int value)
    {
        _remainingMovement -= value;
    }

    public void MoveToCell(Vector3Int cellPosition, Tilemap tilemap, int cost)
    {

        Debug.Log(tilemap + " MoveToCell");
        // Si ya se movió, no puede mover más
        if (_hasMoved)
        {

            Debug.Log("Este personaje ya no puede moverse este turno.");
            return;
        }

        if (_remainingMovement < cost)
        {
            Debug.Log("No tienes suficientes turnos para este movimiento");
            checkMovement();

            return;
        }

        Vector3 targetPosition = tilemap.GetCellCenterWorld(cellPosition);

        // Activar animación de caminar
        //_character.Animator.SetBool("IsWalking", true);

        _character.Animator.Play("Walk");
        _character.ParticleDust.Play();
        StartCoroutine(MoveWithAnimation(targetPosition, _duration));


        _remainingMovement -= cost;
        checkMovement();
    }

    public void Setup(int maxMovement)
    {
        _character = gameObject.GetComponent<Character>();
        _movement = maxMovement;
        _remainingMovement = _movement;
        _hasMoved = false;
    }

    public void ResetMovement()
    {
        _remainingMovement = _movement;
        _hasMoved = false;
    }
    public void MoveToInitialPosition(Tilemap tilemap)
    {
        Vector3 cellCenter = tilemap.GetCellCenterWorld(tilemap.WorldToCell(_initialPosition));
        transform.position = cellCenter;
    }

    public void Attack(int cost)
    {
        _remainingMovement = Mathf.Clamp(_remainingMovement - cost, 0, Movement);

        checkMovement();
    }

    public void Attack()
    {
        _remainingMovement = Mathf.Clamp(_remainingMovement - 1, 0, Movement);

        checkMovement();
    }

    public void checkMovement()
    {
        _character.UpdateMovementUI();

        if (_remainingMovement > 0)
        {
            _hasMoved = false;
            return;
        }

        _hasMoved = true;
        Debug.Log($"{gameObject.name} ha terminado sus movimiento.");
    }

    public IEnumerator MoveWithAnimation(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        //yield return new WaitForSeconds(_delay); // Pequeño retraso antes de mover.
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Desactivar animación de caminar.
        _character.Animator.Play("Idle");

        transform.position = targetPosition;
    }

    public IEnumerator AttackAnimation(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        //yield return new WaitForSeconds(_delay); // Pequeño retraso antes de mover.
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Desactivar animación de caminar.
        _character.Animator.Play("Idle");

        transform.position = targetPosition;
    }
}

