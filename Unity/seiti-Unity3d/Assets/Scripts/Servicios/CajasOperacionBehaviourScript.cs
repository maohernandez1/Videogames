using UnityEngine;
using System.Collections;


public class CajasOperacionBehaviourScript : MonoBehaviour, ICajaOperacionesBehaviourScript
{
    public bool isOcupada { get; set; }
    public int valor { get; set; }
    Vector2 fwd;
    //RaycastHit2D hit;
    NumeroScript script;
    public bool comienzo { get; set; }

    // Use this for initialization
    void awake()
    {
        comienzo = false;
    }
    void Start()
    {
        StartCoroutine("IniciarMetodo");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (comienzo)
        {
            AcomodarNumeroEnCaja(true);
        }
    }


    public void AcomodarNumeroEnCaja(bool isCompleto)
    {
        fwd = transform.TransformDirection(Vector2.zero);
        //hit = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y), 1.5f, fwd); 
        Collider2D elemento = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), 1f);
        //script = elemento != null ? (NumeroScript)elemento.GetComponent<NumeroScript>() : new NumeroScript();
        if (elemento != null)
        {
            print("Elemento dentro " + elemento.name);
            script = (NumeroScript)elemento.GetComponent<NumeroScript>();
            script.cajaOperacionesScript = this;
            if (isCompleto)
            {
                if ((this.transform.position.x + 1.6f > elemento.transform.position.x) && (this.transform.position.x < elemento.transform.position.x + 1.6f)
                        && (this.transform.position.y + 1.6f > elemento.transform.position.y) && (this.transform.position.y < elemento.transform.position.y + 1.6f))
                {
                    if (!isOcupada)
                    {
                        Acomodar(elemento);
                        script.esTocado = false;
                        script.esEnPosicion = true;
                        isOcupada = true;
                        script.GetComponent<AudioSource>().Play();
                        valor = int.Parse(elemento.name.Split('_')[0]);
                    }
                }
                else
                {
                    //script.esEnPosicion = false;
                    
                    valor = -99999;
                }
            }
            else
            {
                Acomodar(elemento);
            }
        }
        else
        {
            isOcupada = false;
            //if(script is NumeroScript)
            //{
            //    print(script.name);
            //    script.esEnPosicion = false;
            //}
            valor = -99999;
        }
        //print(this.name + "----" + valor);
    }

    public void Acomodar(Collider2D hit)
    {
        hit.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, hit.transform.position.z);
    }

    IEnumerator IniciarMetodo()
    {
        yield return new WaitForSeconds(1f);
        comienzo = true;
    }
}
