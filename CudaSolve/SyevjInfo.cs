﻿//	Copyright (c) 2020, Michael Kunz. All rights reserved.
//	http://kunzmi.github.io/managedCuda
//
//	This file is part of ManagedCuda.
//
//	ManagedCuda is free software: you can redistribute it and/or modify
//	it under the terms of the GNU Lesser General Public License as 
//	published by the Free Software Foundation, either version 2.1 of the 
//	License, or (at your option) any later version.
//
//	ManagedCuda is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//	GNU Lesser General Public License for more details.
//
//	You should have received a copy of the GNU Lesser General Public
//	License along with this library; if not, write to the Free Software
//	Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
//	MA 02110-1301  USA, http://www.gnu.org/licenses/.


using System;
using System.Text;
using System.Diagnostics;
using ManagedCuda.BasicTypes;

namespace ManagedCuda.CudaSolve
{
	/// <summary>
	/// 
	/// </summary>
	public class SyevjInfo : IDisposable
	{
		private syevjInfo _info;
		private cusolverStatus res;
		private bool disposed;

		#region Contructors
		/// <summary>
		/// </summary>
		public SyevjInfo()
		{
			_info = new syevjInfo();
			res = CudaSolveNativeMethods.Dense.cusolverDnCreateSyevjInfo(ref _info);
			Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnCreateSyevjInfo", res));
			if (res != cusolverStatus.Success)
				throw new CudaSolveException(res);
		}

		/// <summary>
		/// For dispose
		/// </summary>
		~SyevjInfo()
		{
			Dispose(false);
		}
		#endregion

		#region Dispose
		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// For IDisposable
		/// </summary>
		/// <param name="fDisposing"></param>
		protected virtual void Dispose(bool fDisposing)
		{
			if (fDisposing && !disposed)
			{
				//Ignore if failing
				res = CudaSolveNativeMethods.Dense.cusolverDnDestroySyevjInfo(_info);
				Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnDestroySyevjInfo", res));
				disposed = true;
			}
			if (!fDisposing && !disposed)
				Debug.WriteLine(String.Format("ManagedCUDA not-disposed warning: {0}", this.GetType()));
		}
		#endregion

		/// <summary>
		/// Returns the inner handle.
		/// </summary>
		public syevjInfo Info
		{
			get { return _info; }
		}

		/// <summary>
		/// </summary>
		public void SetTolerance(double tolerance)
		{
			res = CudaSolveNativeMethods.Dense.cusolverDnXsyevjSetTolerance(_info, tolerance);
			Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnXsyevjSetTolerance", res));
			if (res != cusolverStatus.Success)
				throw new CudaSolveException(res);
		}

		/// <summary>
		/// </summary>
		public void SetMaxSweeps(int max_sweeps)
		{
			res = CudaSolveNativeMethods.Dense.cusolverDnXsyevjSetMaxSweeps(_info, max_sweeps);
			Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnXsyevjSetMaxSweeps", res));
			if (res != cusolverStatus.Success)
				throw new CudaSolveException(res);
		}

		/// <summary>
		/// </summary>
		public void SetSortEig(int sort_eig)
		{
			res = CudaSolveNativeMethods.Dense.cusolverDnXsyevjSetSortEig(_info, sort_eig);
			Debug.WriteLine(String.Format("{0:G}, {1}: {2}", DateTime.Now, "cusolverDnXsyevjSetSortEig", res));
			if (res != cusolverStatus.Success)
				throw new CudaSolveException(res);
		}
	}
}
