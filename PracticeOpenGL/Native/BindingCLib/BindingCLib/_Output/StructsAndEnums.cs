using System;
using System.Runtime.InteropServices;

namespace com.Artefact.BindingCLib
{
	[StructLayout (LayoutKind.Explicit)]
	public struct __darwin_mbstate_t
	{
		[FieldOffset (0)]
		public sbyte[] __mbstate8;

		[FieldOffset (0)]
		public long _mbstateL;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct __darwin_pthread_handler_rec
	{
		public unsafe Action<void*>* __routine;

		public unsafe void* __arg;

		public unsafe __darwin_pthread_handler_rec* __next;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct __darwin_pthread_attr_t
	{
		public nint __sig;

		public sbyte[] __opaque;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct __darwin_pthread_cond_t
	{
		public nint __sig;

		public sbyte[] __opaque;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct __darwin_pthread_condattr_t
	{
		public nint __sig;

		public sbyte[] __opaque;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct __darwin_pthread_mutex_t
	{
		public nint __sig;

		public sbyte[] __opaque;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct __darwin_pthread_mutexattr_t
	{
		public nint __sig;

		public sbyte[] __opaque;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct __darwin_pthread_once_t
	{
		public nint __sig;

		public sbyte[] __opaque;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct __darwin_pthread_rwlock_t
	{
		public nint __sig;

		public sbyte[] __opaque;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct __darwin_pthread_rwlockattr_t
	{
		public nint __sig;

		public sbyte[] __opaque;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct _opaque_pthread_t
	{
		public nint __sig;

		public unsafe __darwin_pthread_handler_rec* __cleanup_stack;

		public sbyte[] __opaque;
	}

	static class CFunctions
	{
		// extern int32_t clib_add_internal (int32_t left, int32_t right);
		[DllImport ("__Internal")]
		[Verify (PlatformInvoke)]
		static extern int clib_add_internal (int left, int right);
	}
}
