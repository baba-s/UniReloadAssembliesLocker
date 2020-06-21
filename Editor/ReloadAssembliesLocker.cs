﻿using UnityEditor;

namespace Kogane.Internal
{
	[InitializeOnLoad]
	internal static class ReloadAssembliesLocker
	{
		private static bool m_isLock;

		static ReloadAssembliesLocker()
		{
			UpdateChecked();

			void OnChanged( PlayModeStateChange change )
			{
				if ( change != PlayModeStateChange.ExitingEditMode ) return;
				Unlock();
			}

			EditorApplication.playModeStateChanged += OnChanged;
		}

		[MenuItem( "Edit/UniReloadAssembliesLocker/Lock" )]
		private static void Lock()
		{
			if ( m_isLock ) return;
			m_isLock = true;
			EditorApplication.LockReloadAssemblies();
			UpdateChecked();
		}

		[MenuItem( "Edit/UniReloadAssembliesLocker/Unlock" )]
		private static void Unlock()
		{
			if ( !m_isLock ) return;
			m_isLock = false;
			EditorApplication.UnlockReloadAssemblies();
			UpdateChecked();
		}

		private static void UpdateChecked()
		{
			Menu.SetChecked( "Edit/UniReloadAssembliesLocker/Lock", m_isLock );
			Menu.SetChecked( "Edit/UniReloadAssembliesLocker/Unlock", !m_isLock );
		}
	}
}