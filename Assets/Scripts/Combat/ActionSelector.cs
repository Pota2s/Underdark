using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ActionSelector
{
    public abstract IEnumerator SelectAction(
        CharacterObject actor,
        BattleContext context,
        Action<Action_Base, List<CharacterObject>> onComplete
    );
}

public class PlayerActionSelector : ActionSelector
{

    public override IEnumerator SelectAction(
        CharacterObject actor,
        BattleContext context,
        Action<Action_Base, List<CharacterObject>> onComplete
    )
    {
        Action_Base selectedAction = null;
        List<CharacterObject> targets = new List<CharacterObject>();
        bool isDecided = false;

        
        BattleUI_Master battleUI = BattleUI_Master.instance;
        battleUI.Enable();
        Debug.Log("PlayerActionSelector: Waiting for player to select an action.");

        battleUI.attackButton.onClick.AddListener(() =>
        {

            Action_Base basicAction = actor.characterData.baseStatistics.basicAction;
            DescriptionManager.instance.SetDescription(basicAction);
            selectedAction = basicAction;

            if (basicAction.target == Action_Base.TargetType.SELF)
            {
                targets.Add(actor);
                isDecided = true;
            }
            else if (basicAction.target == Action_Base.TargetType.ENEMY)
            {
                BattleUI_Master.instance.EnableTargeting(
                    context.enemyCharacters.FindAll(c => !c.characterData.IsDead()),
                    (CharacterObject target) =>
                    {
                        targets.Add(target);
                        isDecided = true;
                    }
                );
            }
            else if (basicAction.target == Action_Base.TargetType.ALL_ENEMIES)
            {
                targets.AddRange(context.enemyCharacters.FindAll(c => !c.characterData.IsDead()));
                isDecided = true;
            }
        });

        battleUI.spellButton.onClick.AddListener(() =>
        {
            Action_Base spellAction = null;
            battleUI.EnableSpellSelection(actor, (action) =>
            {
            spellAction = action;
            DescriptionManager.instance.SetDescription(spellAction);
            selectedAction = spellAction;
                if (spellAction.target == Action_Base.TargetType.SELF)
                {
                    targets.Add(actor);
                    isDecided = true;
                }
                else if (spellAction.target == Action_Base.TargetType.ENEMY)
                {
                    BattleUI_Master.instance.EnableTargeting(
                        context.enemyCharacters.FindAll(c => !c.characterData.IsDead()),
                        (CharacterObject target) =>
                        {
                            targets.Add(target);
                            isDecided = true;
                        }
                    );
                }
                else if (spellAction.target == Action_Base.TargetType.ALL_ENEMIES)
                {
                    targets.AddRange(context.enemyCharacters.FindAll(c => !c.characterData.IsDead()));
                    isDecided = true;
                }
                else if (spellAction.target == Action_Base.TargetType.ALL_ALLIES)
                {
                    targets.AddRange(context.playerCharacters.FindAll(c => !c.characterData.IsDead()));
                    isDecided = true;
                }
                else if (spellAction.target == Action_Base.TargetType.ALLY)
                {
                    BattleUI_Master.instance.EnableTargeting(
                        context.playerCharacters.FindAll(c => !c.characterData.IsDead()),
                        (CharacterObject target) =>
                        {
                            targets.Add(target);
                            isDecided = true;
                        }
                    );
                }
            });

        });


        while (!isDecided)
        {
            yield return null;
        }

        battleUI.Disable();
        battleUI.attackButton.onClick.RemoveAllListeners();
        battleUI.spellButton.onClick.RemoveAllListeners();

        onComplete.Invoke(selectedAction, targets);
        yield break;
    }
}

public class DumbEnemyActionSelector : ActionSelector
{
    public override IEnumerator SelectAction(
        CharacterObject actor,
        BattleContext context,
        Action<Action_Base, List<CharacterObject>> onComplete
    )
    {
        Action_Base selectedAction = actor.characterData.baseStatistics.basicAction;
        List<CharacterObject> possibleTargets = context.playerCharacters.FindAll(c => !c.characterData.IsDead());
        CharacterObject target = possibleTargets[UnityEngine.Random.Range(0, possibleTargets.Count)];
        onComplete(selectedAction, new List<CharacterObject> { target });
        yield return new WaitForSeconds(1f);
    }
}
