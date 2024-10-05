using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game3D;

public class Scene
{
    private List<Entity> entities = new();

    public Camera Camera;
    public ContentManager Content;

    public Scene(GraphicsDevice gpu, ContentManager content)
    {
        Camera = new(gpu, Vector3.UnitZ);
        Content = content;
    }

    public void AddEntity(Entity entity)
    {
        entity.AddScene(this);
        entity.Start();
        entities.Add(entity);
    }

    public void Update(float deltaTime)
    {
        foreach (var entity in entities)
        {
            entity.Update(deltaTime);
            entity.UpdateAnimation(deltaTime);
            entity.UpdateTransforms();
        }
        Camera.DebugUpdate(deltaTime);
    }

    public void Draw()
    {
        foreach (var entity in entities)
        {
            entity.Draw(Camera.View, Camera.Projection);
        }
    }
}
