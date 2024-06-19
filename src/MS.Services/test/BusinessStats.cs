using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MS.Services
{
    public class BusinessStats
    {
        [DllImport("BusinessStatsLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double CalculateTurnover([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] double[] amountArray, int size);

        [DllImport("BusinessStatsLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double CalculateOrderCompletionRate(int validOrdersCount, int totalOrdersCount);
    }
}
