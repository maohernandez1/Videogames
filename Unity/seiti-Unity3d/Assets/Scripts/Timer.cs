using System;

/// <summary>
/// Cronometro de tiempo para calculos de intervalos.
/// </summary>
public class Timer
{
	#region Miembros privados
	private int delta;
	private int pauseDelta;
	private bool isPaused = false;
	#endregion
	
	#region Propiedades
	/// <summary>
	/// Indica si el cronometro esta en pausa.
	/// </summary>
	public bool IsPaused
	{
		get { return isPaused; }
	}
	
	/// <summary>
	/// Devuelve los milisegundos transcurridos desdel ultimo reinicio o desde que se creo el cronometro.
	/// </summary>
	public int Value
	{
		get
		{
			return (isPaused ? pauseDelta : System.Environment.TickCount) - delta;
		}
	}
	#endregion
	
	#region Constructores
	/// <summary>
	/// Crea una instancia de un cronometro.
	/// </summary>
	public Timer()
	{
		this.Reset();
	}
	#endregion
	
	#region Metodos y funciones
	/// <summary>
	/// Reinicia el cronometro a 0.
	/// </summary>
	public void Reset()
	{
		isPaused = false;
		delta = System.Environment.TickCount;
	}
	
	/// <summary>
	/// Pausa o reactiva el cronometro.
	/// </summary>
	public void Pause()
	{
		isPaused = !isPaused;
		if (isPaused)
			pauseDelta = System.Environment.TickCount;
		else
			delta += System.Environment.TickCount - pauseDelta;
	}
	#endregion
}
