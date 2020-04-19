using WalkerGame.Component;

namespace WalkerGame.Logic.SystemMessage.Collision
{
    public interface CollisionSubscriber
    {
        public void Collide(ref Transform thisBounds, ref Transform otherBounds);
        public int GetChannel();
    }
}