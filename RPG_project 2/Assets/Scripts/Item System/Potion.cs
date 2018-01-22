using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item {

    public Buff buff;

    public override void Use() {
        base.Use();
        PlayerStats.instance.AddBuff(buff);
        RemoveFromInventory();
    }

}
