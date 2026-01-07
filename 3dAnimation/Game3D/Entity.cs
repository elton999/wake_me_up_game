using Microsoft.Xna.Framework;
namespace Game3D;

public class Entity
{
    public Vector3 Position = new Vector3(0, 0, 0);

    private Scene _scene;
    public Scene Scene => _scene;

    public virtual void Start() { }

    public virtual void Update(float deltaTime) { }

    public void AddScene(Scene scene) => _scene = scene;

    public virtual void OnDestroy() { }
}
