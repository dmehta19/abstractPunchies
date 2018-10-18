using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace PaintIn3D
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(P3dPaintDebug))]
	public class P3dPaintDebug_Editor : P3dEditor<P3dPaintDebug>
	{
		protected override void OnInspector()
		{
			DrawDefault("color", "The color of the debug.");
			BeginError(Any(t => t.Duration <= 0.0f));
				DrawDefault("duration", "The duration of the debug.");
			EndError();
			BeginError(Any(t => t.Size <= 0.0f));
				DrawDefault("size", "The size of the debug.");
			EndError();
		}
	}
}
#endif

namespace PaintIn3D
{
	/// <summary>This component allows you to debug hit points. A hit point can be found using a companion component like: P3dDragRaycast, P3dOnCollision, P3dOnParticleCollision.</summary>
	[HelpURL(P3dHelper.HelpUrlPrefix + "P3dPaintDebug")]
	[AddComponentMenu(P3dHelper.ComponentMenuPrefix + "Paint Debug")]
	public class P3dPaintDebug : MonoBehaviour, IHitHandler
	{
		/// <summary>The color of the debug.</summary>
		public Color Color { set { color = value; } get { return color; } } [SerializeField] private Color color = Color.white;

		/// <summary>The duration of the debug.</summary>
		public float Duration { set { duration = value; } get { return duration; } } [SerializeField] private float duration = 0.05f;

		/// <summary>The size of the debug.</summary>
		public float Size { set { size = value; } get { return size; } } [SerializeField] private float size = 0.05f;

		/// <summary>This allows you to paint a decal at the specified point.</summary>
		public void HandleHit(Vector3 position, Vector3 normal, bool preview, float pressure)
		{
			var x = Vector3.right   * size;
			var y = Vector3.up      * size;
			var z = Vector3.forward * size;

			Debug.DrawLine(position - x, position + x, color, duration);
			Debug.DrawLine(position - y, position + y, color, duration);
			Debug.DrawLine(position - z, position + z, color, duration);
		}
	}
}