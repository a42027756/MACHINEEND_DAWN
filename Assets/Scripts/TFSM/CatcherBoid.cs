using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherBoid : Singleton<CatcherBoid>
{
    private List<Catcher> boid = new List<Catcher>();

     public static float boidDivisionRatio = 0.25f;
    
     private float boidDivisionEffectRadius = 1f;
    
    public void AddSelfToList(Catcher self)
    {
        boid.Add(self);
    }

    public void RemoveSelfFromList(Catcher self)
    {
        boid.Remove(self);
    }

    private float CalcCatchersDistance(Catcher a, Catcher b)
    {
        Vector2 aPos = a.transform.position;
        Vector2 bPos = b.transform.position;
        float distance = (aPos - bPos).magnitude;
        return distance;
    }

    private Vector2 CalcCatchersDirectionVector2(Catcher from, Catcher to)
    {
        Vector2 fromPos = from.transform.position;
        Vector2 toPos = to.transform.position;
        Vector2 retVector2 = (toPos - fromPos).normalized;
        return retVector2;
    }
    
    private List<Catcher> GetAdjacentPeers(Catcher self)
    {
        List<Catcher> adjacentPeers = new List<Catcher>();

        foreach (var entity in boid)
        {
            float distance = CalcCatchersDistance(self, entity);

            if (distance <= boidDivisionEffectRadius && self != entity)
            {
                adjacentPeers.Add(entity);
            }
        }
        return adjacentPeers;
    }

    public Vector2 GetBoidDivisionVelocity(Catcher self)
    {
        List<Catcher> adjacentPeers = GetAdjacentPeers(self);
    
        Vector2 divisionVelocity = Vector2.zero;

        foreach (var entity in adjacentPeers)
        {
            float distance = CalcCatchersDistance(self, entity);
            Vector2 directionVector2 = CalcCatchersDirectionVector2(entity, self);
            float reciprocal = 1f / (distance * distance);

            divisionVelocity += directionVector2 * reciprocal;
        }
        
        return divisionVelocity;
    }

    public Vector2 GetBoidDivisionDirectionVelocity(Catcher self)
    {
        Vector2 divisionVelocityDirection = GetBoidDivisionVelocity(self).normalized;
        return divisionVelocityDirection;
    }
    
}
