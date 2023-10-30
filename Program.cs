using static System.Console;
using System;

Random rand = new Random();
// int N = 1;
int N = 10_000;
int Attackers;
int Defenders;

// WriteLine("Attackers: " + Attackers);
// WriteLine("Defenders: " + Defenders);
monteCarlo();

int roll() => rand.Next(6) + 1;

void monteCarlo()
{
    int count = 0;
    bool result;
    for (int i = 0; i < N; i++)
    {
        result = Battles();
        // WriteLine(result);
        if (result)
            count++;
    }
    WriteLine(count / (float)N * 100); // ~0,167
    WriteLine(100 - count / (float)N * 100); // ~0,167
}

bool Battles()
{
    Attackers = 4;
    Defenders = 1;
    while (Attackers > 1 && Defenders > 0)
    {
        Battle();
        // WriteLine("Attackers: " + Attackers);
        // WriteLine("Defenders: " + Defenders);
    }
    // WriteLine(Attackers);
    if (Attackers > 1)
        return true;
    else
        return false;
}

void Battle()
{
    int attackersCount = Attackers - 1;
    int defendersCount = Defenders;
    int attackersRemaining = attackersCount % 3;
    int defendersRemaining = defendersCount % 3;
    int attackersSquad = attackersCount / 3;
    int defendersSquad = defendersCount / 3;

    if (defendersCount < 3 && attackersSquad > 0)
    {
        attackersSquad--;
        attackersRemaining += 3;
    }

    if (attackersCount < 3 && defendersSquad > 0)
    {
        defendersSquad--;
        defendersRemaining += 3;
    }

    // WriteLine("attackersCount: " + attackersCount);
    // WriteLine("attackersRemaining: " + attackersRemaining);
    // WriteLine("attackersSquad: " + attackersSquad);

    // WriteLine();

    // WriteLine("defendersCount: " + defendersCount);
    // WriteLine("defendersRemaining: " + defendersRemaining);
    // WriteLine("defendersSquad: " + defendersSquad);
    
    // WriteLine();

    int round = defendersSquad < attackersSquad ? defendersSquad : attackersSquad;
    int[] attackingData = new int[3];
    int[] defendingData = new int[3];
    // WriteLine("round: " + round);

    int roundRemaining =
        defendersRemaining < attackersRemaining ? defendersRemaining : attackersRemaining;
    int[] attackingDataRemaining = new int[attackersRemaining];
    int[] defendingDataRemaining = new int[defendersRemaining];
    // WriteLine("roundRemaining: " + roundRemaining);

    for (int i = 0; i < round; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            attackingData[j] = roll();
            defendingData[j] = roll();
            // WriteLine(attackingData[j]);
            // WriteLine(defendingData[j]);
        }
        Array.Sort(attackingData);
        Array.Reverse(attackingData);
        Array.Sort(defendingData);
        Array.Reverse(defendingData);
        for (int k = 0; k < 3; k++)
        {
            if (attackingData[k] > defendingData[k])
                Defenders--;
            else
                Attackers--;
        }
    }

    for (int j = 0; j < attackersRemaining; j++)
        attackingDataRemaining[j] = roll();
    for (int j = 0; j < defendersRemaining; j++)
        defendingDataRemaining[j] = roll();
    Array.Sort(attackingDataRemaining);
    Array.Reverse(attackingDataRemaining);
    Array.Sort(defendingDataRemaining);
    Array.Reverse(defendingDataRemaining);

    for (int k = 0; k < roundRemaining; k++)
    {
        if (attackingDataRemaining[k] > defendingDataRemaining[k])
            Defenders--;
        else
            Attackers--;
    }
}
