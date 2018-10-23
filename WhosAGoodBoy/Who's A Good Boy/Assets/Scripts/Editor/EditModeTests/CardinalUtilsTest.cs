using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor.SceneManagement;

public class CardinalUtilsTest {
    private static readonly Vector2 _downRight = Vector2.down + Vector2.right;

    [Test]
    public void GetChange_SameVectors_ReturnsNull()
    {
        Assert.IsNull(CardinalUtils.GetChange(Vector2.down, Vector2.down));
    }

    [Test]
    public void GetChange_SameVectors_DifferentMagnitudes_ReturnsNull()
    {
        Assert.IsNull(CardinalUtils.GetChange(Vector2.down, Vector2.down * .5f));
    }

    [Test]
    public void GetChange_DifferentSimple_ReturnsNew()
    {
        Assert.AreEqual(Cardinal.S, CardinalUtils.GetChange(Vector2.down, Vector2.up));
    }

    [Test]
    public void GetChange_SameComplex_ReturnsNull()
    {
        Assert.IsNull(CardinalUtils.GetChange(_downRight, _downRight));
    }

    [Test]
    public void GetChange_ComplexPrevSimpleNext_ReturnsNext()
    {
        Assert.AreEqual(Cardinal.W, CardinalUtils.GetChange(Vector2.left, _downRight));
    }

    [Test]
    public void GetChange_SimplePrevComplexNext_ReturnsCorrectCardinal()
    {
        Assert.AreEqual(Cardinal.S, CardinalUtils.GetChange(_downRight, Vector2.right));
    }

    [Test]
    public void GetChange_ZeroNext_ReturnsNull()
    {
        Assert.IsNull(CardinalUtils.GetChange(Vector2.zero, Vector2.right));
    }

    [Test]
    public void ToVector2_N_ReturnsUp()
    {
        Assert.AreEqual(Vector2.up, Cardinal.N.ToVector2());
    }

    [Test]
    public void ToVector2_S_ReturnsDown()
    {
        Assert.AreEqual(Vector2.down, Cardinal.S.ToVector2());
    }

    [Test]
    public void ToVector2_E_Returnsright()
    {
        Assert.AreEqual(Vector2.right, Cardinal.E.ToVector2());
    }

    [Test]
    public void ToVector2_W_ReturnsLeft()
    {
        Assert.AreEqual(Vector2.left, Cardinal.W.ToVector2());
    }
}
