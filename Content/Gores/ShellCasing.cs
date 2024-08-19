using Terraria.DataStructures;
using Terraria.GameContent;

namespace Aurora.Content.Gores;

public class ShellCasing : ModGore
{
    public override void SetStaticDefaults() {
        ChildSafety.SafeGore[Type] = true;
    }

    public override void OnSpawn(Gore gore, IEntitySource source) {
        gore.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
    }

    public override bool Update(Gore gore) {
        if (gore.alpha >= 255) {
            gore.active = false;
        }

        gore.alpha += 5;

        return true;
    }
}
