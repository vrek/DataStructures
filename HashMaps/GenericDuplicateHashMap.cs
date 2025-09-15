using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.HashMaps;

internal class GenericDuplicateHashMap
{
    internal static bool FindDuplicate<T>(T[] items)
    {
        HashSet<T> SeenItems = [];
        foreach (T item in items)
        {
            if (SeenItems.Contains(item)) return true;
            SeenItems.Add(item);
        }
        return false;
    }
}

public class GenericDuplicateHashMapPassingTests
{
    [Theory]
    [InlineData(new int[] { 1, 2, 3, 1 }, true)]
    [InlineData(new int[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 }, true)]
    [InlineData(new string[] { "apple", "banana", "orange", "apple" }, true)]
    [InlineData(new double[] { 1.1, 2.2, 3.3, 1.1 }, true)]
    [InlineData(new char[] { 'a', 'b', 'c', 'a' }, true)]
    [InlineData(new bool[] { true, false, true }, true)]
    [InlineData(new object[] { "x", 42, "x" }, true)]
    //[InlineData(new DateTime[] { new(2020, 1, 1), new DateTime(2021, 1, 1), new DateTime(2020, 1, 1) }, true)]
    public void GivenValidItemsFunctionShouldReturnATrue<T>(T[] items, bool expected)
    {
        bool result = GenericDuplicateHashMap.FindDuplicate(items);
        Assert.Equal(expected, result);
    }
}

public class GenericDuplicateHashMapFailingTests
{
    [Theory]
    [InlineData(new string[] { "cat", "dog", "fish" }, false)]
    [InlineData(new int[] { 10, 20, 30, 40 }, false)]
    [InlineData(new string[] { "red", "green", "blue" }, false)]
    [InlineData(new double[] { 0.1, 0.2, 0.3 }, false)]
    [InlineData(new char[] { 'x', 'y', 'z' }, false)]
    [InlineData(new bool[] { true, false }, false)]

    public void GivenValidItemsFunctionShouldReturnAFalse<T>(T[] items, bool expected)
    {
        bool result = GenericDuplicateHashMap.FindDuplicate(items);
        Assert.Equal(expected, result);
    }
}

public class GenericDuplicateHashMapMixedTypesTests
{
    [Theory]
    [InlineData(new object[] { "apple", 42, true, "apple" }, true)]
    [InlineData(new object[] { "apple", 42, true }, false)]
    public void GivenValidItemsFunctionShouldReturnAFalse<T>(T[] items, bool expected)
    {
        bool result = GenericDuplicateHashMap.FindDuplicate(items);
        Assert.Equal(expected, result);
    }
}