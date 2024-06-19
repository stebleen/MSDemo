#pragma once
#ifdef BUSINESSSTATSLIB_EXPORTS
#define BUSINESSSTATSLIB_API __declspec(dllexport)
#else
#define BUSINESSSTATSLIB_API __declspec(dllimport)
#endif

extern "C" {
    BUSINESSSTATSLIB_API double CalculateTurnover(double* amountArray, int size);
    BUSINESSSTATSLIB_API double CalculateOrderCompletionRate(int validOrdersCount, int totalOrdersCount);
}
