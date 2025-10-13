using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
public class BattleManager
{
    List<BattleSite> mBattleSites;
    public void StartBattle(BattlePartyComponent playerParty, BattlePartyComponent enemyParty)
    {
        if(mBattleSites == null)
        {
            mBattleSites = new List<BattleSite>();
            mBattleSites.AddRange(GameObject.FindObjectsByType<BattleSite>(FindObjectsSortMode.None));
        }

        Debug.Log($"Starting Battle between: {playerParty.gameObject.name} and {enemyParty.gameObject.name}");
        PrepParty(playerParty);
        PrepParty(enemyParty);
    }

    private void PrepParty(BattlePartyComponent party)
    {
        BattleSite partyBattleSite = mBattleSites.Find((battleSite) => { return !battleSite.IsPlayerSite; });
        if (party.gameObject.CompareTag("Player"))
        {
            partyBattleSite = mBattleSites.Find((battleSite) => { return battleSite.IsPlayerSite; });
        }

        int i = 0;
        foreach (BattleCharacter partyBattleCharacter in party.GetBattleCharacters())
        {
            partyBattleCharacter.transform.position = partyBattleSite.GetPositionForUnit(i);
            partyBattleCharacter.transform.rotation = partyBattleSite.transform.rotation;
            i++;
        }

        party.FinishPrep();
    }
    
}
