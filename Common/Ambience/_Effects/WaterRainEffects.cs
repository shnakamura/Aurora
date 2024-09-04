using Aurora.Content.Dusts;

namespace Aurora.Common.Ambience;

public sealed class RainAmbienceEffects : ILoadable
{
    void ILoadable.Load(Mod mod) {
        On_Rain.Update += RainUpdateHook;
    }

    void ILoadable.Unload() { }

    private static void RainUpdateHook(On_Rain.orig_Update orig, Rain self) {
        orig(self);

        var isRainWet = Collision.WetCollision(self.position, 2, 2);
        var canSpawnBubble = Main.rand.NextFloat(250f) > Main.gfxQuality * 100f;

        if (!isRainWet || !canSpawnBubble) {
            return;
        }

        var dust = Dust.NewDustDirect(self.position, 2, 2, ModContent.DustType<BubbleDust>());
        var tile = Framing.GetTileSafely(self.position.ToTileCoordinates());

        switch (tile.LiquidType) {
            case LiquidID.Water:
                dust.velocity = self.velocity / 4f;
                break;
            case LiquidID.Lava:
                dust.velocity = -self.velocity.SafeNormalize(Vector2.Zero);
                dust.color = new Color(230, 174, 158);
                break;
            case LiquidID.Honey:
                dust.velocity = -self.velocity.SafeNormalize(Vector2.Zero) / 2f;
                dust.color = new Color(230, 227, 158);
                break;
            case LiquidID.Shimmer:
                dust.velocity = new Vector2(self.velocity.X, -self.velocity.Y).SafeNormalize(Vector2.Zero) * 2f;
                dust.color = new Color(250, 212, 246);
                break;
        }

        dust.scale += 0.5f * Main.cloudAlpha;

        self.active = false;
    }
}
