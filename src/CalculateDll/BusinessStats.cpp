#include "BusinessStats.h"

#include "pch.h"

extern "C" {

    double CalculateTurnover(double* amountArray, int size) {
        double total = 0.0;
        for (int i = 0; i < size; ++i) {
            total += amountArray[i];
        }
        return total;
    }

    double CalculateOrderCompletionRate(int validOrdersCount, int totalOrdersCount) {
        if (totalOrdersCount == 0) return 0.0; // Avoid division by zero
        return (double)validOrdersCount / totalOrdersCount;
    }
}