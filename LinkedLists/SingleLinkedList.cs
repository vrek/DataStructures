using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LinkedLists;

internal class SingleLinkedList
{
    public class Node(int value)
    {
        public int Value = value;
        public Node? Next = null;
    }

    public Node? Head;
    
    public SingleLinkedList()
    {
        Head = null;
    }

    public void InsertAtStart(int value)
    {
        Node newNode = new(value)
        {
            Next = Head
        };
        Head = newNode;
    }

    public void InsertAtEnd(int value)
    {
        Node newNode = new(value);
        if (Head == null)
        {
            Head = newNode;
            return;
        }
        Node current = Head;
        while (current.Next != null)
        {
            current = current.Next;
        }
        current.Next = newNode;
    }

    public void InsertAtPosition(int value, int position)
    {
        Node newNode = new(value);
        if (position == 0)
        {
            newNode.Next = Head;
            Head = newNode;
            return;
        }
        if (position > GetLength() || position < 0)
            throw new ArgumentOutOfRangeException(nameof(position), "Position is out of bounds.");
        Node current = Head;
        for (int i = 0; i < position - 1; i++)
        {
            current = current.Next;
        }
        newNode.Next = current.Next;
        current.Next = newNode;
    }

    public int[] ToArray()
    {
        List<int> values = [];
        Node current = Head;
        while (current != null)
        {
            values.Add(current.Value);
            current = current.Next;
        }
        return [.. values];
    }

    public int GetLength()
    {
        int length = 0;
        Node? current = Head;
        while (current != null)
        {
            length++;
            current = current.Next;
        }
        return length;
    }

    public int GetValueAtPosition(int position)
    {
        if (position < 0 || position >= GetLength())
            throw new ArgumentOutOfRangeException(nameof(position), "Position is out of bounds.");
        Node current = Head;
        for (int i = 0; i < position; i++)
        {
            current = current.Next;
        }
        return current.Value;
    }

    public void RemoveAtPosition(int position)
    {
        if (position < 0 || position >= GetLength())
            throw new ArgumentOutOfRangeException(nameof(position), "Position is out of bounds.");
        if (position == 0)
        {
            Head = Head.Next;
            return;
        }
        Node current = Head;
        for (int i = 0; i < position - 1; i++)
        {
            current = current.Next;
        }
        current.Next = current.Next.Next;
    }
}

public class RemoveAtPositionTests
{
    [Fact]
    public void RemoveAtPosition_ShouldRemoveNodeAtGivenPosition()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(1);
        list.InsertAtStart(2);
        list.InsertAtStart(3); // Final list: 3 → 2 → 1

        list.RemoveAtPosition(1); // Remove node at position 1 (value 2)


        Assert.Equal(3, list.GetValueAtPosition(0));
        Assert.Equal(1, list.GetValueAtPosition(1));
        Assert.Equal(2, list.GetLength());
    }
    [Fact]
    public void RemoveAtPosition_InvalidPosition_ShouldThrowArgumentOutOfRangeException()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(1);
        list.InsertAtStart(2);
        list.InsertAtStart(3); // Final list: 3 → 2 → 1
        Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAtPosition(-1)); // Negative index
        Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAtPosition(3));  // Equal to length
        Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAtPosition(100)); // Far beyond length
    }
}

public class GetValueAtPositionTests
{
    public static class InvalidPositionTestData
    {
        public static IEnumerable<object[]> Cases =>
            new List<object[]>
            {
            new object[] { -1 },  // Negative index
            new object[] { 3 },   // Equal to length (out of bounds)
            new object[] { 100 }, // Far beyond length
            };
    }

    public static class ValidPositionTestData
    {
        public static IEnumerable<object[]> Cases =>
            new List<object[]>
            {
            new object[] { 0, 1 }, // Head
            new object[] { 1, 2 }, // Middle
            new object[] { 2, 3 }, // Tail
            };
    }


    [Theory]
    [MemberData(nameof(ValidPositionTestData.Cases), MemberType = typeof(ValidPositionTestData))]
    public void GetValueAtPosition_ShouldReturnCorrectValue(int position, int expectedValue)
    {
        var list = new SingleLinkedList();
        list.InsertAtEnd(1);
        list.InsertAtEnd(2);
        list.InsertAtEnd(3); // List: 1 → 2 → 3

        int actualValue = list.GetValueAtPosition(position);
        Assert.Equal(expectedValue, actualValue);
    }

    [Theory]
    [MemberData(nameof(InvalidPositionTestData.Cases), MemberType = typeof(InvalidPositionTestData))]
    public void GetValueAt_InvalidPosition_ShouldThrowArgumentOutOfRangeException(int invalidPosition)
    {
        var list = new SingleLinkedList();
        list.InsertAtStart(3);
        list.InsertAtStart(2);
        list.InsertAtStart(1); // List: 1 → 2 → 3

        Assert.Throws<ArgumentOutOfRangeException>(() => list.GetValueAtPosition(invalidPosition));
    }

    [Fact]
        public void GetValueAtPosition_EmptyList_ShouldThrowArgumentOutOfRangeException()
    {     var list = new SingleLinkedList();
        Assert.Throws<ArgumentOutOfRangeException>(() => list.GetValueAtPosition(0));
    }
}
public class InsertAtStartTests
{
    [Fact]
    public void InsertAtStart_ShouldInsertNodesAtHeadInReverseOrder()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(1);
        list.InsertAtStart(2);
        list.InsertAtStart(3); // Final list: 3 → 2 → 1

        Assert.NotNull(list.Head);
        Assert.Equal(3, list.Head.Value);

        Assert.NotNull(list.Head.Next);
        Assert.Equal(2, list.Head.Next.Value);

        Assert.NotNull(list.Head.Next.Next);
        Assert.Equal(1, list.Head.Next.Next.Value);

        Assert.Null(list.Head.Next.Next.Next);
    }


    [Fact]
    public void InsertAtStart_ShouldAddNodesToHead()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(3);
        list.InsertAtStart(2);
        list.InsertAtStart(1); // Expected: 1 → 2 → 3

        Assert.Equal([1, 2, 3], list.ToArray());
    }

    [Fact]
    public void InsertAtStart_EmptyList_ShouldCreateSingleNode()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(42);

        Assert.Equal(1, list.GetLength());
        Assert.Equal([42], list.ToArray());
    }

    [Fact]
    public void InsertAtStart_ShouldHandleNegativeAndZeroValues()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(-1);
        list.InsertAtStart(0);
        list.InsertAtStart(-99); // Expected: -99 → 0 → -1

        Assert.Equal(3, list.GetLength());
        Assert.Equal([-99, 0, -1], list.ToArray());
    }

    [Fact]
    public void InsertAtStart_ManyInsertions_ShouldMaintainOrder()
    {
        SingleLinkedList list = new();
        for (int i = 100; i >= 1; i--)
            list.InsertAtStart(i); // Expected: 1 → 2 → ... → 100

        Assert.Equal(100, list.GetLength());
        Assert.Equal([.. Enumerable.Range(1, 100)], list.ToArray());
    }

    [Fact]
    public void InsertAtStart_ThenInsertAtPosition_ShouldPreserveStructure()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(3);
        list.InsertAtStart(2);
        list.InsertAtStart(1); // List: 1 → 2 → 3

        list.InsertAtPosition(99, 1); // Insert between 1 and 2

        Assert.Equal(4, list.GetLength());
        Assert.Equal([1, 99, 2, 3], list.ToArray());
    }

    [Fact]
    public void InsertAtStart_Duplicates_ShouldBeHandledCorrectly()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(5);
        list.InsertAtStart(5);
        list.InsertAtStart(5); // Expected: 5 → 5 → 5

        Assert.Equal(3, list.GetLength());
        Assert.Equal([5, 5, 5], list.ToArray());
    }

    [Theory]
    [MemberData(nameof(LinkedListLengthTestData.Cases), MemberType = typeof(LinkedListLengthTestData))]
    public void GivenVariousInputs_InsertAtStartShouldBuildCorrectLength(int[] values, int expectedLength)
    {
        SingleLinkedList list = new();

        foreach (int value in values)
            list.InsertAtStart(value);

        int actualLength = list.GetLength();
        Assert.Equal(expectedLength, actualLength);
    }
}

public class InsertAtPositionTests
{
    [Fact]
    public void InsertAtPosition_Zero_ShouldInsertAtHead()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(2);
        list.InsertAtStart(1); // List: 1 -> 2

        list.InsertAtPosition(99, 0); // Insert at head

        Assert.Equal(3, list.GetLength());
        Assert.Equal([99, 1, 2], list.ToArray());
    }

    [Fact]
    public void InsertAtPosition_Middle_ShouldInsertCorrectly()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(3);
        list.InsertAtStart(2);
        list.InsertAtStart(1); // List: 1 -> 2 -> 3

        list.InsertAtPosition(99, 1); // Insert between 1 and 2

        Assert.Equal(4, list.GetLength());
        Assert.Equal([1, 99, 2, 3], list.ToArray());
    }

    [Fact]
    public void InsertAtPosition_End_ShouldAppend()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(2);
        list.InsertAtStart(1); // List: 1 -> 2

        list.InsertAtPosition(99, 2); // Insert at end

        Assert.Equal(3, list.GetLength());
        Assert.Equal([1, 2, 99], list.ToArray());
    }

    [Fact]
    public void InsertAtPosition_InvalidNegative_ShouldThrow()
    {
        SingleLinkedList list = new();
        Assert.Throws<ArgumentOutOfRangeException>(() => list.InsertAtPosition(99, -1));
    }

    [Fact]
    public void InsertAtPosition_TooFar_ShouldThrow()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(1); // Length = 1

        Assert.Throws<ArgumentOutOfRangeException>(() => list.InsertAtPosition(99, 5));
    }

    [Fact]
    public void InsertAtPosition_EmptyListZero_ShouldWork()
    {
        SingleLinkedList list = new();
        list.InsertAtPosition(99, 0);

        Assert.Equal(1, list.GetLength());
        Assert.Equal([99], list.ToArray());
    }

    [Fact]
    public void InsertAtEveryValidPosition_ShouldMaintainCorrectOrder()
    {
        // Initial list: 10 -> 20 -> 30 -> 40 -> 50
        SingleLinkedList list = new();
        list.InsertAtStart(50);
        list.InsertAtStart(40);
        list.InsertAtStart(30);
        list.InsertAtStart(20);
        list.InsertAtStart(10);

        // Insert value 99 at every valid position (0 to 5)
        for (int position = 0; position <= list.GetLength(); position++)
        {
            SingleLinkedList testList = CloneList(list); // Clone to preserve original
            testList.InsertAtPosition(99, position);

            int[] expected = BuildExpectedArrayWithInsert([10, 20, 30, 40, 50], 99, position);
            Assert.Equal(expected, testList.ToArray());
        }
    }
    private static SingleLinkedList CloneList(SingleLinkedList original)
    {
        SingleLinkedList clone = new();
        int[] values = original.ToArray();
        // Insert in reverse to preserve order
        for (int i = values.Length - 1; i >= 0; i--)
            clone.InsertAtStart(values[i]);
        return clone;
    }

    private static int[] BuildExpectedArrayWithInsert(int[] original, int valueToInsert, int position)
    {
        List<int> result = [.. original];
        result.Insert(position, valueToInsert);
        return [.. result];
    }
}

public class ToArrayTests
{
    [Fact]
    public void ToArrayReturnsCorrectArray()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(3);
        list.InsertAtStart(2);
        list.InsertAtStart(1); // List: 1 -> 2 -> 3
        int[] array = list.ToArray();
        Assert.Equal([1, 2, 3], array);
    }
}

public class InsertAtEndTests 
{
    [Fact]
    public void InsertAtEnd_SingleValue_ShouldCreateSingleNode()
    {
        SingleLinkedList list = new();
        list.InsertAtEnd(42);

        Assert.Equal(1, list.GetLength());
        Assert.Equal([42], list.ToArray());
    }

    [Fact]
    public void InsertAtEnd_MultipleValues_ShouldAppendInOrder()
    {
        SingleLinkedList list = new();
        list.InsertAtEnd(1);
        list.InsertAtEnd(2);
        list.InsertAtEnd(3);

        Assert.Equal(3, list.GetLength());
        Assert.Equal([1, 2, 3], list.ToArray());
    }

    [Fact]
    public void InsertAtEnd_AfterInsertAtStart_ShouldAppendCorrectly()
    {
        SingleLinkedList list = new();
        list.InsertAtStart(2);
        list.InsertAtStart(1); // List: 1 → 2

        list.InsertAtEnd(3); // Should result in: 1 → 2 → 3

        Assert.Equal(3, list.GetLength());
        Assert.Equal([1, 2, 3], list.ToArray());
    }

    [Fact]
    public void InsertAtEnd_DuplicateValues_ShouldBeHandledCorrectly()
    {
        SingleLinkedList list = new();
        list.InsertAtEnd(5);
        list.InsertAtEnd(5);
        list.InsertAtEnd(5);

        Assert.Equal(3, list.GetLength());
        Assert.Equal([5, 5, 5], list.ToArray());
    }

    [Fact]
    public void InsertAtEnd_StressTest_ShouldMaintainOrder()
    {
        SingleLinkedList list = new();
        for (int i = 1; i <= 100; i++)
            list.InsertAtEnd(i);

        Assert.Equal(100, list.GetLength());
        Assert.Equal([.. Enumerable.Range(1, 100)], list.ToArray());
    }
}


public static class LinkedListLengthTestData
{
    public static IEnumerable<object[]> Cases =>
        [
            [Array.Empty<int>(), 0],
            [new int[] { 42 }, 1],
            [new int[] { 1, 2, 3 }, 3],
            [Enumerable.Range(1, 10).ToArray(), 10],
            [Enumerable.Range(1, 100).ToArray(), 100],
        ];
}

