using UnityEngine;
using NUnit.Framework;

public class PlayerControllerTests {
    private static readonly Vector2 _downRight = Vector2.down + Vector2.right;

    [Test]
    public void GetChange_SameVectors_ReturnsNull()
    {
        Assert.IsNull(PlayerController.GetDirectionChange(Vector2.down, Vector2.down));
    }

    [Test]
    public void GetChange_SameVectors_DifferentMagnitudes_ReturnsNull()
    {
        Assert.IsNull(PlayerController.GetDirectionChange(Vector2.down, Vector2.down * .5f));
    }

    [Test]
    public void GetChange_DifferentSimple_ReturnsNew()
    {
        Assert.AreEqual(Cardinal.S, PlayerController.GetDirectionChange(Vector2.down, Vector2.up));
    }

    [Test]
    public void GetChange_SameComplex_ReturnsNull()
    {
        Assert.IsNull(PlayerController.GetDirectionChange(_downRight, _downRight));
    }

    [Test]
    public void GetChange_ComplexPrevSimpleNext_ReturnsNext()
    {
        Assert.AreEqual(Cardinal.W, PlayerController.GetDirectionChange(Vector2.left, _downRight));
    }

    [Test]
    public void GetChange_SimplePrevComplexNext_ReturnsCorrectCardinal()
    {
        Assert.AreEqual(Cardinal.S, PlayerController.GetDirectionChange(_downRight, Vector2.right));
    }

    [Test]
    public void GetChange_ZeroNext_ReturnsNull()
    {
        Assert.IsNull(PlayerController.GetDirectionChange(Vector2.zero, Vector2.right));
    }
}
