using UnityEngine;
using System.Collections;

public interface ICajaOperacionesBehaviourScript
{
    void AcomodarNumeroEnCaja(bool isCompleto);
    void Acomodar(Collider2D hit);
}
