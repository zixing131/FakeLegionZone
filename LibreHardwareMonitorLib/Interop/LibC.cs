// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// Copyright (C) LibreHardwareMonitor and Contributors.
// All Rights Reserved.

using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace LibreHardwareMonitor.Interop
{
    public class LibC
    {
        private const string DllName = "libc";

        [DllImport(DllName)]
        public static extern int sched_getaffinity(int pid, IntPtr maskSize, ref ulong mask);

        [DllImport(DllName)]
        public static extern int sched_setaffinity(int pid, IntPtr maskSize, ref ulong mask);
    }
}
