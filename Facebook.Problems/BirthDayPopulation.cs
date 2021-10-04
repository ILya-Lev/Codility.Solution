using System;
using System.Linq;

namespace Facebook.Problems
{
    public class BirthDayPopulation
    {
        public static (int year, int population) FindMostPopulatedYear_Sorted((int birth, int death)[] lifetimes)
        {
            var birthPopularity = lifetimes
                .GroupBy(lt => lt.birth, lt => lt.birth)
                .OrderBy(g => g.Key)
                .Select(g => (year: g.Key, change: g.Count()))
                .ToList();

            var deathPopularity = lifetimes
                .GroupBy(lt => lt.death, lt => lt.death)
                .OrderBy(g => g.Key)
                .Select(g => (year: g.Key, change: g.Count()))
                .ToList();

            var maxYear = 0;
            int alive = 0, maxAlive = 0;

            int birthIndex = 0, deathIndex = 0;
            while (birthIndex < birthPopularity.Count)
            {
                alive += birthPopularity[birthIndex].change;
                var year = birthPopularity[birthIndex].year;

                birthIndex++;
                while (deathIndex < deathPopularity.Count && deathPopularity[deathIndex].year <= year)
                {
                    alive -= deathPopularity[deathIndex].change;

                    if (maxAlive < alive)
                    {
                        maxAlive = alive;
                        maxYear = deathPopularity[deathIndex].year;
                    }

                    deathIndex++;
                }

                if (maxAlive < alive)
                {
                    maxAlive = alive;
                    maxYear = year;
                }
            }

            return (maxYear, maxAlive);
        }

        public static (int year, int population) FindMostPopulatedYear((int birth, int death)[] lifetimes)
        {
            var (earliestBirth, latestDeath) = FindLimits(lifetimes);       //O(N)

            var yearPopulation = new int[latestDeath - earliestBirth + 1];
            FillInPopulation(yearPopulation, lifetimes, earliestBirth);     //O(N*max lifetime)

            var (yearShift, population) = FindMaxPopulation(yearPopulation);     //O(N)

            return (yearShift + earliestBirth, population);
        }

        private static (int, int) FindLimits((int birth, int death)[] lifetimes)
        {
            int earliestBirth = int.MaxValue, latestDeath = int.MinValue;
            foreach (var lifetime in lifetimes)
            {
                earliestBirth = Math.Min(earliestBirth, lifetime.birth);
                latestDeath = Math.Max(latestDeath, lifetime.death);
            }
            return (earliestBirth, latestDeath);
        }

        private static void FillInPopulation(int[] yearPopulation, (int birth, int death)[] lifetimes, int earliestBirth)
        {
            foreach (var lifetime in lifetimes)
            {
                for (int year = lifetime.birth; year <= lifetime.death; year++)
                {
                    yearPopulation[year - earliestBirth]++;
                }
            }
        }

        private static (int yearShift, int population) FindMaxPopulation(int[] yearPopulation)
        {
            var maxPopulation = int.MinValue;
            var shift = 0;
            for (int yearShift = 0; yearShift < yearPopulation.Length; yearShift++)
            {
                if (maxPopulation < yearPopulation[yearShift])
                {
                    maxPopulation = yearPopulation[yearShift];
                    shift = yearShift;
                }
            }

            return (shift, maxPopulation);
        }
    }
}
