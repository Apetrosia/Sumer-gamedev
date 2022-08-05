
using System;
using Leopotam.EcsLite;

namespace Client
{
    // Обёртка над сущностью в ECS - поколение запоминается на случай
    //  смерти entity или переиспользования индекса

    public struct Entity
    {
        public int index;
        public short gen;

        public static Entity Generate(EcsWorld ecsWorld)
        {
            int e = ecsWorld.NewEntity();
            return new Entity() {
                index = e,
                gen = ecsWorld.GetEntityGen(e)
            };
        }

        public bool Alive(EcsWorld ecsWorld)
        {
            return ecsWorld.GetEntityGen(index) == gen;
        }

// ################################################################# //
// ##### Методы для сравнения и хранения в хеш-таблице ############# //
// ################################################################# //

        public static bool operator == (Entity e1, Entity e2)
        {
            return e1.index == e2.index && e1.gen == e2.gen;
        }

        public static bool operator != (Entity e1, Entity e2)
        {
            return !(e1 == e2);
        }

        public override bool Equals(object obj)
        {
            return obj is Entity pointer &&
                   index == pointer.index &&
                   gen == pointer.gen;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(index, gen);
        }

// Перегрузка ToString для удобного вывода в unity редакторе //
        public override string ToString()
        {
            return $"Entity-{index:x8} (Gen-{gen})";
        }
    }
}
