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
        SceneManager.LoadScene("FarmScene");
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
    
    [UnityTest]
    public IEnumerator TestPlayerJump()
    {
        GameObject player = GameObject.FindWithTag("Player");
        yield return null;
        
        Vector2 startPos = player.transform.position;
        
        Press(keyboard.spaceKey);
        yield return new WaitForSeconds(.5f);
        Assert.That(player.transform.position.y, Is.GreaterThan(startPos.y));
    }

    [UnityTest]
    public IEnumerator TestPlayerDash()
    {
        GameObject player = GameObject.FindWithTag("Player");
        yield return null;
        
        Vector2 startPos = player.transform.position;
        
        Press(keyboard.cKey);
        yield return new WaitForSeconds(.5f);
        Assert.That(player.transform.position.y, Is.LessThan(startPos.y));
    }
}
