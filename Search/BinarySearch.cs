using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Search;

internal class BinarySearch
{
    public static int CountNumberOfSearchesToFindTarget(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length - 1;
        int count = 1;

        int[] SortedNums = nums.OrderBy(n => n).ToArray();

        while (left <= right)
        {
            int mid = (right + left) / 2;
            if (SortedNums[mid] == target) return count;
            if (SortedNums[mid] < target) left = mid + 1;
            else right = mid - 1;
            count++;
        }

        return -1;
    }
}

public class BinarySearchTests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3, 4, 5 }, 3, 1)]
    [InlineData(new int[] { 10, 20, 30, 40, 50 }, 30, 1)]
    [InlineData(new int[] { -5, -3, -1, 0, 1 }, -1, 1)]
    [InlineData(new int[] { 100, 200, 300, 400, 500 }, 100, 2)]
    [InlineData(new int[] { 7, 14, 21, 28, 35 }, 35, 3)]
    [InlineData(new int[] { 1, 3, 5, 7, 9, 11 }, 2, -1)]
    [InlineData(new[] { 5, 3, 9, 1, 7 }, 9, 3)]

    public void GivenValidIntegersFunctionShouldReturnAValidResponse(int[] nums, int target, int expected)
    {
        int result = BinarySearch.CountNumberOfSearchesToFindTarget(nums, target);
        Assert.Equal(expected, result);
    }
}


