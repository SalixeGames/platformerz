namespace Platformerz.scripts.enemies;

public enum EnemiesDirection
{
    Vertical,
    Horizontal
}

public enum EnemiesAggroLevel
{
    Flee,
    Passive,
    Aggressive
}

public enum EnemiesAttackType
{
    Forward,
    Target,
    Spring,  // Retirer?
    Projectile
}