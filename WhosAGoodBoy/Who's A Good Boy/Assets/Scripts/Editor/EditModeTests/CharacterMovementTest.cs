using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class CharacterMovementTest {

    private GameObject _gameObject;
    private CharacterMovement _characterMovement;


    [SetUp]
    public void SetUp()
    {
        _gameObject = new GameObject();
        _characterMovement = _gameObject.AddComponent<CharacterMovement>();
    }

    [Test]
    public void CharacterMovement_StartsWithNoSpeed()
    {
        Assert.AreEqual(_characterMovement.Speed, 0);
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator EditModeTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}
