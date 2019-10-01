using System.Collections.Generic;
using System.Linq;

public class DealDamageToRandomEnemy : Ability
{
    public DealDamageToRandomEnemy(PlayerController cardOwner, CardOnBoardController card, int abilityAmount, int abilityCharges) : base(cardOwner, card, abilityAmount, abilityCharges) { }

    public override void WhenCardPlayed()
    {
        var generator = new System.Random();
        List<CardOnBoardController> cardsOnFrontRow = GameManager.Instance.WhoseTurn.otherPlayer.frontRow.CardsOnFrontRow;
        List<CardOnBoardController> cardsOnBackRow = GameManager.Instance.WhoseTurn.otherPlayer.backRow.CardsOnBackRow;

        List<CardOnBoardController> allCards = cardsOnFrontRow.Concat(cardsOnBackRow).ToList();

        //Get random creature from enemy board if there is one
        if (allCards.Count > 0)
        {
            int targetIndex = generator.Next(allCards.Count);
            CardOnBoardController target = allCards[targetIndex];
        
            new DealDamageCommand(cardOwner, target.ID, abilityAmount).AddToQueue();
        }
    }
}
