using Svelto.ECS;

namespace WalkerGame.EntitySystem
{
    public static class EntityGroups
    {
        public static ExclusiveGroup Creatures = new ExclusiveGroup();
        public static ExclusiveGroup Players = new ExclusiveGroup();
        public static ExclusiveGroup Statics = new ExclusiveGroup();
        public static ExclusiveGroup Decorations = new ExclusiveGroup();

        public static ExclusiveGroup[] Collidables =
        {
            Creatures, Players, Statics
        };
    }
}