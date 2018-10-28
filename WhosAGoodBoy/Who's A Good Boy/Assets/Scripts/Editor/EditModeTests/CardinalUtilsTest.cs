using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

public class CardinalUtilsTest {

    [Test]
    public void ToVector2_N_Roundtrip()
    {
        Assert.AreEqual(Vector2.up, Cardinal.N.ToVector2().ToCardinal().ToVector2());
    }

    [Test]
    public void ToVector2_S_Roundtrip()
    {
        Assert.AreEqual(Vector2.down, Cardinal.S.ToVector2().ToCardinal().ToVector2());
    }

    [Test]
    public void ToVector2_E_Roundtript()
    {
        Assert.AreEqual(Vector2.right, Cardinal.E.ToVector2().ToCardinal().ToVector2());
    }

    [Test]
    public void ToVector2_W_Roundtrip()
    {
        Assert.AreEqual(Vector2.left, Cardinal.W.ToVector2().ToCardinal().ToVector2());
    }

    [Test]
    public void ToCardinal_NonCardinalVec_ReturnsNull()
    {
        Assert.Throws<KeyNotFoundException>(() => { Vector2.zero.ToCardinal(); });
    }

    [Test]
    public void IsCardinal_NonCardinalVec_ReturnsFalse()
    {
        Assert.IsFalse(Vector2.zero.IsCardinal());
    }

    [Test]
    public void IsCardinal_CardinalVec_ReturnsTrue()
    {
        Assert.IsTrue(Vector2.left.IsCardinal());
    }

}
