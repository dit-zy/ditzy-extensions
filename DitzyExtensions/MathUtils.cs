using System;
using System.Drawing;
using System.Numerics;

namespace DitzyExtensions {
	public static class MathUtils {
		public static Vector2 V2(float x) => V2(x, x);
		public static Vector2 V2(float x, float y) => new Vector2(x, y);

		public static Vector3 V3(float x) => V3(x, x, x);
		public static Vector3 V3(Vector2 xy, float z) => V3(xy.X, xy.Y, z);
		public static Vector3 V3(float x, float y, float z) => new Vector3(x, y, z);

		public static Vector4 V4(float x) => V4(x, x, x, x);
		public static Vector4 V4(Vector2 xy, float z, float w) => V4(xy.X, xy.Y, z, w);
		public static Vector4 V4(Vector2 xy, Vector2 zw) => V4(xy.X, xy.Y, zw.X, zw.Y);
		public static Vector4 V4(Vector3 xyz, float w) => V4(xyz.X, xyz.Y, xyz.Z, w);
		public static Vector4 V4(float x, float y, float z, float w) => new Vector4(x, y, z, w);

		public static byte GetBit(this byte n, int bitIndex) => (byte)((n >> bitIndex) & 1);
		public static short GetBit(this short n, int bitIndex) => (short)((n >> bitIndex) & 1);
		public static ushort GetBit(this ushort n, int bitIndex) => (ushort)((n >> bitIndex) & 1);
		public static int GetBit(this int n, int bitIndex) => (n >> bitIndex) & 1;
		public static uint GetBit(this uint n, int bitIndex) => (n >> bitIndex) & 1U;
		public static long GetBit(this long n, int bitIndex) => (n >> bitIndex) & 1L;
		public static ulong GetBit(this ulong n, int bitIndex) => (n >> bitIndex) & 1UL;

		public static bool IsBitSet(this byte n, int bitIndex) => n.GetBit(bitIndex) != 0;
		public static bool IsBitSet(this short n, int bitIndex) => n.GetBit(bitIndex) != 0;
		public static bool IsBitSet(this ushort n, int bitIndex) => n.GetBit(bitIndex) != 0;
		public static bool IsBitSet(this int n, int bitIndex) => n.GetBit(bitIndex) != 0;
		public static bool IsBitSet(this uint n, int bitIndex) => n.GetBit(bitIndex) != 0;
		public static bool IsBitSet(this long n, int bitIndex) => n.GetBit(bitIndex) != 0;
		public static bool IsBitSet(this ulong n, int bitIndex) => n.GetBit(bitIndex) != 0;

		public static float Clamp01(this float f) => f < 0 ? 0 : (f > 1 ? 1 : f);

		public static double Clamp01(this double d) => d < 0 ? 0 : (d > 1 ? 1 : d);

#if !N48_S2
		public static float Frac(this float f) => Clamp01(f - MathF.Truncate(f));
#else
		public static float Frac(this float f) => (float)Clamp01(f - Math.Truncate(f));
#endif

		public static double Frac(this double d) => Clamp01(d - Math.Truncate(d));

		public static long Factorial(this int n) {
			var factorial = 1L;
			for (; 1 < n; n--) {
				factorial *= n;
			}
			return factorial;
		}
	}
}
