''' <summary>
''' One decay-chain branch: an ordered, 1-based list of DecaySeriesItem,
''' matching the call-site surface (Item/Count/Add/Remove, 1-based
''' indexing) of the Microsoft.VisualBasic.Collection this type replaces
''' throughout ProcessDecaySeries.vb and RadToolzFunctions.vb (DDR-0006).
''' Unlike Collection.Item, Item here returns DecaySeriesItem directly
''' instead of Object, so callers no longer late-bind on property access -
''' this is the change that resolves the DEBT-0001/Option-Strict-On late
''' binding this type was introduced to fix. Item also supports a setter
''' (Collection.Item does not), used by BubbleSortCollection's in-place
''' swap.
''' </summary>
Public Class DecayChainBranch

    Private ReadOnly _items As New List(Of DecaySeriesItem)

    ''' <summary>Number of items currently in the branch.</summary>
    Public ReadOnly Property Count As Integer
        Get
            Return _items.Count
        End Get
    End Property

    ''' <summary>1-based item access, matching Collection.Item's existing convention.</summary>
    Default Public Property Item(index As Integer) As DecaySeriesItem
        Get
            Return _items(index - 1)
        End Get
        Set(value As DecaySeriesItem)
            _items(index - 1) = value
        End Set
    End Property

    ''' <summary>Appends an item to the end of the branch.</summary>
    Public Sub Add(item As DecaySeriesItem)
        _items.Add(item)
    End Sub

    ''' <summary>Removes the item at the given 1-based index.</summary>
    Public Sub Remove(index As Integer)
        _items.RemoveAt(index - 1)
    End Sub

End Class