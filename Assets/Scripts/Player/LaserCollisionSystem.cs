using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


public partial struct LaserCollisionSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Laser>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Entity laserEntity = SystemAPI.GetSingletonEntity<Laser>();
        LocalTransform laserTransform = state.EntityManager.GetComponentData<LocalTransform>(laserEntity);
        Laser laser = state.EntityManager.GetComponentData<Laser>(laserEntity);
        float2 laserStart = laserTransform.Position.xy;
        float2 laserEnd = (laserTransform.Position + laser.Direction * laser.Length).xy;

        new LaserCollisionCheckJob()
        {
            LaserStart = laserStart,
            LaserEnd = laserEnd
        }.ScheduleParallel();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }
}

[BurstCompile]
public partial struct LaserCollisionCheckJob : IJobEntity
{
    public float2 LaserStart;
    public float2 LaserEnd;

    void Execute(ref EnemyTag enemy, in LocalTransform transform)
    {
        float2 enemyPos = transform.Position.xy;

        if (LineCircleCollisionTest(LaserStart, LaserEnd, enemyPos, 0.5f))
        {
            enemy.IsDead = true;
        }
    }

    [BurstCompile]
    private bool LineCircleCollisionTest(float2 lineStart, float2 lineEnd, float2 circleCenter, float circleRadius)
    {
        // is either end INSIDE the circle?
        // if so, return true immediately
        bool inside1 = PointCircleTest(lineStart, circleCenter, circleRadius);
        bool inside2 = PointCircleTest(lineEnd, circleCenter, circleRadius);
        if (inside1 || inside2) return true;

        // get length of the line
        float distX = lineStart.x - lineEnd.x;
        float distY = lineStart.y - lineEnd.y;
        float len = math.sqrt((distX * distX) + (distY * distY));

        // get dot product of the line and circle
        float dot = ((circleCenter.x - lineStart.x) * (lineEnd.x - lineStart.x) +
                     ((circleCenter.y - lineStart.y) * (lineEnd.y - lineStart.y))) / math.pow(len, 2);

        // find the closest point on the line
        float closestX = lineStart.x + dot * (lineEnd.x - lineStart.x);
        float closestY = lineStart.y + dot * (lineEnd.y - lineStart.y);

        // is this point actually on the line segment?
        // if so keep going, but if not, return false
        bool onSegment = LinePointTest(lineStart, lineEnd, new float2(closestX, closestY));
        if (!onSegment) return false;

        // get distance to closest point
        distX = closestX - circleCenter.x;
        distY = closestY - circleCenter.y;
        float distance = math.sqrt((distX * distX) + (distY * distY));

        if (distance <= circleRadius)
        {
            return true;
        }

        return false;
    }

    [BurstCompile]
    private bool PointCircleTest(float2 point, float2 circleCenter, float circleRadius)
    {
        // get distance between the point and circle's center
        // using the Pythagorean Theorem
        float distX = point.x - circleCenter.x;
        float distY = point.y - circleCenter.y;
        float distance = math.sqrt((distX * distX) + (distY * distY));

        // if the distance is less than the circle's
        // radius the point is inside!
        if (distance <= circleRadius)
        {
            return true;
        }

        return false;
    }

    [BurstCompile]
    private bool LinePointTest(float2 lineStart, float2 lineEnd, float2 point)
    {
        // get distance from the point to the two ends of the line
        float d1 = math.distance(point, lineStart);
        float d2 = math.distance(point, lineEnd);

        // get the length of the line
        float lineLen = math.distance(lineStart, lineEnd);

        float buffer = 0.1f;

        // if the two distances are equal to the line's
        // length, the point is on the line!
        // note we use the buffer here to give a range,
        // rather than one #
        if (d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer)
        {
            return true;
        }

        return false;
    }
}