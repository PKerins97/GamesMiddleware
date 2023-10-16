
public interface iCollidable 
{
    bool isColliding(iCollidable otherObject);

    void resolvedVelocityForCollisonWith(iCollidable otherObject);
}
