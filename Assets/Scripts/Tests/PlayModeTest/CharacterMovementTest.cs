using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class CharacterMovementTest : InputTestFixture
{
    private Keyboard keyboard;
    private Mouse mouse;
    
    public override void Setup()
    {
        base.Setup();
        keyboard = InputSystem.AddDevice<Keyboard>();
        mouse = InputSystem.AddDevice<Mouse>();
    }

    [OneTimeSetUp]
    public void Init()
    {
        // SceneManager.LoadScene("Scenes/Test/TestScene");
        SceneManager.LoadScene("FarmScene");
        
        // keyboard = InputSystem.AddDevice<Keyboard>();
        // mouse = InputSystem.AddDevice<Mouse>();
    }

    [UnityTest]
    public IEnumerator TestPlayerInstance()
    {
        GameObject player = GameObject.FindWithTag("Player");
        yield return null;
        Assert.IsNotNull(player);
    }

    // A Test behaves as an ordinary method
    [UnityTest]
    public IEnumerator TestPlayerMoveRight()
    {
        GameObject player = GameObject.FindWithTag("Player");
        yield return null;
        
        Vector2 startPos = player.transform.position;
        
        Press(keyboard.dKey);
        yield return new WaitForSeconds(1f);
        Assert.That(player.transform.position.x, Is.GreaterThan(startPos.x));
    }

    [UnityTest]
    public IEnumerator TestPlayerMoveLeft()
    {
        GameObject player = GameObject.FindWithTag("Player");
        yield return null;
        
        Vector2 startPos = player.transform.position;
        
        Press(keyboard.aKey);
        yield return new WaitForSeconds(1f);
        Assert.That(player.transform.position.x, Is.LessThan(startPos.x));
    }
}
