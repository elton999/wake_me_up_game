using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game3D;

public class Scene
{
    private List<Entity3d> _entities = new();
    private List<UIEntity> _ui = new();

    public Camera Camera;
    public ContentManager Content;

    public Scene(GraphicsDevice gpu, ContentManager content)
    {
        Camera = new(gpu, Vector3.UnitZ);
        Content = content;
    }

    public void AddEntity3d(Entity3d entity)
    {
        entity.AddScene(this);
        entity.Start();
        _entities.Add(entity);
    }

    public void Update(float deltaTime)
    {
        foreach (var entity in _entities)
        {
            entity.Update(deltaTime);
            entity.UpdateAnimation(deltaTime);
            entity.UpdateTransforms();
        }

        foreach (var entity in _ui)
        {
            entity.Update(deltaTime);
        }

        Camera.DebugUpdate(deltaTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var entity in _entities)
        {
            entity.Draw(Camera.View, Camera.Projection);
        }

        spriteBatch.Begin();
        foreach (var entity in _ui)
        {
            entity.Draw(spriteBatch);
        }
        spriteBatch.End();
    }
}
