using UnityEngine;
using NUnit.Framework;

public class BoundExtensionsTest {

    [Test]
    public void Delta_EncompassedByOther_ReturnsZero()
    {
        Bounds inner = new Bounds(new Vector3(0, 0, 0), new Vector3(2, 2, 2));
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(4, 4, 4));
        Assert.AreEqual(Vector3.zero, inner.Delta(outer));
    }

    [Test]
    public void Delta_EncompassesOther_ReturnsZero()
    {
        Bounds inner = new Bounds(new Vector3(0, 0, 0), new Vector3(2, 2, 2));
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(4, 4, 4));
        Assert.AreEqual(Vector3.zero, outer.Delta(inner));
    }

    [Test]
    public void Delta_TooUp_ReturnsDown()
    {
        Bounds inner = new Bounds(new Vector3(0, 1, 0), new Vector3(2, 2, 2));
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(2, 2, 2));
        Assert.AreEqual(new Vector3(0, -1, 0), inner.Delta(outer));
    }

    [Test]
    public void Delta_TooDown_ReturnsUp()
    {
        Bounds inner = new Bounds(new Vector3(0, -1, 0), new Vector3(2, 2, 2));
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(2, 2, 2));
        Assert.AreEqual(new Vector3(0, 1, 0), inner.Delta(outer));
    }

    [Test]
    public void Delta_TooLeft_ReturnRight()
    {
        Bounds inner = new Bounds(new Vector3(-1, 0, 0), new Vector3(2, 2, 2));
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(2, 2, 2));
        Assert.AreEqual(new Vector3(1, 0, 0), inner.Delta(outer));
    }

    [Test]
    public void Delta_TooIn_ReturnOut()
    {
        Bounds inner = new Bounds(new Vector3(0, 0, 1), new Vector3(2, 2, 2));
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(2, 2, 2));
        Assert.AreEqual(new Vector3(0, 0, -1), inner.Delta(outer));
    }

    [Test]
    public void Delta_TooRight_ReturnLeft()
    {
        Bounds inner = new Bounds(new Vector3(1, 0, 0), new Vector3(2, 2, 2));
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(2, 2, 2));
        Assert.AreEqual(new Vector3(-1, 0, 0), inner.Delta(outer));
    }

    [Test]
    public void Delta_UpRight_ReturnDownLeft()
    {
        Bounds inner = new Bounds(new Vector3(1, 1, 0), new Vector3(2, 2, 2));
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(2, 2, 2));
        Assert.AreEqual(new Vector3(-1, -1, 0), inner.Delta(outer));
    }

    [Test]
    public void Delta_DownLeft_ReturnUpRight()
    {
        Bounds inner = new Bounds(new Vector3(-1, -1, 0), new Vector3(2, 2, 2));
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(2, 2, 2));
        Assert.AreEqual(new Vector3(1, 1, 0), inner.Delta(outer));
    }

    [Test]
    public void Delta_DownRight_ReturnUpLeft()
    {
        Bounds inner = new Bounds(new Vector3(1, -1, 0), new Vector3(2, 2, 2));
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(2, 2, 2));
        Assert.AreEqual(new Vector3(-1, 1, 0), inner.Delta(outer));
    }

    [Test]
    public void Delta_OddShapes_StillWorks()
    {
        Bounds inner = new Bounds(new Vector3(1, 2, 0), new Vector3(2, 4, 2));
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(4, 6, 8));
        Assert.AreEqual(new Vector3(0, -1, 0), inner.Delta(outer));
    }

    [Test]
    public void Delta_TooRight_ButMiddle_ReturnsJustLeft()
    {
        Bounds inner = new Bounds(new Vector3(4, 1, 0), new Vector3(2, 2, 2));
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(4, 16, 4));
        Assert.AreEqual(new Vector3(-3, 0, 0), inner.Delta(outer));
    }

    [Test]
    public void Contains_WhenSameCenterOuterLarger_ReturnsTrue()
    {
        Bounds inner = new Bounds(new Vector3(0, 0, 0), new Vector3(2, 2, 2) );
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(6, 6, 6) );
        Assert.IsTrue(outer.Contains(inner));
    }


    [Test]
    public void Contains_WhenSameCenterOuterSmaller_ReturnsTrue()
    {
        Bounds inner = new Bounds(new Vector3(0, 0, 0), new Vector3(2, 2, 2) );
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(6, 6, 6) );
        Assert.IsFalse(inner.Contains(outer));
    }

    [Test]
    public void Contains_WhenTotallyDisjoint_ReturnsFalse()
    {
        Bounds inner = new Bounds(new Vector3(20, 20, 20), new Vector3(2, 2, 2) );
        Bounds outer = new Bounds(new Vector3(0, 0, 0), new Vector3(6, 6, 6) );
        Assert.IsFalse(inner.Contains(outer));
    }
}
